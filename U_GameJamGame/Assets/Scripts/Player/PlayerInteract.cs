using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _interactAction;

    private HudManager _hudManager;

    private Interactable _interactableOnRange;

    private void OnEnable()
    {
        BindInteractAction();
    }


    private void BindInteractAction()
    {
        _playerInput = GetComponent<PlayerInput>();
        _interactAction = _playerInput.GetInputActionAsset().FindAction("Interact");
        _interactAction.Enable();
        _interactAction.started += OnInteractPerformed;
    }

    private void OnDisable()
    {
        _interactAction.started -= OnInteractPerformed;
        _interactAction.Disable();
    }


    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (_interactableOnRange == null) return;

        _interactableOnRange.Interact();
    }

    public void OnInteractRangeEnter(string interactableName, Interactable interactable)
    {
        _hudManager = HudManager.GetInstance();
        _interactableOnRange = interactable;
        _hudManager.EnterInteractRange(interactableName);
    }

    public void OnInteractRangeExit()
    {
        _interactableOnRange = null;
        _hudManager.LeaveInteractRange();
    }
}