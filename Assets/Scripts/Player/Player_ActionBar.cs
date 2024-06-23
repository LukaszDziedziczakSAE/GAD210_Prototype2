using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ActionBar : MonoBehaviour
{
    [field: SerializeField] public Inventory.InvItem Slot1 { get; private set; }
    [field: SerializeField] public Inventory.InvItem Slot2 { get; private set; }
    [field: SerializeField] public Inventory.InvItem Slot3 { get; private set; }
    [field: SerializeField] public Inventory.InvItem Slot4 { get; private set; }
    [field: SerializeField] public Inventory.InvItem Slot5 { get; private set; }

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
            if (Slot1 == null) return 1;
            if (Slot2 == null) return 2;
            if (Slot3 == null) return 3;
            if (Slot4 == null) return 4;
            if (Slot5 == null) return 5;
            return -1;
        }
    }

    public static int NumberOfSlots => 5;

    public Inventory.InvItem NextFreeSlot { get; private set; }
}
