using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        print("Pressed Start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
