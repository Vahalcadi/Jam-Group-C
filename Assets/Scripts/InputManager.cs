using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private PlayerControls playerControls;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance.gameObject);
        else
            Instance = this;

        playerControls = new PlayerControls();
    }

    public Vector2 GetMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetLookPosition()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool GetJump()
    {
        return playerControls.Player.Jump.triggered;
    }

    public bool GetCrouch()
    {
        return playerControls.Player.Crouch.triggered;
    }

    public bool GetGrabObject()
    {
        return playerControls.Player.GrabObjects.IsPressed();
    }

    public bool GetThrowObject()
    {
        return playerControls.Player.ThrowObjects.triggered;
    }

    public bool GetInteract()
    {
        return playerControls.Player.Interact.triggered;
    }

    public void OnEnable()
    {
        playerControls.Enable();
    }

    public void OnDisable()
    {
        playerControls.Disable();
    }
}
