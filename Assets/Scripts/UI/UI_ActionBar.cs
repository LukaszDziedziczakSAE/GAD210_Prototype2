using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActionBar : MonoBehaviour
{
    public List<UI_ActionSlot> ActionSlots = new List<UI_ActionSlot>();
    [SerializeField] UI_ActionSlot actionSlotPrefab;

    private void OnEnable()
    {
        CreateListOfSlots();
    }

    private void OnDisable()
    {
        ClearListofSlots();
    }

    public void CreateListOfSlots()
    {
        ClearListofSlots();

        for (int i = 1; i <= Player_ActionBar.NumberOfSlots; i++)
        {
            UI_ActionSlot newSlot = SpawnSlot();
            newSlot.SetIndex(i);
            ActionSlots.Add(newSlot);
        }
    }

    public UI_ActionSlot SpawnSlot()
    {
        return Instantiate(actionSlotPrefab, transform);
    }

    public void ClearListofSlots()
    {

        if (ActionSlots.Count > 0)
        {
            foreach (UI_ActionSlot slot in ActionSlots)
            {
                Destroy(slot.gameObject);
            }
            ActionSlots.Clear();
        }
    }

    public void UpdateSlots()
    {
        foreach(UI_ActionSlot slot in ActionSlots)
        {
            slot.UpdateSlot();
        }
    }

}
