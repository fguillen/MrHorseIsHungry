using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;

public class MsGiraffeBubblesController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] BubbleController bubble01GoodMorning;
    [SerializeField] BubbleController bubble02HowAreYou;
    [SerializeField] BubbleController bubble03ISeeYouAreHungry;
    [SerializeField] BubbleController bubble04DoYouWantAnApple;
    [SerializeField] BubbleController bubble05IseeYouAreVeryHungry;
    [SerializeField] BubbleController bubble06DoYouWantAnotherApple;
    [SerializeField] BubbleController bubble07NoMoreApples;

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
        if(bubbleActive && Input.GetButtonDown("Jump"))
        {
            print("MsGiraffeBubblesController Jump");
            NextStep();
        }
    }

    public void NextStep()
    {
        step ++;

        print("Ms Giraffe step: " + step);

        waitingForAutomaticNextStep = false;

        switch (step)
        {
            case 1:
                ObjectsReferrer.instance.virtualCameraController.TargetMsGiraffe();
                bubble01GoodMorning.Appear();
                bubbleActive = true;
                ObjectsReferrer.instance.msGiraffeController.Talk();
                break;
            case 2:
                bubble01GoodMorning.Disappear();
                waitingForAutomaticNextStep = true;
                Invoke("NextStep", 0.5f);
                break;
            case 3:
                bubble02HowAreYou.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                break;
            case 4:
                bubble02HowAreYou.Disappear();
                SetBubbleNotActiveNextFrame();
                break;
            case 5:
                bubble03ISeeYouAreHungry.Appear();
                bubbleActive = true;
                ObjectsReferrer.instance.msGiraffeController.Talk();
                break;
            case 6:
                bubble03ISeeYouAreHungry.Disappear();
                waitingForAutomaticNextStep = true;
                Invoke("NextStep", 0.5f);
                break;
            case 7:
                bubble04DoYouWantAnApple.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                ObjectsReferrer.instance.msGiraffeController.OfferApple();
                break;
            case 8:
                bubble04DoYouWantAnApple.Disappear();
                SetBubbleNotActiveNextFrame();
                break;
            case 9:
                bubble05IseeYouAreVeryHungry.Appear();
                bubbleActive = true;
                ObjectsReferrer.instance.msGiraffeController.Talk();
                break;
            case 10:
                bubble05IseeYouAreVeryHungry.Disappear();
                waitingForAutomaticNextStep = true;
                Invoke("NextStep", 0.5f);
                break;
            
            // Block repeated until no more Apples :: INI
            case 11: // Apple offering
                bubble06DoYouWantAnotherApple.Appear();
                bubbleActive = true;
                ObjectsReferrer.instance.msGiraffeController.Talk();
                ObjectsReferrer.instance.msGiraffeController.OfferApple();
                break;
            case 12:
                bubble06DoYouWantAnotherApple.Disappear();
                SetBubbleNotActiveNextFrame();
                break;
            case 13:
                if(ObjectsReferrer.instance.msGiraffeController.numOfApplesInCest > 0)
                {
                    step = 10;
                }
                break;
            // Block repeated until no more Apples :: END

            // Block repeated infintely :: INI
            case 14: // No More Apples
                bubble07NoMoreApples.Appear();
                bubbleActive = true;
                ObjectsReferrer.instance.msGiraffeController.Talk();
                ObjectsReferrer.instance.virtualCameraController.TargetMrHorse();
                break;
            case 15:
                bubble07NoMoreApples.Disappear();
                SetBubbleNotActiveNextFrame();
                step = 13;
                break;
            // Block repeated infintely :: END
 
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
