using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonsController : MonoBehaviour
{
    public void LoadSceneGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadSceneMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShowCredits()
    {
        print("ShowCredits");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
