using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public string currentDialogue;

    public SuspectNPC currentDialogueNPC;
    [SerializeField] private HudManager _hudManager;

    private void Start()
    {
        BindEvent();

        _hudManager = HudManager.GetInstance();
    }

    void BindEvent()
    {
        GameManager.Instance.OnDialogueStarted += EnterDialogue;
        GameManager.Instance.OnDialogueFinished += CloseDialogue;

        GameManager.Instance.OnGameStarted += CloseDialogue;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnDialogueStarted -= EnterDialogue;
        GameManager.Instance.OnDialogueFinished -= CloseDialogue;

        GameManager.Instance.OnGameStarted -= CloseDialogue;
    }


    private async void EnterDialogue(object sender, EventArgs args)
    {
        if (_hudManager == null) _hudManager = HudManager.GetInstance();
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
        if (_hudManager == null) return;
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