using System.Collections;
using System.Collections.Generic;
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
    bool isShown;
    void Start()
    {
        animator = GetComponent<Animator>();       
        textUI = GetComponentInChildren<TextMeshProUGUI>();

        originalText = textUI.text;

        writtingTimeSeconds = originalText.Length / (float)charactersPerSecond;

        print("Text: " + textUI.text + ", writtingTimeSeconds: " +  writtingTimeSeconds);
    }

    void Update()
    {
        if(isShown && !finished)
        {
            UpdateText();
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
            finished = true;
        }

    }

    public void Appear()
    {
        animator.SetTrigger("appear");
        isShown = true;
        finished = false;
        startWrittingAt = Time.time;
    }

    public void Disappear()
    {
        animator.SetTrigger("disappear");
        isShown = false;
    }

}
