using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public string currentDialogue;

    public SuspectNPC currentDialogueNPC;
    private HudManager _hudManager;

    private void Start()
    {
        BindEvent();
    }

    void BindEvent()
    {
        GameManager.Instance.OnDialogueStarted += EnterDialogue;
        GameManager.Instance.OnDialogueFinished += CloseDialogue;
    }


    private async void EnterDialogue(object sender, EventArgs args)
    {
        try
        {
            if (args is EnterDialogueEventArgs eventArgs)
            {
                currentDialogueNPC = eventArgs.npc;
                currentDialogue = eventArgs.UID;
            }


            await PopulateDialogue();

            _hudManager.OpenDialogue();
        }
        catch (Exception e)
        {
                Debug.LogError(e);
        }
    }

    private UniTask PopulateDialogue()
    {
        if (!currentDialogueNPC)
        {
            
            Debug.LogError("Current NPC not finded");
            //UniTask.WaitUntilCanceled()
        }

        _hudManager = HudManager.GetInstance();

        _hudManager.PopulateDialogue(currentDialogueNPC, currentDialogue);

        return UniTask.CompletedTask;
    }

    public void NextDialogue(string nextDialogue)
    {
        currentDialogue = nextDialogue;
        PopulateDialogue();
    }

    private void CloseDialogue(object o, EventArgs args)
    {
        _hudManager.CloseDialogue();
    }

    #region Singleton

    public static DialogueManager Instance;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    #endregion
}