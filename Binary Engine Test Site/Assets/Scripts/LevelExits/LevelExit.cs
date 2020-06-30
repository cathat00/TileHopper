using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit: MonoBehaviour
{
    // A gameobject that provides as the exit from one level to the next, can be open or closed

    protected SpriteRenderer spriteRender;

    public bool isOpen;
    
    [SerializeField] protected Sprite openSprite;
    [SerializeField] protected Sprite closedSprite;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    protected virtual void Open() { }
    protected virtual void Close() { }
}
