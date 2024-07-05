using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    Player player;
    [SerializeField] float maxDistance = 2.5f;

    IInteractable interactable;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.Input.OnInteractPress += OnInteraction;
    }

    private void OnDisable()
    {
        player.Input.OnInteractPress -= OnInteraction;
    }

    private void Update()
    {
        if (player.ObjectInHand.HasObjectInHand) return;

        Ray ray = player.Camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        Debug.DrawRay(ray.origin, ray.direction * maxDistance);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (hit.collider.TryGetComponent<Cup>(out Cup cup))
            {
                IInteractable interactableObject = null;

                if (cup.CurrentVolume > 0 && cup.Warm)
                {
                    interactableObject = cup.GetComponent<DrinkInteraction>();
                }
                else if (player.ActionBar.KettleInvItem != null && player.ActionBar.KettleInvItem.Quantity > 0 && player.ActionBar.KettleInvItem.Volume >= cup.Config.MaxVolume && player.ActionBar.KettleInvItem.Warmed)
                {
                    interactableObject = cup.GetComponent<FillCupInteraction>();
                }
                else
                {
                    interactableObject = cup.GetComponent<ItemPickUpInteraction>();
                }

                if (interactableObject != null)
                {
                    interactable = interactableObject;
                    UI.LeftInteractIndicator.gameObject.SetActive(true);
                    string label = interactable.InteractionLabel();
                    if (label != "") UI.LeftInteractIndicator.SetLabel(label);
                    else UI.LeftInteractIndicator.gameObject.SetActive(false);
                }
                else if (interactable != null)
                {
                    interactable = null;
                    UI.LeftInteractIndicator.gameObject.SetActive(false);
                }
            }
            else
            {
                IInteractable interactableObject = hit.collider.GetComponentInParent<IInteractable>();
                if (interactableObject != null)
                {
                    interactable = interactableObject;
                    UI.LeftInteractIndicator.gameObject.SetActive(true);
                    string label = interactable.InteractionLabel();
                    if (label != "") UI.LeftInteractIndicator.SetLabel(label);
                    else UI.LeftInteractIndicator.gameObject.SetActive(false);
                }
                else if (interactable != null)
                {
                    interactable = null;
                    UI.LeftInteractIndicator.gameObject.SetActive(false);
                }
            }
        }
        else if (interactable != null)
        {
            interactable = null;
            UI.LeftInteractIndicator.gameObject.SetActive(false);
        }
    }

    private void OnInteraction()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
}
