using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpInteraction : MonoBehaviour, IInteractable
{
    Kettle kettle;
    Item item;
    Cup cup;

    private void Awake()
    {
        kettle = GetComponent<Kettle>();
        item = GetComponent<Item>();
        cup = GetComponent<Cup>();
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
                Player.Instance.ActionBar.KettleInvItem.Warmed = kettle.Cooked;
            }
        }

        if (cup != null)
        {
            if (Player.Instance.ActionBar.CupInvItem != null && Player.Instance.ActionBar.CupInvItem.Quantity > 0)
            {
                Player.Instance.ActionBar.CupInvItem.Volume = cup.CurrentVolume;
                Player.Instance.ActionBar.CupInvItem.Warmed = cup.Warm;
            }
        }

        Destroy(item.gameObject);
        UI.ActionBar.UpdateSlots();
    }

    public string InteractionLabel()
    {
        return "Pick Up " + item.Config.ItemName;
    }
}
