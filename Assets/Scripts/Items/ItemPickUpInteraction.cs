using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpInteraction : MonoBehaviour, IInteractable
{
    Kettle kettle;
    Item item;

    private void Awake()
    {
        kettle = GetComponent<Kettle>();
        item = GetComponent<Item>();
    }

    public void Interact()
    {
        if (Player.Instance.ActionBar.HasFreeSlots) Player.Instance.ActionBar.SetNextFreeSlot(item.Config);
        else Player.Instance.Inventory.Add(item.Config);

        if (kettle != null)
        {
            if (Player.Instance.ActionBar.KettleInvItem != null && Player.Instance.ActionBar.KettleInvItem.Quantity > 0)
            {
                Player.Instance.ActionBar.KettleInvItem.Volume = kettle.CurrentVolume;
            }
        }

        Destroy(item.gameObject);
    }

    public string InteractionLabel()
    {
        return "Pick Up " + item.Config.ItemName;
    }
}
