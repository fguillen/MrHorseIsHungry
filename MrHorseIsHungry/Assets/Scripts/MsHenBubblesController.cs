using System.Collections;
using System;
using UnityEngine;

public class MsHenBubblesController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] BubbleController bubble01Hello;
    [SerializeField] BubbleController bubble02NiceMorning;
    [SerializeField] BubbleController bubble03YouAreHungry;
    [SerializeField] BubbleController bubble04WaitASecond;
    [SerializeField] BubbleController bubble05TakeThisEgg;
    [SerializeField] BubbleController bubble06NoMoreEgg;

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
        if(bubbleActive && !waitingForAutomaticNextStep && Input.GetButtonDown("Jump"))
        {
            print("MsHenBubblesController Jump");
            NextStep();
        }
    }

    public void NextStep()
    {
        step ++;

        print("MsHen step: " + step);

        waitingForAutomaticNextStep = false;

        switch (step)
        {
            case 1:
                ObjectsReferrer.instance.virtualCameraController.TargetMsHen();
                bubble01Hello.Appear();
                ActivateBubble();
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 2:
                bubble01Hello.Disappear();
                waitingForAutomaticNextStep = true;
                Invoke("NextStep", 0.5f);
                break;
            case 3:
                bubble02NiceMorning.Appear();
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 4:
                bubble02NiceMorning.Disappear();
                SetBubbleNotActiveNextFrame();
                break;
            case 5:
                bubble03YouAreHungry.Appear();
                ActivateBubble();
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 6:
                bubble03YouAreHungry.Disappear();
                waitingForAutomaticNextStep = true;
                Invoke("NextStep", 0.5f);
                break;
            case 7:
                bubble04WaitASecond.Appear();
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 8:
                bubble04WaitASecond.Disappear();
                SetBubbleNotActiveNextFrame();
                Invoke("NextStep", 0.5f);
                break;

            // Offering Egg
            case 9:
                ObjectsReferrer.instance.msHenController.OfferEgg();
                break;
            case 10:
                bubble05TakeThisEgg.Appear();
                ActivateBubble();
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 11:
                bubble05TakeThisEgg.Disappear();
                SetBubbleNotActiveNextFrame();
                break;
            
            
            // Block repeated infintely :: INI
            case 12: // No More Bead
                ObjectsReferrer.instance.virtualCameraController.TargetMrHorse();
                bubble06NoMoreEgg.Appear();
                ActivateBubble();
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 13:
                bubble06NoMoreEgg.Disappear();
                SetBubbleNotActiveNextFrame();
                step = 11;
                break;
            // Block repeated infintely :: END
 
            default:
                throw new ArgumentException("step not valid: " + step);
        }
    }
    void ActivateBubble()
    {
        print("bubbleActive: true");
        bubbleActive = true;
    }

    void SetBubbleNotActiveNextFrame()
    {
        StartCoroutine(SetBubbleNotActiveNextFrameCoroutine());
    }

    IEnumerator SetBubbleNotActiveNextFrameCoroutine()
    {
        yield return null;
        print("bubbleActive: false");
        bubbleActive = false;
    }
}
