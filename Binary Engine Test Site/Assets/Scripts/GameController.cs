using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private int currentLevel = 0;

    private GameObject[] players;
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
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        levelComplete = true;

        foreach (GameObject p in players)
        {
            Player playerState = p.GetComponent<Player>();
            if (!playerState.finishedLevel)
            {
                levelComplete = false;
            }
        }

        if (levelComplete)
        {
            SceneManager.LoadScene(++currentLevel); // Load the next level!
        }
    }
}
