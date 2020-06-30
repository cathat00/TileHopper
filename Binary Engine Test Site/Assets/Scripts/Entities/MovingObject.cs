using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    [SerializeField] protected float timeToMove;
    [SerializeField] protected LayerMask blockingLayer;

    public bool moving = false;
    protected Rigidbody2D rb2d;
    protected float inverseMoveTime;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / timeToMove;
    }

    public virtual bool Move(int xDir, int yDir)
    {
        Vector2 start = transform.position; // The starting coordinates of the movement
        Vector2 end = start + new Vector2(xDir, yDir); // The end coordinates of the movement
        RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);
        if (hit.collider == null) // Didn't hit anything
        {
            StartCoroutine(SmoothMovement(end));
            return true; // Able to move in given direction
        }
        else
        {
            return false; // Not able to move in given direction
        }
    }

    protected virtual IEnumerator SmoothMovement(Vector2 end)
    {
        moving = true;
        while (new Vector2(transform.position.x, transform.position.y) != end)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            rb2d.MovePosition(newPos);
            yield return null;
        }
        moving = false;
    }
}
