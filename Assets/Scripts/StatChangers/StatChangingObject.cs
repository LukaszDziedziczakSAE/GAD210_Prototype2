using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatChangingObject : MonoBehaviour
{
    [SerializeField, UClassSelector(typeof(Stat_Base))] string type;
    protected System.Type statType => Type.GetType(type);
    [SerializeField, Tooltip("How much will the stat change")] protected float changeAmount;
    [SerializeField, Tooltip("Over how much time the stat will change")] protected float timeLength = 1f;

    public void BeginStatChange()
    {
        Stat_Base playerStat = Player.Instance.GetStat(statType);
        if (playerStat == null) return;

        if (timeLength > 0)
        {
            playerStat.ChangeStatOverTime(changeAmount, timeLength);
        }

        else playerStat.Modify(changeAmount);
    }
}
