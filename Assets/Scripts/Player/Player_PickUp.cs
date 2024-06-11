using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PickUp : MonoBehaviour
{
    Player player;
    [SerializeField] float maxDistance = 2.5f;

    PickUpObject pickUpObject;

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
        Ray ray = player.Camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        Debug.DrawRay(ray.origin, ray.direction * maxDistance);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            PickUpObject obj = hit.collider.GetComponentInParent<PickUpObject>();
            if (obj != null)
            {
                pickUpObject = obj;
            }
            else if (pickUpObject != null)
            {
                pickUpObject = null;
            }
        }
        else if (pickUpObject != null)
        {
            pickUpObject = null;
        }
    }

    private void OnInteraction()
    {
        if (pickUpObject != null)
        {
            pickUpObject.PickUp();
        }
    }
}
