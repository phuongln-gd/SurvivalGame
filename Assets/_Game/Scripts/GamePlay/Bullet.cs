using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    private bool isRunning;
    CounterTime counterTime = new CounterTime();
    private Vector3 targPos;
    private float moveSpeed = 3f;
    public void OnInit(Vector3 target)
    {
        counterTime.Start(OnDespawn, 1.25f);
        isRunning = true;
        targPos = (target- TF.position).normalized;
    }

    private void Update()
    {
        counterTime.Execute();
        if (isRunning)
        {
            TF.position = TF.position + moveSpeed * Time.deltaTime * targPos;
        }   
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
        isRunning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Bot bot = collision.GetComponent<Bot>();
            bot.OnHit();
        }
    }
}
