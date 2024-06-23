using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New ItemConfig")]
public class ItemConfig : ScriptableObject
{
    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public Texture Icon { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
}
