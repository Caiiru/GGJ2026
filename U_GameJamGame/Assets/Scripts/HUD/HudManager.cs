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
        GameManager.Instance.OnDialogueStarted += OpenDialogue;
        GameManager.Instance.OnDialogueFinished += CloseDialogue;
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

    public void OpenDialogue(object o, EventArgs e)
    {
        dialogueGo.SetActive(true);
    }

    public void CloseDialogue(object o, EventArgs e)
    {
        dialogueGo.SetActive(false);
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