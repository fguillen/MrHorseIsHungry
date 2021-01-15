using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonsController : MonoBehaviour
{
    void LoadSceneGame()
    {
        SceneManager.LoadScene("Game");
    }

    void LoadSceneMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void ShowCredits()
    {
        print("ShowCredits");
    }

    void Quit()
    {
        Application.Quit();
    }
}
