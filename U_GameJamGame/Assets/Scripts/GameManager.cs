using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerGo;

    void Start()
    {
        if (playerGo == null)
        {
            Debug.LogError("Player not binded");
        }
    }

    public Transform GetPlayerTransform()
    {
        return playerGo.transform;
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