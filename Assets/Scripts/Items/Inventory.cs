using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] public List<InvItem> InvItems { get; private set; } = new List<InvItem>();

    [System. Serializable]
    public class InvItem
    {
        public ItemConfig Item;
        public int Quantity;
        public float Volume;
        public bool Warmed;

        public float VolumePercentage => Volume / Item.MaxVolume;
        public bool IsFull => VolumePercentage == 1;

        public InvItem(ItemConfig item)
        {
            Item = item;
            Quantity = 1;
        }

        public InvItem(ItemConfig item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public void Add(int quantity = 1)
        {
            if (quantity > 0)
            {
                Quantity += quantity;
            }
        }

        public void Remove(int quantity = 1)
        {
            if (quantity > 0)
            {
                Quantity -= quantity;
            }
        }
    }

    public void Add(InvItem newItems)
    {
        bool foundInventory = false;
        foreach (InvItem invItem in InvItems)
        {
            if (invItem.Item == newItems.Item)
            {
                invItem.Quantity += newItems.Quantity;
                foundInventory = true;
            }
        }

        if (!foundInventory)
        {
            InvItems.Add(newItems);
        }
    }

    public void Add(ItemConfig itemConfig)
    {
        Add(new InvItem(itemConfig));
    }

    public void Add(ItemConfig itemConfig, int quantity)
    {
        Add(new InvItem(itemConfig, quantity));
    }
}
