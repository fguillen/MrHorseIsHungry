using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
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

    [SerializeField] List<BubbleController> bubbleControllers;

    public int step;

    void Start()
    {
        step = 0;
        // bubbleControllers = new List<BubbleController>();
        InitializeBubbleControllersList();
    }

    void Update()
    {
    }

    public void NextStep()
    {
        step ++;

        print("Ms Giraffe step: " + step);

        switch (step)
        {
            case 1:
                ObjectsReferrer.instance.virtualCameraController.TargetMsGiraffe();
                bubble01GoodMorning.Appear(NextStep);
                ObjectsReferrer.instance.msGiraffeController.Talk();
                break;
            case 2:
                bubble02HowAreYou.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                break;
            case 3:
                bubble03ISeeYouAreHungry.Appear(NextStep);
                ObjectsReferrer.instance.msGiraffeController.Talk();
                break;
            case 4:
                bubble04DoYouWantAnApple.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                ObjectsReferrer.instance.msGiraffeController.OfferApple();
                break;
            case 5:
                bubble05IseeYouAreVeryHungry.Appear(NextStep);
                ObjectsReferrer.instance.msGiraffeController.Talk();
                break;
            
            // Block repeated until no more Apples :: INI
            case 6: // Apple offering
                bubble06DoYouWantAnotherApple.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                ObjectsReferrer.instance.msGiraffeController.OfferApple();
                break;
            case 7:
                if(ObjectsReferrer.instance.msGiraffeController.numOfApplesInCest > 0)
                {
                    step = 5;
                }
                break;
            // Block repeated until no more Apples :: END

            // Block repeated infintely :: INI
            case 8: // No More Apples
                bubble07NoMoreApples.Appear();
                ObjectsReferrer.instance.msGiraffeController.Talk();
                ObjectsReferrer.instance.virtualCameraController.TargetMrHorse();
                step = 7;
                break;
            // Block repeated infintely :: END
 
            default:
                throw new ArgumentException("step not valid: " + step);
        }
    }

    
    void InitializeBubbleControllersList()
    {        
        // I have tried to use some metaprogramming
        //   to initialize this List but I was not able
        //   I have tried PropertyInfo but didn't work:
        // #
        // var propertyInfos = this.GetType().GetProperties();
        // foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
        // {
        //     bubbleControllers.Add((BubbleController)propertyInfo.GetValue(this, null));
        // }
        // #

        bubbleControllers.Add(bubble01GoodMorning);
        bubbleControllers.Add(bubble02HowAreYou);
        bubbleControllers.Add(bubble03ISeeYouAreHungry);
        bubbleControllers.Add(bubble04DoYouWantAnApple);
        bubbleControllers.Add(bubble05IseeYouAreVeryHungry);
        bubbleControllers.Add(bubble06DoYouWantAnotherApple);
        bubbleControllers.Add(bubble07NoMoreApples);
    }

    public bool AnyBubbleActive()
    {
        return bubbleControllers.Any(e => e.isShown);
    }
}
