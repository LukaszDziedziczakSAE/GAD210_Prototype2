using UnityEngine;

public class FillCupInteraction : MonoBehaviour, IInteractable
{
    Cup cup;

    private void Awake()
    {
        cup = GetComponent<Cup>();
    }

    public void Interact()
    {
        float fillAmount = cup.Config.MaxVolume;

        Player.Instance.ActionBar.KettleInvItem.Volume -= fillAmount;
        cup.CurrentVolume += fillAmount;
        cup.Warm = Player.Instance.ActionBar.KettleInvItem.Warmed;

        UI.ActionBar.UpdateSlots();
    }

    public string InteractionLabel()
    {
        return "Fill " + cup.Config.ItemName;
    }
}
