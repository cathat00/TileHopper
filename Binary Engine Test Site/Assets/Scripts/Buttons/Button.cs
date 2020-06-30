using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Button : MonoBehaviour
{
    protected SpriteRenderer spriteRender;

    [SerializeField] public bool activated; // Whether or not the button is on/off

    [SerializeField] protected Sprite onSprite;
    [SerializeField] protected Sprite offSprite;

    [SerializeField] protected string[] pressedBy; // The gameobjects that can press this button

    // Start is called before the first frame update
    protected virtual void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        spriteRender.sprite = activated ? onSprite : offSprite;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        //if (Array.Exists(pressedBy, element => element == other.gameObject.tag))
        //{
        //    toggleActive(true);
        //}
        toggleActive(true);
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        //if (Array.Exists(pressedBy, element => element == other.gameObject.tag))
        //{
        //    toggleActive(false);
        //}
        toggleActive(false);
    }

    protected virtual void toggleActive(bool beingPressed) { }
}
