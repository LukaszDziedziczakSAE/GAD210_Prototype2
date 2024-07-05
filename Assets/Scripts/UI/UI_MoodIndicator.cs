using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MoodIndicator : MonoBehaviour
{
    [SerializeField] Color lowEnd;
    [SerializeField] Color highEnd;
    [SerializeField] Color mid;
    [SerializeField] Image indicator;

    [SerializeField] Stat_Mood mood;


    private void Update()
    {
        if (mood != null && indicator != null)
        {
            indicator.color = moodColor;
        }
        
    }

    private Color moodColor
    {
        get
        {
            if (mood.Current < (mood.Max/2))
            {
                return Color.Lerp(lowEnd, mid, mood.Current / (mood.Max / 2));
            }

            else
            {
                return Color.Lerp(mid, highEnd, (mood.Current / 2) / (mood.Max / 2));
            }
        }
    }
}
