using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ActionSlot : MonoBehaviour
{
    [field: SerializeField, Range(0,9)] public int SlotIndex { get; private set; }
    [SerializeField] RawImage icon;

    private Inventory.InvItem slot => Player.Instance.ActionBar.Slot(SlotIndex);

    public void SetIndex(int newIndex)
    {
        SlotIndex = newIndex;
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (slot != null && slot.Item != null && slot.Quantity > 0)
        {
            if (slot.Item.Icon != null && icon != null) icon.texture = slot.Item.Icon;
        }
    }
}
