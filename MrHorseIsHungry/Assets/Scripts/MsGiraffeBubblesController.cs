using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;

public class MsGiraffeBubblesController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] BubbleController _01GoodMoring;
    [SerializeField] BubbleController _02HowAreYou;
    [SerializeField] BubbleController _03ISeeYouAreHungry;
    [SerializeField] BubbleController _04DoYouWantAnApple;
    [SerializeField] BubbleController _05IseeYouAreVeryHungry;
    [SerializeField] BubbleController _06DoYouWantAnotherApple;
    [SerializeField] BubbleController _07NoMoreApples;

    public int step;

    void Start()
    {
        step = 0;
    }

    void Update()
    {
        if(ObjectsReferrer.instance.msGiraffeController.IsTalking() && Input.GetButtonDown("Jump"))
        {
            NextStep();
        }
    }

    public void NextStep()
    {
        step ++;

        switch (step)
        {
            case 1:
                _01GoodMoring.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                break;
            case 2:
                _01GoodMoring.Disappear();
                Invoke("NextStep", 0.5f);
                break;
            case 3:
                _02HowAreYou.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                break;
            case 4:
                _02HowAreYou.Disappear();
                Invoke("NextStep", 0.5f);
                break;
            case 5:
                ObjectsReferrer.instance.msGiraffeController.Idle();
                break;
            case 6:
                _03ISeeYouAreHungry.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                break;
            case 7:
                _03ISeeYouAreHungry.Disappear();
                Invoke("NextStep", 0.5f);
                break;
            case 8:
                _04DoYouWantAnApple.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                ObjectsReferrer.instance.msGiraffeController.OfferApple();
                break;
            case 9:
                _04DoYouWantAnApple.Disappear();
                break;
            case 10:
                _05IseeYouAreVeryHungry.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                break;
            case 11:
                _05IseeYouAreVeryHungry.Disappear();
                Invoke("NextStep", 0.5f);
                break;
            case 12:
                _06DoYouWantAnotherApple.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                ObjectsReferrer.instance.msGiraffeController.OfferApple();
                break;
            case 13:
                _06DoYouWantAnotherApple.Disappear();
                break;
            case 14:
                _06DoYouWantAnotherApple.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                ObjectsReferrer.instance.msGiraffeController.OfferApple();
                break;
            case 15:
                _06DoYouWantAnotherApple.Disappear();
                break;
            // case 15:
            //     _07NoMoreApples.Appear();
            //     break;
            // case 16:
            //     _07NoMoreApples.Disappear();
            //     break;
            default:
                throw new ArgumentException("step not valid: " + step);
        }
    }
}
