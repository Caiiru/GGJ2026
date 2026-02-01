using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SuspectNPC : Interactable
{
    private SuspectNPC _guiltyNPC;
    public Sprite guiltySprite;
    public string startDialogueUid = "A01";
    private Transform _playerTransform;

    [Header("Dialogues")] public List<DialogueStruct> dialogueNPC;

    public List<DialogueBasedOnGuilty> dialogues;


    private void Start()
    {
        _playerTransform = GameManager.Instance.GetPlayerTransform();
    }

    public void InitializeNPC()
    {
        _guiltyNPC = GameManager.Instance.assassinNPC;
        SetDialogue();
    }


    void SetDialogue()
    {
        foreach (DialogueBasedOnGuilty dialogue in dialogues)
        {
            if (dialogue.guiltyNPC == this._guiltyNPC)
            {
                dialogueNPC = dialogue.dialogue;
                startDialogueUid = dialogue.dialogue[0].UID;
                break;
            }
        }
        //SET SPRITE
        if (_guiltyNPC == this)
        {
            guiltySprite = _guiltyNPC.guiltySprite;
        }

        startDialogueUid = "A01";
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