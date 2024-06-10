using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnDepletion : MonoBehaviour
{
    [SerializeField] StatChangingArea area;

    private void Start()
    {
        if (area == null) area = GetComponent<StatChangingArea>();
        if (area == null) area = GetComponentInParent<StatChangingArea>();

        if (area != null) area.OnAreaDepleted += HideArea;
    }

    private void HideArea()
    {
        if (area == null) return;

        area.OnAreaDepleted -= HideArea;

        gameObject.SetActive(false);
    }
}
