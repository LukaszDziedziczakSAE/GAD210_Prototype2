using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DrinkInteraction : MonoBehaviour, IInteractable
{
    Cup cup;
    StatChangingObject statChanging;

    private void Awake()
    {
        cup = GetComponent<Cup>();
        statChanging= GetComponent<StatChangingObject>();
    }

    public void Interact()
    {
        cup.CurrentVolume = 0;
        cup.Warm = false;

        statChanging.BeginStatChange();
    }

    public string InteractionLabel()
    {
        return "Drink Tea";
    }
}
