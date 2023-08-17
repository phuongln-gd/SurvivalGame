using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] GameObject sprite;
    private Player player;

    public void Awake()
    {
        player = LevelManager.Instance.player;
    }

    public GameObject Sprite_Skin => sprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            player.OnAttack(collision.transform.position);
        }
    }
}
