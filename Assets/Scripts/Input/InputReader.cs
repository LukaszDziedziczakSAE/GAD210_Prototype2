using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;

    [field: SerializeField] public Vector2 Movement { get; private set; }
    [field: SerializeField] public Vector2 Look { get; private set; }
    public event Action OnInteractPress;
    public event Action OnActionSlot1Press;
    public event Action OnActionSlot2Press;
    public event Action OnActionSlot3Press;
    public event Action OnActionSlot4Press;
    public event Action OnActionSlot5Press;
    public event Action OnPauseKeyPress;

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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) OnInteractPress?.Invoke();
    }

    public void OnActionSlot1(InputAction.CallbackContext context)
    {
        if (context.performed) OnActionSlot1Press?.Invoke();
    }

    public void OnActionSlot2(InputAction.CallbackContext context)
    {
        if (context.performed) OnActionSlot2Press?.Invoke();
    }

    public void OnActionSlot3(InputAction.CallbackContext context)
    {
        if (context.performed) OnActionSlot3Press?.Invoke();
    }

    public void OnActionSlot4(InputAction.CallbackContext context)
    {
        if (context.performed) OnActionSlot4Press?.Invoke();
    }

    public void OnActionSlot5(InputAction.CallbackContext context)
    {
        if (context.performed) OnActionSlot5Press?.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) OnPauseKeyPress?.Invoke();
    }
}
