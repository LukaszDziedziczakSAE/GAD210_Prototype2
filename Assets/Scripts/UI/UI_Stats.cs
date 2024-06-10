using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Stats : MonoBehaviour
{
    [SerializeField] UI_StatItem statItemPrefab;

    List<UI_StatItem> statItems = new List<UI_StatItem>();

    public void DrawStatList()
    {
        ClearStatList();

        foreach (Stat_Base stat in Player.Stats)
        {
            UI_StatItem statItem = Instantiate(statItemPrefab, transform);
            statItem.Initilise(stat);
            statItems.Add(statItem);
        }
    }

    private void ClearStatList()
    {
        if (statItems.Count > 0)
        {
            foreach (UI_StatItem statItem in statItems)
            {
                Destroy(statItem.gameObject);
            }
            statItems.Clear();
        }
    }
}
