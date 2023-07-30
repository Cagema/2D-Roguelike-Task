using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float TurnDelay = 0.1f;
    public static GameManager Instance = null;
    public BoardManager BoardScript;
    public int PlayerFoodPoints = 100;
    [HideInInspector] public bool PlayersTurn = true;

    private int _level = 1;
    List<Enemy> _enemies;
    bool _enemiesMoving;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        _enemies = new();
        BoardScript = GetComponent<BoardManager>();
        InitGame();
    }
    private void Update()
    {
        if (PlayersTurn || _enemiesMoving) return;

        StartCoroutine(MoveEnemies());
    }
    private void InitGame()
    {
        _enemies.Clear();
        BoardScript.SetupScene(_level);
    }

    public void GameOver()
    {
        enabled = false;
    }

    public void AddEnemyToList(Enemy script)
    {
        _enemies.Add(script);
    }
    IEnumerator MoveEnemies()
    {
        _enemiesMoving = true;
        yield return new WaitForSeconds(TurnDelay);
        if (_enemies.Count == 0)
        {
            yield return new WaitForSeconds(TurnDelay);
        }

        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].MoveEnemy();
            yield return new WaitForSeconds(_enemies[i].MoveTime);
        }
    }
}
