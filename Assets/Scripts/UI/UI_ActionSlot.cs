using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_ActionSlot : MonoBehaviour
{
    [field: SerializeField, Range(0,9)] public int SlotIndex { get; private set; }
    [SerializeField] RawImage icon;
    [SerializeField] Image boarder;
    [SerializeField] Color inactive;
    [SerializeField] Color active;

    public bool Active
    {
        get
        {
            return boarder.color == active;
        }
        set
        {
            boarder.color = value ? active : inactive;
        }
    }

    private Inventory.InvItem slot => Player.Instance.ActionBar.Slot(SlotIndex);

    private void Start()
    {
        Active = false;
    }

    public void SetIndex(int newIndex)
    {
        SlotIndex = newIndex;

        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (slot != null && slot.Item != null && slot.Quantity > 0)
        {
            if (slot.Volume > 0)
            {
                if (slot.Warmed)
                {
                    if (slot.Item.WarmIcon != null && icon != null) icon.texture = slot.Item.WarmIcon;
                    else icon.texture = null;
                }
                else
                {
                    if (slot.Item.ColdIcon != null && icon != null) icon.texture = slot.Item.ColdIcon;
                    else icon.texture = null;
                }
            }
            else
            {
                if (slot.Item.Icon != null && icon != null) icon.texture = slot.Item.Icon;
                else icon.texture = null;
            }
        }
        else
        {
            icon.texture = null;
        }

        Active = Player.Instance.ActionBar.ActiveSlot == SlotIndex;
    }
}
