using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTap : MonoBehaviour, IInteractable
{
    Player_ActionBar playerActionBar => Player.Instance.ActionBar;

    public void Interact()
    {
        if (playerActionBar.KettleInvItem != null && !playerActionBar.KettleInvItem.IsFull)
        {
            playerActionBar.KettleInvItem.Volume = playerActionBar.KettleInvItem.Item.MaxVolume;
        }
    }

    public string InteractionLabel()
    {
        if (playerActionBar.KettleInvItem != null && !playerActionBar.KettleInvItem.IsFull)
        {
            return "Fill Kettle";
        }
        else return string.Empty;
    }
}
