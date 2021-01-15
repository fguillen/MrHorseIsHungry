using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonsController : MonoBehaviour
{
    public void Play()
    {
        LevelLoaderController.instance.LoadGame();
    }

    public void Menu()
    {
        LevelLoaderController.instance.LoadMenu();
    }

    public void Credits()
    {
        print("ShowCredits");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
