using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerBody;
    private Camera _playerCamera;

    
    [Header("Look")] public float lookSensitivity = 100f;
    private InputAction _lookAction;
    private float _rotationX = 0f;
    public bool canLook = true;

    private void OnEnable()
    {
        BindEvent();
        BindAction();
    }

    void BindEvent()
    {
        GameManager.Instance.OnDialogueStarted += DisableLook;
        GameManager.Instance.OnDialogueFinished += EnableLook;

        GameManager.Instance.OnGameStarted += EnableLook;
    }

    private void OnDisable()
    {
        
        GameManager.Instance.OnDialogueStarted -= DisableLook;
        GameManager.Instance.OnDialogueFinished -= EnableLook;

        GameManager.Instance.OnGameStarted -= EnableLook;
    }

    void BindAction()
    {
        _lookAction = InputSystem.actions.FindAction("Look");
    }

    void EnableLook(object sender, EventArgs args)
    {
        canLook = true;
    }

    void DisableLook(object sender, EventArgs args)
    {
        canLook = false;
    }
    
    
    private void Update()
    {
        if (!canLook) return;
        Vector2 lookValue = _lookAction.ReadValue<Vector2>();
        float mouseX = lookValue.x * lookSensitivity * Time.deltaTime;
        float mouseY = lookValue.y * lookSensitivity * Time.deltaTime;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}