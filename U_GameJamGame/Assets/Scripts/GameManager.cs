using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerGo;


    public event EventHandler OnDialogueStarted;
    public event EventHandler OnDialogueFinished;

    public event EventHandler OnVictory;
    public event EventHandler OnLoose;

    public event EventHandler OnGameStarted;
    public event EventHandler OnAssassinWasChosen;

    [Header("Game Loop")] public SuspectNPC assassinNPC;

    public List<SuspectNPC> suspectNpcs;
    [Header("Game Over")] public Image blackScreenFadeout;
    public float blackScreenAnimDuration = 0.75f;
    public Color blackColor;


    //reset

    private Vector3 _playerStartPosition;
    private Quaternion _playerStartRotation;

    void Start()
    {
        BindObjects();
    }

    private void BindObjects()
    {
        if (playerGo == null)
        {
            Debug.LogError("Player not bound");
        }

        _playerStartPosition = playerGo.transform.position;
        _playerStartRotation = playerGo.transform.rotation;

        if (blackScreenFadeout == null)
        {
            Debug.LogError("Black screen fadeout not bound");
        }
    }

    public Transform GetPlayerTransform()
    {
        return playerGo.transform;
    }

    #region Game Loop

    public void InitializeGame()
    {
        ResetGame();
        SelectRandomAssassin();
        blackScreenFadeout.color = new Color(0, 0, 0, 0);
        Cursor.lockState = CursorLockMode.Locked;
        OnGameStarted?.Invoke(this, EventArgs.Empty);
        InitializeSuspects();
    }

    private void SelectRandomAssassin()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag($"Suspects");
        foreach (var npc in npcs)
        {
            suspectNpcs.Add(npc.GetComponent<SuspectNPC>());
        }

        assassinNPC = suspectNpcs[UnityEngine.Random.Range(0, suspectNpcs.Count)];
        ChooseAssassinEventArgs choosenAssassinEventArgs = new ChooseAssassinEventArgs
        {
            GuiltyNPC = assassinNPC
        };
        // OnAssassinWasChosen?.Invoke(this, choosenAssassinEventArgs);
        OnAssassinWasChosen?.Invoke(this, EventArgs.Empty);
    }

    private void InitializeSuspects()
    {
        foreach (var suspect in suspectNpcs)
        {
            suspect.InitializeNPC();
        }
    }


    private void ChangeCameraMainParent(Transform newParent)
    {
        if (Camera.main != null)
            Camera.main.transform.SetParent(newParent);
    }

    public async UniTask AccuseCurrentNPC(SuspectNPC currentNPC)
    {
        await ActivateBlackScreenFadeout();
        ChangeCameraMainParent(this.transform);
        Debug.Log($"Accusing {currentNPC.name}");
        if (currentNPC == assassinNPC)
        {
            OnVictory?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnLoose?.Invoke(this, EventArgs.Empty);
        }

        await UniTask.WaitForSeconds(1);

        ResetGame();
    }

    private async UniTask ActivateBlackScreenFadeout()
    {
        //Debug.Log("Black screen fadeout");
        blackScreenFadeout.DOColor(blackColor, blackScreenAnimDuration).SetEase(Ease.InBounce);

        await UniTask.WaitForSeconds(blackScreenAnimDuration);
    }

    #endregion

    #region Reset Game

    private void ResetGame()
    {
        playerGo.transform.position = _playerStartPosition;
        playerGo.transform.rotation = _playerStartRotation;
    }

    #endregion

    #region Dialogue Options

    public void EnterOnDialogue(SuspectNPC npc)
    {
        EnterDialogueEventArgs eventArgs = new EnterDialogueEventArgs
        {
            npc = npc,
            UID = npc.startDialogueUid
        };
        //Debug.Log($"Enter dialogue: {npc.name},  UID: {npc.startDialogueUid}");

        OnDialogueStarted?.Invoke(this, (EnterDialogueEventArgs)eventArgs);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ExitOnDialogue()
    {
        OnDialogueFinished?.Invoke(this, EventArgs.Empty);
        Cursor.lockState = CursorLockMode.Locked;
    }

    #endregion


    #region Singleton

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    #endregion
}

public class EnterDialogueEventArgs : EventArgs
{
    public SuspectNPC npc;
    public string UID;
}

public class ChooseAssassinEventArgs : EventArgs
{
    public SuspectNPC GuiltyNPC;
}