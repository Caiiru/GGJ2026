using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerGo;


    public event EventHandler OnDialogueStarted;
    public event EventHandler OnDialogueFinished;

    void Start()
    {
        if (playerGo == null)
        {
            Debug.LogError("Player not binded");
        }


        Cursor.lockState = CursorLockMode.Locked;
    }

    public Transform GetPlayerTransform()
    {
        return playerGo.transform;
    }

    public void EnterOnDialogue(SuspectNPC npc)
    {
        EnterDialogueEventArgs eventArgs = new EnterDialogueEventArgs
        {
            npc = npc,
            UID = npc.StartDialogueUID
        };

        OnDialogueStarted?.Invoke(this,(EnterDialogueEventArgs)eventArgs);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ExitOnDialogue()
    {
        OnDialogueFinished?.Invoke(this, EventArgs.Empty);
        Cursor.lockState = CursorLockMode.Locked;
    }

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