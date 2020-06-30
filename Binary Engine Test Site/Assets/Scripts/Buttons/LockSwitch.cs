using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSwitch : Button
{
    // On if the player stands on it, will remain so even when the player steps off

    protected override void toggleActive(bool beingPressed)
    {
        activated = true;
    }
}
