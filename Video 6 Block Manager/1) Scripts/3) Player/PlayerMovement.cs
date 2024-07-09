using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerSystem
{
    [SerializeField] private float speed;
    private Rigidbody rb;
    private Vector2 moveInput;

    protected override void Awake() {
        base.Awake();

        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        // evente abone oldum
        player.events.OnMovement += OnMovement;
    }

    private void OnDisable()
    {
        // eventten abonelikten ciktim
        player.events.OnMovement -= OnMovement;
    }

    private void Update()
    {
        if(moveInput != Vector2.zero)
        {
            Vector3 moveDirection = Vector3.zero;

            moveDirection += transform.right * moveInput.x + transform.forward * moveInput.y;

            rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
        }
    }
    private void OnMovement(Vector2 input)
    {
        moveInput = input;
    }
}
