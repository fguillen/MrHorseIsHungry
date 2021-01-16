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
        ObjectsReferrer.instance.menuCanvasController.CreditsShow();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CreditsClose()
    {
        ObjectsReferrer.instance.menuCanvasController.CreditsHide();        
    }
}
