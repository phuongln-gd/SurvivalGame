using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : GameUnit
{
    [SerializeField] private Animator anim;
    [SerializeField] private NavMeshAgent agent;
    private string currentAnim;
    private bool isDeath;
    private IState<Bot> currentState;
    private Player target;
    private Vector3 targetPos;

    private void Update()
    {
        if(!isDeath && currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    private void Awake()
    {
        target = LevelManager.Instance.player;
    }
    public void OnInit()
    {
        OnReset();
        ChangeState(new PatronState());
    }

    public void OnDespawn()
    {
        ChangeState(null);
        StopMoving();
        ChangeAnim("death");
        isDeath = true;
        Invoke(nameof(DelayDespawn), 1f);
        LevelManager.Instance.BotDeath(this);
    }

    public void OnReset()
    {
        isDeath = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    internal void OnHit()
    {
        SimplePool.Spawn<CombatText>(PoolType.CombatText, TF.position, Quaternion.identity).OnInit(50);
        OnDespawn();
    }

    public void DelayDespawn()
    {
        SimplePool.Despawn(this);
    }
    private void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);

            currentAnim = animName;

            anim.SetTrigger(currentAnim);
        }
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void MoveToPlayer()
    {
        agent.enabled = true;
        Vector3 curPosPlayer = target.GetCurrentPosition();
        targetPos = new Vector3(curPosPlayer.x, curPosPlayer.y, 0);
        agent.SetDestination(target.GetCurrentPosition());
        ChangeAnim("move");
    }

    public bool IsDestination => Vector2.Distance( new Vector2(TF.position.x, TF.position.y),
        new Vector2(targetPos.x, targetPos.y)) < 0.1f;

    public bool CanAttack => Vector2.Distance(new Vector2(TF.position.x, TF.position.y),
        new Vector2(target.GetCurrentPosition().x, target.GetCurrentPosition().y)) < 0.1f;
    public void StopMoving()
    {
        agent.enabled = false;
    }

    public void OnAttack()
    {
        if (!isDeath)
        {
            ChangeAnim("hit");
        }
    }
}
