using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour, IInteractable
{
    public bool FireOn
    {
        get
        {
            return fire.gameObject.activeSelf;
        }
        set
        {
            if (fire != null) fire.gameObject.SetActive(value);
        }
    }
    public Kettle Kettle;
    [SerializeField] float kettleCookTime;
    [SerializeField] ParticleSystem fire;

    private Vector3 kettlePos;
    private Quaternion kettleRot;

    private float timer;

    private void Start()
    {
        if (Kettle != null)
        {
            kettlePos = Kettle.transform.position;
            kettleRot = Kettle.transform.rotation;
        }
        FireOn = false;
    }

    private void Update()
    {
        if (FireOn && Kettle != null && Kettle.CurrentVolume > 0 && !Kettle.Cooked)
        {
            if (timer < kettleCookTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Kettle.Cooked = true;
            }
        }
    }

    public void PlaceKettle(Kettle kettle)
    {
        this.Kettle = kettle;
        timer = 0;
        Kettle.transform.position = kettlePos;
        Kettle.transform.rotation = kettleRot;
    }

    public void Interact()
    {
        if (Kettle == null && Player.Instance.ActionBar.KettleInvItem != null)
        {
            PlaceKettle(Instantiate(Player.Instance.ActionBar.KettleInvItem.Item.Prefab, Vector3.zero, Quaternion.identity).GetComponent<Kettle>());
            if (Kettle != null)
            {
                Kettle.CurrentVolume = Player.Instance.ActionBar.KettleInvItem.Volume;
                Player.Instance.ActionBar.RemoveKettle();
            }
        }
    }

    public string InteractionLabel()
    {
        if (Kettle == null && Player.Instance.ActionBar.KettleInvItem != null)
        {
            return "Place Kettle on Stove";
        }

        else return string.Empty;
    }
}
