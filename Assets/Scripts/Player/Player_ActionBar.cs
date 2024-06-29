using System.Collections.Generic;
using UnityEngine;

public class Player_ActionBar : MonoBehaviour
{
    [SerializeField] Player player;
    [field: SerializeField] public Inventory.InvItem Slot1 { get; private set; }
    [field: SerializeField] public Inventory.InvItem Slot2 { get; private set; }
    [field: SerializeField] public Inventory.InvItem Slot3 { get; private set; }
    [field: SerializeField] public Inventory.InvItem Slot4 { get; private set; }
    [field: SerializeField] public Inventory.InvItem Slot5 { get; private set; }

    [field: SerializeField] public int ActiveSlot { get; private set; } = -1;

    private void Awake()
    {
        if (player == null) player = GetComponent<Player>();
    }

    public List<Inventory.InvItem> Slots
    {
        get
        {
            List<Inventory.InvItem> slots = new List<Inventory.InvItem>();

            slots.Add(Slot1);
            slots.Add(Slot2);
            slots.Add(Slot3);
            slots.Add(Slot4);
            slots.Add(Slot5);

            return slots;
        }
    }

    public Inventory.InvItem Slot(int slotIndex)
    {
        switch (slotIndex)
        {
            case 1: return Slot1;
            case 2: return Slot2;
            case 3: return Slot3;
            case 4: return Slot4;
            case 5: return Slot5;

            default: return null;
        } 
    }

    public int NextFreeSlotIndex
    {
        get
        {
            if (Slot1 == null || Slot1.Item == null) return 1;
            if (Slot2 == null || Slot2.Item == null) return 2;
            if (Slot3 == null || Slot3.Item == null) return 3;
            if (Slot4 == null || Slot4.Item == null) return 4;
            if (Slot5 == null || Slot5.Item == null) return 5;
            return -1;
        }
    }

    public static int NumberOfSlots => 5;

    public Inventory.InvItem NextFreeSlot => Slot(NextFreeSlotIndex);

    public bool HasFreeSlots => NextFreeSlotIndex != -1;

    public void SetSlot(int index, Inventory.InvItem item)
    {
        switch (index)
        {
            case 1: Slot1 = item; break;
            case 2: Slot2 = item; break; 
            case 3: Slot3 = item; break;
            case 4: Slot4 = item; break;
            case 5: Slot5 = item; break;
        }
        UI.ActionBar.UpdateSlots();
    }

    public void SetNextFreeSlot(Inventory.InvItem item)
    {
        SetSlot(NextFreeSlotIndex, item);
    }

    public void SetNextFreeSlot(ItemConfig itemConfig)
    {
        SetSlot(NextFreeSlotIndex, new Inventory.InvItem(itemConfig));
    }

    public void SetNextFreeSlot(ItemConfig itemConfig, int quantity)
    {
        SetSlot(NextFreeSlotIndex, new Inventory.InvItem(itemConfig, quantity));
    }

    public Inventory.InvItem KettleInvItem
    {
        get
        {
            for (int i = 1; i <= 5; i++)
            {
                if (Slot(i) == null || Slot(i).Item == null) continue;
                Kettle kettle = Slot(i).Item.Prefab.GetComponent<Kettle>();
                if (kettle != null) return Slot(i);
            }
            return null;
        }
    }

    public void ClearEmptySlots()
    {
        if (Slot1 != null && Slot1.Quantity <= 0) Slot1 = null;
        if (Slot2 != null && Slot2.Quantity <= 0) Slot2 = null;
        if (Slot3 != null && Slot3.Quantity <= 0) Slot3 = null;
        if (Slot4 != null && Slot4.Quantity <= 0) Slot4 = null;
        if (Slot5 != null && Slot5.Quantity <= 0) Slot5 = null;
    }

    public void RemoveKettle()
    {
        KettleInvItem.Quantity -= 1;
        ClearEmptySlots();
        UI.ActionBar.UpdateSlots();
    }


    public void SetActiveSlot(int slot)
    {
        ActiveSlot = slot;
        UI.ActionBar.UpdateSlots();
        player.ObjectInHand.UpdateHand();
    }

    public void ClearActiveSlot()
    {
        Slot(ActiveSlot).Quantity = 0;
        ClearEmptySlots();
        ActiveSlot = -1;
        UI.ActionBar.UpdateSlots();
    }
}
