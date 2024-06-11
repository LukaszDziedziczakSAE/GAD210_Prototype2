using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Stat_Base : MonoBehaviour
{
    public abstract string StatName { get; protected set; }
    [field: SerializeField] public float Max { get; protected set; } = 100f;
    [field: SerializeField] public float Current { get; protected set; } = 100f;
    [field: SerializeField] public float ChangeRate { get; protected set; } = 0f;

    public event Action OnStatAtZero;
    public event Action OnStatAtMax;
    public event Action OnStatChanged;

    float statChangeLength;
    float statChangeAmount;
    float timedStatChangeRate;

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        if (ChangeRate < 0)
        {
            Remove(-ChangeRate * Time.deltaTime);
        }

        else if (ChangeRate > 0)
        {
             Add(ChangeRate * Time.deltaTime);
        }

        if (statChangeAmount != 0)
        {
            float amount;
            if (statChangeAmount > 0) amount = MathF.Min(timedStatChangeRate * Time.deltaTime, statChangeAmount);
            else amount = MathF.Max(timedStatChangeRate * Time.deltaTime, statChangeAmount);
            statChangeAmount -= amount;
            Modify(amount);
        }
    }

    public virtual void Add(float amount)
    {
        Current = Mathf.Min(Max, Current + amount);

        OnStatChanged?.Invoke();
        if (Current == Max)
        {
            OnStatAtMax?.Invoke();
        }
    }

    public virtual void Remove(float amount)
    {
        Current = Mathf.Max(0, Current - amount);

        OnStatChanged?.Invoke();
        if (Current == 0)
        {
            OnStatAtZero?.Invoke();
        }
    }

    public virtual void Modify(float amount)
    {
        Current += amount;
        if (Current > Max) Current = Max;
        else if (Current < 0) Current = 0;

        OnStatChanged?.Invoke();
        if (Current == Max) OnStatAtMax?.Invoke();
        if (Current == 0) OnStatAtZero?.Invoke();
    }

    public virtual void ResetStat()
    {
        Current = Max;
    }

    public virtual void ModifyChangeRate(float newChangeRate)
    {
        ChangeRate += newChangeRate;
    }

    public virtual void SetNewChangeRate(float newChangeRate)
    {
        ChangeRate = newChangeRate;
    }

    public float Percentage => Current / Max;

    public bool AtZero => Current == 0;

    public bool AtMax => Current == Max;

    public virtual void ChangeStatOverTime(float amount, float timeLength)
    {
        statChangeAmount = amount;
        statChangeLength = timeLength;
        timedStatChangeRate = amount / timeLength;
    }
}
