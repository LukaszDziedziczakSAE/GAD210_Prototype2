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
}
