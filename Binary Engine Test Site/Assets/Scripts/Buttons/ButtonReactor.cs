using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReactor : MonoBehaviour
{
    // A gameobject that reacts to buttons being pressed by changing states
    protected SpriteRenderer spriteRender;

    [SerializeField] protected List<Button> dependencies; // Dependency buttons for activation
    [SerializeField] public bool active; // State change activated

    //[SerializeField] protected Sprite activatedSprite;
    //[SerializeField] protected Sprite inactiveSprite;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        List<bool> buttonStates = new List<bool>();

        foreach (Button b in dependencies) // Add all current button states to the buttonstates list
        {
            buttonStates.Add(b.activated);
        }

        if (!buttonStates.Contains(false)) // Check if all buttons are active
        { 
            if (!this.active)
            {
                //activate();
                active = true;
            }
        }
        else { active = false; } // Reactor isn't active if one or more dependency buttons is not active
    }

    //protected virtual void activate() { }
}
