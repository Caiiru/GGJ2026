using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
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
    public GameObject accuseEntryPrefab;
    public GameObject exitEntryPrefab;
    public GameObject investigateEntryPrefab;

    [Header("Investigate")] public Transform investigateContent;
    public GameObject investigateLayout;
    private List<InvestigateSuspect> suspects;

    [Header("Debug")] public bool enterDebug;
    public bool leaveDebug;

    private void Start()
    {
        interactText.color = invisibleColor;
        dialogueGo.SetActive(false);
        BindObjects();
        BindEvents();
    }

    void BindObjects()
    {
        investigateLayout.SetActive(false);
        suspects = new List<InvestigateSuspect>();
        SuspectNPC guiltyNPC = GameManager.Instance.assassinNPC;
        for (int i = 0; i < investigateContent.childCount; i++)
        {
            var sus = investigateContent.GetChild(i).GetComponent<InvestigateSuspect>();
            if (sus == null) continue;


            suspects.Add(sus);
            if (guiltyNPC == sus.refNPC)
            {
                sus.isGuilty = true;
            }
        }
    }

    private void BindEvents()
    {
        //GameManager.Instance.OnDialogueStarted += OpenDialogue;
        //GameManager.Instance.OnDialogueFinished += CloseDialogue;
        GameManager.Instance.OnLoose += ResetInvestigate;
        GameManager.Instance.OnVictory += ResetInvestigate;
    }


    #region InvestigateRegion

    public void ResetInvestigate(object sender, EventArgs args)
    {
        foreach (var s in suspects)
        {
            s.Reset();
            s.gameObject.SetActive(false);
        }
    }

    public void CleanupInvestigate()
    {
        foreach (var s in suspects)
        {
            s.gameObject.SetActive(false);
        }
    }

    public void InvestigateSuspect(SuspectNPC suspect)
    {
        investigateLayout.SetActive(true);
        foreach (var s in suspects)
        {
            if (s.refNPC != suspect) continue;

            s.transform.gameObject.SetActive(true);
        }
    }

    #endregion

    #region Dialogue System

    public void OpenDialogue()
    {
        dialogueGo.SetActive(true);
    }

    public void CloseDialogue()
    {
        if (dialogueGo == null) return;
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

        Instantiate(investigateEntryPrefab, optionsContent);
        Instantiate(accuseEntryPrefab, optionsContent);
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