using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatItem : MonoBehaviour
{
    [SerializeField] TMP_Text statName;
    [SerializeField] TMP_Text statAmount;
    [SerializeField] RectTransform bar;

    Stat_Base stat;

    public void Initilise(Stat_Base stat)
    {
        this.stat = stat;

        statName.text = this.stat.StatName;
        UpdateStat();

        stat.OnStatChanged += UpdateStat;
    }

    public void UpdateStat()
    {
        statAmount.text = stat.Current.ToString("F0") + " / " + stat.Max.ToString("F0");
        bar.localScale = new Vector3(stat.Percentage, 1, 1);
    }

    private void OnDisable()
    {
        if (stat != null)
        {
            stat.OnStatChanged -= UpdateStat;
        }
    }
}
