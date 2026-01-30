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

    [Header("Debug")] public bool enterDebug;
    public bool leaveDebug;

    private void Start()
    {
        interactText.color = invisibleColor;
    }

    private void Update()
    {
        if (enterDebug)
        {
            enterDebug = false;
            EnterInteractRange();
        }

        if (leaveDebug)
        {
            leaveDebug = false;
            LeaveInteractRange();
        }
    }


    public void EnterInteractRange()
    {
        interactText.DOColor(visibleColor, animationDuration);
    }

    public void LeaveInteractRange()
    {
        interactText.DOColor(invisibleColor, animationDuration);
    }

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