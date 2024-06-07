using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;

    [field: SerializeField] public Vector2 Movement { get; private set; }
    [field: SerializeField] public Vector2 Look { get; private set; }

    private void Awake()
    {
        controls = new Controls();
        controls.Player.AddCallbacks(this);
        controls.Player.Enable();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }
}