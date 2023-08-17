using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    internal void OnAttack(Vector3 target)
    {
        Bullet bullet = SimplePool.Spawn<Bullet>(PoolType.Bullet, LevelManager.Instance.player.attackPosition.transform.position, LevelManager.Instance.player.attackPosition.transform.rotation);
        bullet.OnInit(target);
    }
}
