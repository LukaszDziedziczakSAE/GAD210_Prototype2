using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_InteractIndicator : MonoBehaviour
{
    [SerializeField] TMP_Text interactionLabel;

    public void SetLabel(string label)
    {
        interactionLabel.text = label;
    }
}
