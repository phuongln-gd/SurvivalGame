using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : GameUnit
{
    [SerializeField] Text hpText;
    public void OnInit(float hp)
    {
        hpText.text = hp.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
