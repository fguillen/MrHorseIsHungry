using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
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

    List<BubbleController> bubbleControllers;

    public int step;

    void Start()
    {
        step = 0;
        bubbleControllers = new List<BubbleController>();
        InitializeBubbleControllersList();
    }
    public void NextStep()
    {
        step ++;

        print("MrElephant step: " + step);

        switch (step)
        {
            case 1:
                ObjectsReferrer.instance.virtualCameraController.TargetMrElephant();
                bubble01GoodMorning.Appear(NextStep);
                ObjectsReferrer.instance.mrElephantController.Talk();
                break;
            case 2:
                bubble02WonderfulDay.Appear();
                ObjectsReferrer.instance.mrElephantController.Talk();
                break;
            case 3:
                bubble03VigorousAppetite.Appear(NextStep);
                ObjectsReferrer.instance.mrElephantController.Talk();
                break;
            case 4:
                bubble04SomeBread.Appear();
                ObjectsReferrer.instance.mrElephantController.Talk();
                ObjectsReferrer.instance.mrElephantController.OfferBread();
                break;

            // Repeat until no more bread :: INI
            case 5:
                bubble05MoreBread.Appear();
                ObjectsReferrer.instance.mrElephantController.Talk();
                ObjectsReferrer.instance.mrElephantController.OfferBread();
                break;
            case 6:
                if(ObjectsReferrer.instance.mrElephantController.breadNumBites < 4)
                {
                    step = 4;
                }
                NextStep();
                break;
            // Repeat until no more bread :: END
            
            
            // Block repeated infintely :: INI
            case 7: // No More Bead
                ObjectsReferrer.instance.virtualCameraController.TargetMrHorse();
                bubble06NoMoreBread.Appear();
                ObjectsReferrer.instance.mrElephantController.Talk();
                step = 6;
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
        bubbleControllers.Add(bubble02WonderfulDay);
        bubbleControllers.Add(bubble03VigorousAppetite);
        bubbleControllers.Add(bubble04SomeBread);
        bubbleControllers.Add(bubble05MoreBread);
        bubbleControllers.Add(bubble06NoMoreBread);
    }

    public bool AnyBubbleActive()
    {
        return bubbleControllers.Any(e => e.IsActive());
    }
}
