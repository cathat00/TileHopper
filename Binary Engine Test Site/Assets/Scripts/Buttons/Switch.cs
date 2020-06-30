using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Button
{
    // On if the player is standing on it, off if the player is not standing on it
    
    protected override void toggleActive(bool beingPressed)
    {
        activated = beingPressed ? true : false;
    }
}
