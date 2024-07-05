using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StatChangingArea : MonoBehaviour
{
    [SerializeField, UClassSelector(typeof(Stat_Base))] string type;
    protected System.Type statType => Type.GetType(type);
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
                Debug.Log(type + ": " + playerStat.Current);
            }

            else if (changeRate < 0 && !playerStat.AtZero)
            {
                float amount = -changeRate * Time.deltaTime;
                playerStat.Remove(amount);
                Debug.Log(type + ": " + playerStat.Current);
            }
        }

        else if (amountToChange > 0)
        {
            if (changeRate > 0 && !playerStat.AtMax)
            {
                float amount = Mathf.Min(changeRate * Time.deltaTime, amountToChange);
                playerStat.Add(amount);
                amountToChange -= amount;

                Debug.Log(type + ": " + playerStat.Current);

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

                Debug.Log(type + ": " + playerStat.Current);

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
            Debug.Log("Player entered " + name + " (" + playerStat.ToString() + ")");

            if (playerStat == null) Debug.LogError("Missing " + type + " type");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player) && playerStat == player.GetStat(statType))
        {
            playerStat = null;
            Debug.Log("Player exited " + name);
        }
    }
}
