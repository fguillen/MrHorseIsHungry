using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    AudioSource audioSource;
    [SerializeField] AudioClip clipChain;
    [SerializeField] AudioClip clipBang;
    string sceneToLoad;

    public static LevelLoaderController instance;

    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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

    public void LoadScene(string sceneName)
    {
        print("(LevelLoadController) LoadScene(): " + sceneName);
        animator.SetTrigger("fadein");
        sceneToLoad = sceneName;        
    }

    public void FinishFadeinAnimation()
    {
        print("(LevelLoadController) FinishFadeinAnimation");
        if(sceneToLoad == null)
        {
            print("ERROR: sceneToLoad is null");
        } else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void PlayChainSound()
    {
        audioSource.clip = clipChain;
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void PlaySoundBang()
    {
        audioSource.PlayOneShot(clipBang);
    }
}
