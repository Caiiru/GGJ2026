using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [Header("Interact Options")] public TextMeshProUGUI interactText;
    public float animationDuration = 0.5f;
    public Color visibleColor;
    public Color invisibleColor;

    [Header("Dialogue")] public GameObject dialogueGo;
    public TextMeshProUGUI dialogueText;
    public Transform optionsContent;
    public GameObject optionEntryPrefab;
    public GameObject acuseEntryPrefab;
    public GameObject exitEntryPrefab;


    [Header("Debug")] public bool enterDebug;
    public bool leaveDebug;

    private void Start()
    {
        interactText.color = invisibleColor;
        dialogueGo.SetActive(false);
        BindEvents();
    }

    private void BindEvents()
    {
        //GameManager.Instance.OnDialogueStarted += OpenDialogue;
        //GameManager.Instance.OnDialogueFinished += CloseDialogue;
    }

    private void Update()
    {
        if (enterDebug)
        {
            enterDebug = false;
            EnterInteractRange("Debug Master Key");
        }

        if (leaveDebug)
        {
            leaveDebug = false;
            LeaveInteractRange();
        }
    }

    #region Dialogue System

    public void OpenDialogue()
    {
        dialogueGo.SetActive(true);
    }

    public void CloseDialogue()
    {
        dialogueGo.SetActive(false);
    }

    private void Cleanup()
    {
        for (int i = 0; i < optionsContent.childCount; i++)
        {
            Destroy(optionsContent.GetChild(i).gameObject);
        }
    }
    public void PopulateDialogue(SuspectNPC currentDialogueNPC, string currentDialogue)
    {
        Cleanup();
        foreach (DialogueStruct dialogue in currentDialogueNPC.dialogueNPC)
        {
            if (dialogue.UID != currentDialogue)
            {
                continue;
            }

            dialogueText.text = dialogue.npcText;
            for (int i = 0; i < dialogue.options.Count; i++)
            {
                var entry = Instantiate(optionEntryPrefab, optionsContent);
                entry.GetComponent<DialogueOptionEntry>().PopulateOption(dialogue.options[i]);
            }
            break;
        }

        Instantiate(acuseEntryPrefab, optionsContent);
        Instantiate(exitEntryPrefab, optionsContent);
    }

    #endregion

    #region Interact Functions

    public void EnterInteractRange(string interactWith)
    {
        interactText.DOColor(visibleColor, animationDuration);
        interactText.text = $"Pressione E para interagir com {interactWith}";
    }

    public void LeaveInteractRange()
    {
        interactText.DOColor(invisibleColor, animationDuration);
    }

    #endregion

    #region Singleton

    private static HudManager _instance;

    public static HudManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        _instance = this;
    }

    #endregion
}