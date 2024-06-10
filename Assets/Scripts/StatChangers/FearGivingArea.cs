using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearGivingArea : StatChangingArea
{
    private void Awake()
    {
        statType = FindAnyObjectByType<Stat_Fear>().GetType();
    }
}
