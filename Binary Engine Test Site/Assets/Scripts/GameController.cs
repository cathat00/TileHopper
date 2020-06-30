using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private List<Player> players;
    [SerializeField] private float nextLevel;
    private bool levelComplete;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        levelComplete = true;

        foreach (Player p in players)
        {
            if (!p.finishedLevel)
            {
                levelComplete = false;
            }
        }

        if (levelComplete)
        {

        }
    }
}
