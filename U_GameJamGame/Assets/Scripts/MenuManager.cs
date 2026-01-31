using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        if (Camera.main == null) return;
        Camera.main.transform.SetParent(transform);
    }

    public void PlayGame()
    {
        if (Camera.main == null) return;
        
        
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Camera.main.transform.SetParent(playerTransform.GetChild(0).transform);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
