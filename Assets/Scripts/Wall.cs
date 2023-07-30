using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Sprite DmgSprite;
    public int Hp = 4;

    SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    public void DamageWall(int loss)
    {
        _sr.sprite = DmgSprite;
        Hp -= loss;
        if (Hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
