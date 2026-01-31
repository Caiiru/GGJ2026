using System;
using UnityEngine;

public class SuspectNPC : Interactable
{
    private Transform _playerTransform;

    // 

    private void Start()
    {
        _playerTransform = GameManager.Instance.GetPlayerTransform();
    }

    public void Update()
    {
        transform.LookAt(_playerTransform);
    }

    public override void Interact()
    {
        base.Interact();
        // HudManager.GetInstance().OpenDialogue();
        GameManager.Instance.EnterOnDialogue();
    }
}