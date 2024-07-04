using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector2 limitOfCamera;
    [SerializeField] private float mouseSens;
    private Vector2 mouseInput;
    private Vector3 rotation;

    private void OnEnable()
    {
        Player.instance.events.OnMouse += SetMouseInput;
    }
    private void OnDisable()
    {
        Player.instance.events.OnMouse -= SetMouseInput;
    }

    private void Update()
    {
        rotation.x -= mouseInput.y * mouseSens * Time.deltaTime;

        rotation.x = Mathf.Clamp(rotation.x, limitOfCamera.x, limitOfCamera.y);

        rotation.y += mouseInput.x * mouseSens * Time.deltaTime;

        transform.rotation = Quaternion.Euler(rotation);
        playerBody.rotation = Quaternion.Euler(0,rotation.y, 0);
    }
    private void LateUpdate()
    {
        transform.position = followTarget.position;
    }

    private void SetMouseInput(Vector2 input)
    {
        mouseInput = input;
    }
}
