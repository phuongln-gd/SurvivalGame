using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static float SPEED_ATTACK = 1f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 5f;
    [SerializeField] private AttackRange attackRange;
    [SerializeField] private Weapon weapon;
    public GameObject attackPosition;
    CounterTime counterTime = new CounterTime();

    private string currentAnim;
    private bool isDead, isAttack;
    private float range = 1f;
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            counterTime.Execute();
            if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
            {
                Moving();
            }
            else
            {
                ChangeAnim("idle");
            }
        }
    }

    private void ChangeAnim(string animName)
    {
        if(currentAnim != animName)
        {
            anim.ResetTrigger(animName);

            currentAnim = animName;

            anim.SetTrigger(currentAnim);
        }
    }

    public void OnInit()
    {
        isDead = false;
        ResetAttack();
    }

    public void ResetAttack()
    {
        isAttack = false;
    }
    public void OnDespawn()
    {
        
    }
    internal void OnAttack(Vector3 targetPos)
    {
        if (!isAttack)
        {
            isAttack = true;
            weapon.OnAttack(targetPos);
            ChangeAnim("hit");
            counterTime.Start(ResetAttack, SPEED_ATTACK);
        }
    }

    private void Moving()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime * new Vector2(JoystickControl.direct.x, JoystickControl.direct.z));
        transform.position = rb.position;
        attackRange.Sprite_Skin.gameObject.transform.position = transform.position + new Vector3(JoystickControl.direct.x, JoystickControl.direct.z, 0).normalized * range;
        if (JoystickControl.direct.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(JoystickControl.direct.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        ChangeAnim("walk");
    }

    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }
}
