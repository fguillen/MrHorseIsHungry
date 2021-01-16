using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    string sceneToLoad;

    public static LevelLoaderController instance;

    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public void LoadGame()
    {
        print("(LevelLoadController) LoadGame");
        LoadScene("Game");
    }

    public void LoadMenu()
    {
        LoadScene("Menu");
    }

    void LoadScene(string sceneName)
    {
        print("(LevelLoadController) LoadScene(): " + sceneName);
        animator.SetTrigger("fadein");
        sceneToLoad = sceneName;        
    }

    public void FinishFadeinAnimation()
    {
        print("(LevelLoadController) FinishFadeinAnimation");
        SceneManager.LoadScene(sceneToLoad);
    }
}
