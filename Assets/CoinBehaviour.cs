using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    Vector3 mouseStartPosition;
    bool isBeingHeldDown;
    public bool iAmMoving = false;
    [SerializeField] int deadZoneSize;
    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnMouseDown()
    {
        isBeingHeldDown = true;
        mouseStartPosition = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        if (isBeingHeldDown)
        {
            if(Mathf.Abs(diff.x) > deadZoneSize || Mathf.Abs(diff.y) > deadZoneSize)
            {
                rb2d.AddForce(invertedDiff.normalized*Factor);
                iAmMoving = true;
            }
        }

        isBeingHeldDown = false;

    }
    public int Factor;
    public Vector3 diff;
    public Vector3 invertedDiff;

    RaycastHit2D[] hits;
    void Update()
    {

        if (iAmMoving)
        {
            List<GameObject> others = CoinManager.instance.findNotMeCoins(this);
            hits = Physics2D.LinecastAll(others[0].transform.position, others[1].transform.position);
            Debug.DrawLine(others[0].transform.position, others[1].transform.position,Color.black);
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if(hit.collider.gameObject.GetComponent<CoinBehaviour>() == this)
                {
                    Debug.LogError("I AM PASSING THE RIGHT LINE");
                }
            }
        }

        if (isBeingHeldDown)
        {
            //vector3 diff
            diff = Input.mousePosition - mouseStartPosition;
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.position + diff);
            //Vector3 invertedDiff;
            invertedDiff.x = diff.x * -1;
            invertedDiff.y = diff.y * -1;
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.position + invertedDiff,Color.red);

        }

        if (rb2d.velocity.x == 0 || rb2d.velocity.y == 0)
            iAmMoving = false;
        else
            iAmMoving = true;
    }
}
