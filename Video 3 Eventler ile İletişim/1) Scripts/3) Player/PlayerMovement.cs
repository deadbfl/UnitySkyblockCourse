using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerSystem
{
   
    private void OnEnable()
    {
        // evente abone oldum
        player.events.OnMovement += OnMovement;
        player.events.OnMouse += OnMouse;
    }

    private void OnDisable()
    {
        // eventten abonelikten ciktim
        player.events.OnMovement -= OnMovement;
        player.events.OnMouse -= OnMouse;
    }
    private void OnMovement(Vector2 input)
    {
        print(input);
    }
    private void OnMouse(Vector2 input)
    {
        print(input);
    }
}
