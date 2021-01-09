using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MrElephantBubblesController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] BubbleController bubble01GoodMorning;
    [SerializeField] BubbleController bubble02WonderfulDay;
    [SerializeField] BubbleController bubble03VigorousAppetite;
    [SerializeField] BubbleController bubble04SomeBread;
    [SerializeField] BubbleController bubble05MoreBread;
    [SerializeField] BubbleController bubble06NoMoreBread;

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
            print("MrElephantBubblesController Jump");
            NextStep();
        }
    }

    public void NextStep()
    {
        step ++;

        print("Mr Elephant step: " + step);

        switch (step)
        {
            case 1:
                bubble01GoodMorning.Appear();
                bubbleActive = true;
                ObjectsReferrer.instance.mrElephantController.Talk();
                break;
            case 2:
                bubble01GoodMorning.Disappear();
                Invoke("NextStep", 0.5f);
                break;
            case 3:
                bubble02WonderfulDay.Appear();
                ObjectsReferrer.instance.mrElephantController.Talk();
                break;
            case 4:
                bubble02WonderfulDay.Disappear();
                SetBubbleNotActiveNextFrame();
                break;
            case 5:
                bubble03VigorousAppetite.Appear();
                bubbleActive = true;
                ObjectsReferrer.instance.mrElephantController.Talk();
                break;
            case 6:
                bubble03VigorousAppetite.Disappear();
                Invoke("NextStep", 0.5f);
                break;
            case 7:
                bubble04SomeBread.Appear();
                ObjectsReferrer.instance.mrElephantController.Talk();
                ObjectsReferrer.instance.mrElephantController.OfferBread();
                break;
            case 8:
                bubble04SomeBread.Disappear();
                SetBubbleNotActiveNextFrame();
                break;

            // Repeat until no more bread :: INI
            case 9:
                bubble05MoreBread.Appear();
                bubbleActive = true;
                ObjectsReferrer.instance.mrElephantController.Talk();
                ObjectsReferrer.instance.mrElephantController.OfferBread();
                break;
            case 10:
                bubble05MoreBread.Disappear();
                SetBubbleNotActiveNextFrame();
                break;
            case 11:
                if(ObjectsReferrer.instance.mrElephantController.breadNumBites < 4)
                {
                    step = 8;
                    NextStep();
                }
                break;
            // Repeat until no more bread :: END
            
            
            // Block repeated infintely :: INI
            case 12: // No More Bead
                bubble06NoMoreBread.Appear();
                bubbleActive = true;
                ObjectsReferrer.instance.mrElephantController.Talk();
                break;
            case 13:
                bubble06NoMoreBread.Disappear();
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
