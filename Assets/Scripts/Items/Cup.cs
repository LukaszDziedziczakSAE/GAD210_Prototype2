using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : Item
{
    public float CurrentVolume;
    public float MaxVolume => Config.MaxVolume;

    public float FreeVolume => MaxVolume - CurrentVolume;
    public bool Warm
    {
        get
        {
            if (steam == null) return false;
            return steam.gameObject.activeSelf;
        }
        set
        {
            if (steam != null) steam.gameObject.SetActive(value);
        }
    }
    [SerializeField] ParticleSystem steam;

    private void Start()
    {
        Warm = false;
    }
}
