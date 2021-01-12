using System;
using System.Linq;
using UnityEngine;
using TMPro;

public class BubbleController : MonoBehaviour
{
    Animator animator;
    TextMeshProUGUI textUI;
    string originalText;
    [SerializeField] int charactersPerSecond;
    float writtingTimeSeconds;
    float startWrittingAt;

    bool finished;
    public bool isShown;
    Action callback;

    [SerializeField] AudioClip[] talkingEffects;
    int lastTalkingEffectIndex;
    AudioSource audioSource;
    void Start()
    {
        animator = GetComponent<Animator>();       
        textUI = GetComponentInChildren<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();

        originalText = textUI.text;

        writtingTimeSeconds = originalText.Length / (float)charactersPerSecond;

        print("Text: " + textUI.text + ", writtingTimeSeconds: " +  writtingTimeSeconds);
    }

    void Update()
    {
        // if(isShown)
        // {
        //     print("break point");
        // }

        if(isShown && !finished)
        {
            UpdateText();
        }

        if(isShown && finished && Input.GetButtonDown("Jump"))
        {
            Disappear();
        }

        if(isShown && !finished && Input.GetButtonDown("Jump"))
        {
            textUI.text = originalText;
            FinishWritting();
        }
    }

    void UpdateText()
    {
        int lettersCount = originalText.Length;
        float secondsPassed = Time.time - startWrittingAt;

        float percentage = secondsPassed / writtingTimeSeconds;
        int lettersToShowCount = Mathf.CeilToInt(lettersCount * percentage);

        if(lettersToShowCount > lettersCount)
        {
            print("WARNING: lettersToShowCount > lettersCount, lettersToShowCount: " + lettersToShowCount);
            lettersToShowCount = lettersCount;
        }

        string lettersToShow = originalText.Substring(0, lettersToShowCount);
        string finalText = lettersToShow;
        
        if(lettersToShowCount < lettersCount)
        {
            string lettersToHide = originalText.Substring(lettersToShowCount);
            finalText += "<alpha=#00>" + lettersToHide;
        }

        textUI.text = finalText;

        if(lettersToShowCount >= lettersCount)
        {
            FinishWritting();
        }

    }

    public void Appear(Action callback = null)
    {
        this.callback = callback;
        animator.SetTrigger("appear");
        PlayRandomTalkingEffect();
        finished = false;
        isShown = true;
        startWrittingAt = Time.time;
    }

    public void Disappear()
    {
        animator.SetTrigger("disappear");
        FinishWritting();
    }

    public void FinishWritting()
    {
        finished = true;
        audioSource.Stop();
    }

    void AnimatorEventAppearFinished()
    {
        // nothing here yet
    }

    void AnimatorEventDisappearFinished()
    {
        isShown = false;
        callback?.Invoke();
    }

    void PlayRandomTalkingEffect()
    {
        // Not Play if not talking effects
        if(talkingEffects.Length == 0)
        {
            print("WARNING: there is not talkingEffectClips on this Bubble '" + originalText + "'");
            return;
        }

        // We choose one that is not the actual one
        if(talkingEffects.Length == 1)
        {
            audioSource.clip = talkingEffects[0];
        } else {
            var clipsNotActual = talkingEffects.Where(e => audioSource.clip != e);
            audioSource.clip  = clipsNotActual.ElementAt(UnityEngine.Random.Range(0, clipsNotActual.Count()));
        }

        print("Bubble takingEffectClip: " + audioSource.clip.name);

        audioSource.Play();   
    }

}
