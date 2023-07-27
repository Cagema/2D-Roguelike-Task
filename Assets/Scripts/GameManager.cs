using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public BoardManager BoardScript;

    private int level = 3;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        BoardScript = GetComponent<BoardManager>();
        InitGame();
    }

    private void InitGame()
    {
        BoardScript.SetupScene(level);
    }
}
