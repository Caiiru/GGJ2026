using System;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gameMenu;

    private void Awake()
    {
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
    }
}
