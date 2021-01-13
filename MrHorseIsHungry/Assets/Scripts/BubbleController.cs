using System;
using System.Linq;
using UnityEngine;
using TMPro;

enum BubbleState {
    hidden = 1,
    fadeInIni = 2,
    fadeInEnd = 3,
    writeIni = 4,
    writeEnd = 5,
    fadeOutIni = 6,
    fadeOutEnd = 7
}

public class BubbleController : MonoBehaviour
{
    Animator animator;
    TextMeshProUGUI textUI;
    string originalText;
    [SerializeField] int charactersPerSecond;
    float writtingTimeSeconds;
    float startWrittingAt;

    Action callback;

    [SerializeField] AudioClip[] talkingEffects;
    int lastTalkingEffectIndex;
    AudioSource audioSource;
    BubbleState state;
    void Start()
    {
        animator = GetComponent<Animator>();       
        textUI = GetComponentInChildren<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();

        originalText = textUI.text;
        writtingTimeSeconds = originalText.Length / (float)charactersPerSecond;
        state = BubbleState.hidden;
    }

    void Update()
    {
        // if(isShown)
        // {
        //     print("break point");
        // }

        if(
            state == BubbleState.fadeInEnd ||
            state == BubbleState.writeIni
        )
        {
            UpdateText();
        }

        if(
            state == BubbleState.writeEnd &&
            Input.GetButtonDown("Jump")
        )
        {
            Disappear();
        }

        if(
            state == BubbleState.writeIni && 
            Input.GetButtonDown("Jump")
        )
        {
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
        print("(BubbleController) Appear: " + gameObject.name);
        print("(BubbleController) state: " + state);
        
        if(state != BubbleState.hidden)
        {
            print("ERROR: Bubble is not in hidden state, state: " + state);
            return;
        }

        textUI.text = ""; // Set the text to empty during fadeIn
        this.callback = callback;
        animator.SetTrigger("appear");
        state = BubbleState.fadeInIni;
        startWrittingAt = Time.time;
    }

    public void Disappear()
    {
        print("(BubbleController) Disappear: " + gameObject.name);
        animator.SetTrigger("disappear");
        state = BubbleState.fadeOutIni;
        FinishWritting();
    }

    public void FinishWritting()
    {
        print("(BubbleController) FinishWritting: " + gameObject.name);
        textUI.text = originalText;
        state = BubbleState.writeEnd;
        audioSource.Stop();
    }

    void AnimatorEventAppearFinished()
    {
        state = BubbleState.fadeInEnd;
        StartWritting();
    }

    void StartWritting()
    {
        print("(BubbleController) StartWritting: " + gameObject.name);
        print("(BubbleController) state: " + state);
        PlayRandomTalkingEffect();
        state = BubbleState.writeIni;
    }

    void AnimatorEventDisappearFinished()
    {
        callback?.Invoke();
        state = BubbleState.fadeOutEnd; // I know this is not needed but I keep it for consistency
        state = BubbleState.hidden;
    }

    void PlayRandomTalkingEffect()
    {
        // Not Play if not talking effects
        if(talkingEffects.Length == 0)
        {
            print("WARNING: there is not talkingEffectClips on this Bubble '" + originalText + "'");
            return;
        }

        audioSource.Stop(); // Be sure there is not any previous effect running

        // We choose one that is not the actual one
        if(talkingEffects.Length == 1)
        {
            audioSource.clip = talkingEffects[0];
        } else {
            var clipsNotActual = talkingEffects.Where(e => audioSource.clip != e);
            audioSource.clip  = clipsNotActual.ElementAt(UnityEngine.Random.Range(0, clipsNotActual.Count()));
        }

        print("(BubbleController) Bubble takingEffectClip: " + gameObject.name);

        audioSource.Play();   
    }

    public bool IsActive()
    {
        return state != BubbleState.hidden;
    }

}
