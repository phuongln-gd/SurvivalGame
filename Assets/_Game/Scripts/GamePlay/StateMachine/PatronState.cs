using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronState : IState<Bot>
{
    
    public void OnEnter(Bot t)
    {
    }

    public void OnExecute(Bot t)
    {
        t.MoveToPlayer();
        if (t.IsDestination)
        {
            Debug.Log("11");
            t.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot t)
    {

    }

}
