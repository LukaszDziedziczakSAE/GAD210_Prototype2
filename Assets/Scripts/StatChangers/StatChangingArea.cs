using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class StatChangingArea : MonoBehaviour
{
    protected System.Type statType { get; set; }
    [SerializeField, Tooltip("How fast the stat will change")] protected float changeRate;
    [SerializeField, Tooltip("How much value will change in total, -1 means unlimited")] protected float amountToChange = -1;

    Stat_Base playerStat;

    public event Action OnAreaDepleted;

    private void Update()
    {
        if (playerStat == null) return;

        if (amountToChange == -1)
        {
            if (changeRate > 0 && !playerStat.AtMax)
            {
                float amount = changeRate * Time.deltaTime;
                playerStat.Add(amount);
            }

            else if (changeRate < 0 && !playerStat.AtZero)
            {
                float amount = -changeRate * Time.deltaTime;
                playerStat.Remove(amount);
            }
        }

        else if (amountToChange > 0)
        {
            if (changeRate > 0 && !playerStat.AtMax)
            {
                float amount = Mathf.Min(changeRate * Time.deltaTime, amountToChange);
                playerStat.Add(amount);
                amountToChange -= amount;

                if (amountToChange == 0)
                {
                    OnAreaDepleted?.Invoke();
                }
            }

            else if (changeRate < 0 && !playerStat.AtZero)
            {
                float amount = Mathf.Min(-changeRate * Time.deltaTime, amountToChange);
                playerStat.Remove(amount);
                amountToChange -= amount;

                if (amountToChange == 0)
                {
                    OnAreaDepleted?.Invoke();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            playerStat = player.GetStat(statType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player) && playerStat == player.GetStat(statType))
        {
            playerStat = null;
        }
    }
}