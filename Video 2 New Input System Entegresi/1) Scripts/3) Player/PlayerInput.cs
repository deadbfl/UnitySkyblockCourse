using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 moveInput;
    public Vector2 mouseInput;

    public void OnMovement(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
        }
    }

    public void OnMouse(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            mouseInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            mouseInput = Vector2.zero;
        }
    }
}
