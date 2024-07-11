using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public PlayerComEvents events;

    private void Awake()
    {
        instance = this;
    }
}


public struct PlayerComEvents
{
    // Movement
    public event Action<Vector2> OnMovement;
    public void Movement(Vector2 input)
    {
        if (OnMovement != null)
        {
            OnMovement.Invoke(input);
        }
    }

    public event Action<Vector2> OnMouse;
    public void Mouse(Vector2 input) => OnMouse?.Invoke(input);

}