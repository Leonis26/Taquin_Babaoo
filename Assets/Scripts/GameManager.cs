using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public System.Random Rand { get; private set; }// = new System.Random(0);
    [SerializeField] TaquinManager taquinManager;
    [SerializeField] string[] resourceFolders;
    int tries = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        RandomTaquinLoad();
    }

    void RandomTaquinLoad()
    {
        Rand = new System.Random(tries++);
        taquinManager.LoadGameWithResource(resourceFolders[Rand.Next(resourceFolders.Length)]);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        RandomTaquinLoad();
    }

    public void Win()
    {
        Debug.Log("win");
        StartCoroutine(WinAnimationAndRestart());
    }

    IEnumerator WinAnimationAndRestart()
    {
        taquinManager.ShowHiddenTile();
        yield return new WaitForSeconds(2); // do animations / show score here
        RestartLevel();
    }
}
