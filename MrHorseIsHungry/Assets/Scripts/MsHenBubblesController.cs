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

    public int step;

    void Start()
    {
        step = 0;
        SetBubbleNotActiveNextFrame();
    }

    void Update()
    {
        if(bubbleActive && Input.GetButtonDown("Jump"))
        {
            print("MsHenBubblesController Jump");
            NextStep();
        }
    }

    public void NextStep()
    {
        step ++;

        switch (step)
        {
            case 1:
                bubble01Hello.Appear();
                bubbleActive = true;
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 2:
                bubble01Hello.Disappear();
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
                bubbleActive = true;
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 6:
                bubble03YouAreHungry.Disappear();
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
                bubbleActive = true;
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 11:
                bubble05TakeThisEgg.Disappear();
                SetBubbleNotActiveNextFrame();
                break;
            
            
            // Block repeated infintely :: INI
            case 12: // No More Bead
                bubble06NoMoreEgg.Appear();
                bubbleActive = true;
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
