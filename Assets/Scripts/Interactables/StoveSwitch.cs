using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] Stove stove;

    public void Interact()
    {
        stove.FireOn = !stove.FireOn;
    }

    public string InteractionLabel()
    {
        if (stove.FireOn)
        {
            return "Turn Stove Off";
        }
        else
        {
            return "Turn Stove On";
        }
    }
}
