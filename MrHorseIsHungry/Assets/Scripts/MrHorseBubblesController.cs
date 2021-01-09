using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MrHorseBubblesController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] BubbleController bubble01ItsNice;
    [SerializeField] BubbleController bubble02NoHungry;
    [SerializeField] BubbleController bubble03End;

    public bool bubbleActive;
    public bool waitingForAutomaticNextStep;

    public int step;

    void Start()
    {
        step = 0;
        SetBubbleNotActiveNextFrame();

        waitingForAutomaticNextStep = false;
        bubbleActive = false;
    }

    void Update()
    {
        if(
            bubbleActive && 
            !waitingForAutomaticNextStep &&
            Input.GetButtonDown("Jump") &&
            step < 4 // After this no more control
        ){
            print("MrHorseBubblesController Jump");
            NextStep();
        }
    }

    public void NextStep()
    {
        step ++;

        waitingForAutomaticNextStep = false;

        switch (step)
        {
            case 1:
                bubble01ItsNice.Appear();
                bubbleActive = true;
                ObjectsReferrer.instance.mrHorseController.Talk();
                break;
            case 2:
                bubble01ItsNice.Disappear();
                waitingForAutomaticNextStep = true;
                Invoke("NextStep", 0.5f);
                break;
            case 3:
                bubble02NoHungry.Appear();
                ObjectsReferrer.instance.mrHorseController.Talk();
                break;
            case 4:
                bubble02NoHungry.Disappear();
                ObjectsReferrer.instance.mrHorseController.Burp();
                SetBubbleNotActiveNextFrame();
                break;
            case 5:
                waitingForAutomaticNextStep = true;
                Invoke("NextStep", 1f);
                break;
            case 6:
                bubble03End.Appear();
                bubbleActive = true;
                ObjectsReferrer.instance.mrHorseController.Talk();
                break;
 
            default:
                throw new ArgumentException("step not valid: " + step);
        }
    }

    void SetBubbleNotActiveNextFrame()
    {
        StartCoroutine(SetBubbleNotActiveNextFrameCoroutine());
    }

    IEnumerator SetBubbleNotActiveNextFrameCoroutine()
    {
        yield return null;
        bubbleActive = false;
    }
}
