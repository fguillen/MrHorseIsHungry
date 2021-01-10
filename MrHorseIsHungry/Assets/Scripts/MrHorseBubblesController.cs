using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class MrHorseBubblesController : MonoBehaviour
{
    [SerializeField] BubbleController bubble00AMyHouseDirection;
    [SerializeField] BubbleController bubble00BImHungry;
    [SerializeField] BubbleController bubble01ItsNice;
    [SerializeField] BubbleController bubble02NoHungry;
    [SerializeField] BubbleController bubble03End;

    [SerializeField] BubbleController bubbleTutorial01Walk;
    [SerializeField] BubbleController bubbleTutorial02Bite;
    

    List<BubbleController> bubblesControllerActive;

    public bool bubbleActive;
    public bool waitingForAutomaticNextStep;

    public int step;

    void Start()
    {
        step = 0;
        SetBubbleNotActiveNextFrame();

        waitingForAutomaticNextStep = false;
        bubbleActive = false;

        bubblesControllerActive = new List<BubbleController>();
    }

    void Update()
    {
        if(
            !bubblesControllerActive.Any() &&
            bubbleActive && 
            !waitingForAutomaticNextStep &&
            Input.GetButtonDown("Jump") &&
            step < 4 // After this no more control
        ){
            print("MrHorseBubblesController Jump");
            NextStep();
        }

        if(bubblesControllerActive.Any() && Input.GetButtonDown("Jump"))
        {
            HideBubbles();
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

    void ShowBubble(BubbleController bubbleController){
        bubbleController.Appear();
        bubblesControllerActive.Add(bubbleController);
    }

    void HideBubbles()
    {
        foreach (var bubbleController in bubblesControllerActive)
        {
            bubbleController.Disappear();
        }

        bubblesControllerActive.Clear();
    }

    public bool IsBubbleActive()
    {
        return (bubbleActive || bubblesControllerActive.Any());
    }

    public void ShowLeftLimitBubble()
    {
        ShowBubble(bubble00AMyHouseDirection);
    }

    public void ShowImHungryBubble()
    {
        ShowBubble(bubble00BImHungry);
    }

    public void ShowTutorialWalk()
    {
        ShowBubble(bubbleTutorial01Walk);
    }

    public void ShowTutorialBite()
    {
        ShowBubble(bubbleTutorial02Bite);
    }

    
}
