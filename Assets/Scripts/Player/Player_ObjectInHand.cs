using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ObjectInHand : MonoBehaviour
{
    [SerializeField] Player player;
    [field: SerializeField] public Item ObjectInHand { get; private set; }

    public bool HasObjectInHand => ObjectInHand != null;
    [SerializeField, Tooltip("How far away from the camera")] float SpawnDistance;
    [SerializeField] float maxDistance = 2.5f;

    private void OnEnable()
    {
        if (player == null) player = GetComponent<Player>();
        player.Input.OnActionSlot1Press += OnActionSlot1Press;
        player.Input.OnActionSlot2Press += OnActionSlot2Press;
        player.Input.OnActionSlot3Press += OnActionSlot3Press;
        player.Input.OnActionSlot4Press += OnActionSlot4Press;
        player.Input.OnActionSlot5Press += OnActionSlot5Press;
    }

    private void OnDisable()
    {
        player.Input.OnActionSlot1Press -= OnActionSlot1Press;
        player.Input.OnActionSlot2Press -= OnActionSlot2Press;
        player.Input.OnActionSlot3Press -= OnActionSlot3Press;
        player.Input.OnActionSlot4Press -= OnActionSlot4Press;
        player.Input.OnActionSlot5Press -= OnActionSlot5Press;
    }

    private void Update()
    {
        if (ObjectInHand != null)
        {
            ObjectInHand.transform.position = objectPosition;
        }
    }

    private void OnActionSlot1Press()
    {
        if (player.ActionBar.Slot1 != null && player.ActionBar.Slot1.Item != null)
        {
            if (player.ActionBar.ActiveSlot != 1) player.ActionBar.SetActiveSlot(1);
            else player.ActionBar.SetActiveSlot(-1);

        }
    }

    private void OnActionSlot2Press()
    {
        if (player.ActionBar.Slot2 != null && player.ActionBar.Slot2.Item != null)
        {
            if (player.ActionBar.ActiveSlot != 2) player.ActionBar.SetActiveSlot(2);
            else player.ActionBar.SetActiveSlot(-1);
        }

    }

    private void OnActionSlot3Press()
    {
        if (player.ActionBar.Slot3 != null && player.ActionBar.Slot3.Item != null)
        {
            if (player.ActionBar.ActiveSlot != 3) player.ActionBar.SetActiveSlot(3);
            else player.ActionBar.SetActiveSlot(-1);
        }

    }

    private void OnActionSlot4Press()
    {
        if (player.ActionBar.Slot4 != null && player.ActionBar.Slot4.Item != null)
        {
            if (player.ActionBar.ActiveSlot != 4) player.ActionBar.SetActiveSlot(4);
            else player.ActionBar.SetActiveSlot(-1);
        }

    }
    private void OnActionSlot5Press()
    {

        if (player.ActionBar.Slot5 != null && player.ActionBar.Slot5.Item != null)
        {
            if (player.ActionBar.ActiveSlot != 5) player.ActionBar.SetActiveSlot(5);
            else player.ActionBar.SetActiveSlot(-1);
        }
    }

    public void Despawn()
    {
        if (ObjectInHand != null)
        {
            Destroy(ObjectInHand.gameObject);
            ObjectInHand = null;
            player.Input.OnInteractPress -= OnInteractPressed;
        }
    }

    public void Spawn()
    {
        if (player.ActionBar.ActiveSlot > 0)
        {
            ObjectInHand = Instantiate(player.ActionBar.Slot(player.ActionBar.ActiveSlot).Item.Prefab,
                objectPosition, Quaternion.identity).GetComponent<Item>();
            player.Input.OnInteractPress += OnInteractPressed;
        }
    }

    public void UpdateHand()
    {
        Despawn();
        Spawn();
    }

    public void OnInteractPressed()
    {
        player.ActionBar.ClearActiveSlot();
        player.Input.OnInteractPress -= OnInteractPressed;
        ObjectInHand = null;

    }

    private Vector3 objectPosition
    {
        get
        {
            if (Raycast(out Vector3 position)) return position;
            return player.Camera.transform.position + (player.Camera.transform.forward * SpawnDistance);
        }
    }

    private bool Raycast(out Vector3 hitPoint)
    {
        hitPoint = Vector3.zero;
        Ray ray = player.Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(ray.origin, ray.direction * maxDistance);

        RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance);
        if (hits.Length > 0)
        {
            if (ObjectInHand != null)
            {
                IInteractable interactableObject = ObjectInHand.GetComponent<IInteractable>();
                if (interactableObject != null)
                {
                    UI.LeftInteractIndicator.gameObject.SetActive(true);
                    UI.LeftInteractIndicator.SetLabel("Drop " + ObjectInHand.Config.ItemName);
                }
                else
                {
                    UI.LeftInteractIndicator.gameObject.SetActive(false);
                }
            }

            Vector3 closest = Vector3.zero;
            float distance = maxDistance;
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.GetComponent<Item>() == null && hit.collider.GetComponent<Player>() == null)
                {
                    float hitDistance = Vector3.Distance(transform.position, hit.point);
                    if (hitDistance < distance)
                    {
                        closest = hit.point;
                        distance = hitDistance;
                    }
                }
            }
            hitPoint = closest;
            return closest != Vector3.zero;
        }
        else 
        {
            UI.LeftInteractIndicator.gameObject.SetActive(false);
        }

        return false;
    }
}
