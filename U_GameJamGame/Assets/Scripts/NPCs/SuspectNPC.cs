using System;
using System.Collections.Generic;
using UnityEngine;

public class SuspectNPC : Interactable
{
    private Transform _playerTransform;

    [Header("Dialogues")] public List<DialogueStruct> dialogueNPC;
    public List<DialogueStruct> guiltyDialogue; 
    private string startDialogueUID="00";
    public string StartDialogueUID =>  startDialogueUID;
    private void Start()
    {
        startDialogueUID = dialogueNPC[0].UID;
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
        GameManager.Instance.EnterOnDialogue(this);
    }
}