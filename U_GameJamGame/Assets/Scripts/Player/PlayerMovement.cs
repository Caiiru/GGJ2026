using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Inputs")] private InputAction _moveAction;
    [SerializeField] InputActionAsset moveActionAsset;
    [SerializeField] private float playerSpeed;

    [SerializeField] private Vector3 playerVelocity;

    private readonly float _gravityValue = -9.81f;

    //References
    private CharacterController _characterController;

    void Start()
    {
        BindActions();
        BindReferences();

        if (playerSpeed <= 0)
        {
            playerSpeed = 15f;
        }
    }

    void BindActions()
    {
        _moveAction = moveActionAsset.FindAction("Move");
    }

    void BindReferences()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 inputValue = _moveAction.ReadValue<Vector2>();
 
        Vector3 move = new Vector3(inputValue.x, 0, inputValue.y);
        move = transform.TransformDirection(move);

        _characterController.Move(move * Time.deltaTime * playerSpeed);
        // _playerVelocity = move;

        //Gravity
        playerVelocity.y = _gravityValue;
        _characterController.Move(playerVelocity * Time.deltaTime);
    }
}