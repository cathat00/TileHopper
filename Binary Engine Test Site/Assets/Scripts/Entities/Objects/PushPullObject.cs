using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullObject : MovingObject
{
    public bool beingPulled;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        beingPulled = false;
    }

    // Update is called once per frame
    public override bool Move(int xDir, int yDir)
    {
        Vector2 start = transform.position; // The starting coordinates of the movement
        Vector2 end = start + new Vector2(xDir, yDir); // The end coordinates of the movement

        if (beingPulled)
        {
            StartCoroutine(SmoothMovement(end));
            return true; // Collisions don't matter in a pull, so don't check for them
        }

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

    protected override IEnumerator SmoothMovement(Vector2 end)
    {
        moving = true;
        Vector3 start = transform.position;
        while (new Vector2(transform.position.x, transform.position.y) != end)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            rb2d.MovePosition(newPos);
            yield return null;
        }
        moving = false;
        beingPulled = false;
    }
}