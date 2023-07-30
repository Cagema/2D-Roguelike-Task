using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    public int WallDamage = 1;
    public int PointsPerFood = 10;
    public int PointsPerSoda = 20;
    public float RestartLevelDelay = 1f;

    Animator _animator;
    int _food;

    protected override void Start()
    {
        _animator = GetComponent<Animator>();

        _food = GameManager.Instance.PlayerFoodPoints;

        base.Start();
    }

    private void Update()
    {
        if (!GameManager.Instance.PlayersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(WallDamage);
        _animator.SetTrigger("playerChop");
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDisable()
    {
        GameManager.Instance.PlayerFoodPoints = _food;
    }

    protected override void AttemptMove <T> (int xDir, int yDir)
    {
        _food--;

        base.AttemptMove <T> (xDir, yDir);

        RaycastHit2D hit;

        CheckIfGameOver();

        GameManager.Instance.PlayersTurn = false;
    }

    private void CheckIfGameOver()
    {
        if (_food <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exit"))
        {
            Invoke(nameof(Restart), RestartLevelDelay);
            enabled = false;
        }
        else if (collision.CompareTag("Food"))
        {
            _food += PointsPerFood;
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Soda"))
        {
            _food += PointsPerSoda;
            collision.gameObject.SetActive(false);
        }
    }

    public void LoseFood (int loss)
    {
        _animator.SetTrigger("playerHit");
        _food -= loss;
        CheckIfGameOver();
    }
}
