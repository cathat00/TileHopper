using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    [SerializeField] private LayerMask pullingLayer; // Layer of objects that can be pulled
    [SerializeField] private Transform pickup; // Transform that picked up items are parented to
    [SerializeField] private float timeToExit;

    private Animator anim;
    private float inverseExitTime;

    public bool finishedLevel = false; // Has exited the level through an open exit
    private bool manipPushable = false; // Manipulating (pushing/pulling) a pushable object
    private bool pullKeyHeld = false; // Key to pull objects is being held 

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        inverseExitTime = 1f / timeToExit;
    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = 0;
        int vertical = 0;

        // Horizontal movement
        if (Input.GetKeyDown(KeyCode.A)) { horizontal = -1; }
        if (Input.GetKeyDown(KeyCode.D)) { horizontal = 1; }
        // Vertical movement
        if (Input.GetKeyDown(KeyCode.W)) { vertical = 1; }
        if (Input.GetKeyDown(KeyCode.S)) { vertical = -1; }

        if (horizontal != 0) { vertical = 0; } // Can only move on one axis at a time

        if ((horizontal != 0 || vertical != 0) && !(moving || finishedLevel))
        {
            Move(horizontal, vertical);
        }

        // Set animation variables
        anim.SetBool("Manipulating Pushable", manipPushable);
        anim.SetBool("Moving", moving);

        // Check for other key up/down events
        pullKeyHeld = Input.GetKey(KeyCode.LeftShift); // Trying to pull an object
    }

    public override bool Move(int xDir, int yDir)
    {
        Vector2 start = transform.position; // The starting coordinates of the movement
        Vector2 end = start + new Vector2(xDir, yDir); // The end coordinates of the movement
        RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);
        bool canDoMove = false;

        // Didn't hit anything
        if (hit.collider == null) 
        {
            canDoMove = true;
            if (pullKeyHeld) { CheckForPull(xDir, yDir); } // Check if player can pull an object
        }

        // Hit a pushable object
        else if (hit.collider.tag == "PushPull") 
        {
            MovingObject push = hit.collider.gameObject.GetComponent<MovingObject>();
            // If the box can move, move it and move the player into its previous space
            if (push.Move(xDir, yDir))
            {
                canDoMove = true;
                manipPushable = true;
            }
        }

        // Hit an exit object
        else if (hit.collider.tag == "Exit")
        {
            LevelExit exit = hit.collider.gameObject.GetComponent<LevelExit>();
            if (exit.isOpen) // Is open, so the player can exit
            { 
                end = hit.collider.transform.position; // Move to the center of the door
                StartCoroutine(ExitLevel());
                canDoMove = true;
            } 
            else { canDoMove = false; } // Is closed, the player can't move into a closed exit
        }

        if (canDoMove) { StartCoroutine(SmoothMovement(end)); }
        return canDoMove; // Return result of the movement
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
        manipPushable = false;
    }

    private void CheckForPull(int pullX, int pullY)
    {
        Vector2 start = transform.position; // The starting coordinates of the movement
        Vector2 end = start - new Vector2(pullX, pullY); // The end coordinates of the movement
        RaycastHit2D pullCast = Physics2D.Linecast(start, end, pullingLayer);
        if (pullCast.collider != null) // There is an object to pull
        {
            PushPullObject obj = pullCast.collider.gameObject.GetComponent<PushPullObject>();
            obj.beingPulled = true; // The object is now being pulled
            obj.Move(pullX, pullY); // Pull the object
            this.manipPushable = true; // The player is now pulling an object
        }
    }

    private IEnumerator ExitLevel()
    {
        finishedLevel = true;
        while (transform.localScale.x >= 0 && transform.localScale.y >= 0)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, inverseExitTime * Time.deltaTime);
            yield return null;
        }
    }
}
