using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour, IInteractable
{
    StatChangingObject statChangingObject;

    private void Awake()
    {
        statChangingObject = GetComponent<StatChangingObject>();
    }

    public void PickUp()
    {
        if (statChangingObject != null) statChangingObject.BeginStatChange();

        Destroy(gameObject);
    }

    public void Interact()
    {
        PickUp();
    }

    public string InteractionLabel()
    {
        return "Pick Up";
    }
}
