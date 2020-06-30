using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExit : LevelExit
{
    [SerializeField] ParticleSystem particleEffect;
    [SerializeField] Transform particleSpawn;
    private ButtonReactor buttons; // Buttons that determine whether or not the door will open

    protected override void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        buttons = GetComponent<ButtonReactor>();
    }

    private void Update()
    {
        if (!buttons.active) // Check if all buttons are activated...
        {
            isOpen = false;
            spriteRender.sprite = closedSprite;
        }
        else if (!isOpen) { // If just now opened on this iteration...
            isOpen = true;
            Open();
            spriteRender.sprite = openSprite;
        }
    }

    protected override void Open()
    {
        Instantiate(particleEffect, particleSpawn);
    }
}
