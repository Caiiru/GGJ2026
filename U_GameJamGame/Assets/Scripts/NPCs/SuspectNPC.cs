using System;
using System.Collections.Generic;
using UnityEngine;

public class SuspectNPC : Interactable
{
    private Transform _playerTransform;

    [Header("Dialogues")] public List<DialogueStruct> dialogueNPC;

    private SuspectNPC guiltyNPC;
    public List<DialogueBasedOnGuilty> dialogues;


    public string startDialogueUID = "A01"; 

    private void Start()
    {
        _playerTransform = GameManager.Instance.GetPlayerTransform();
        BindEvents();
    }

    void BindEvents()
    {
        GameManager.Instance.OnAssassinWasChosen += SetDialogue;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnAssassinWasChosen -= SetDialogue;
    }

    void SetDialogue(object sender, EventArgs args)
    {
        ChooseAssassinEventArgs a = (ChooseAssassinEventArgs)args;

        guiltyNPC = a.assassinNPC;

        foreach (DialogueBasedOnGuilty dialogue in dialogues)
        {
            if (dialogue.guiltyNPC == this.guiltyNPC)
            {
                dialogueNPC = dialogue.dialogue;
                startDialogueUID = dialogue.dialogue[0].UID;
                break;
            }
        }

        // startDialogueUID = dialogueNPC[0].UID;
        if (startDialogueUID == "00")
        {
            startDialogueUID = "A01";
        }
    }

    public void Update()
    {
        transform.LookAt(new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z));
    }

    public override void Interact()
    {
        base.Interact();
        // HudManager.GetInstance().OpenDialogue();
        GameManager.Instance.EnterOnDialogue(this);
    }
}