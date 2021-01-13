using System.Collections.Generic;
using System;
using System.Linq;
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

        print("MsHen step: " + step);

        switch (step)
        {
            case 1:
                ObjectsReferrer.instance.virtualCameraController.TargetMsHen();
                bubble01Hello.Appear(NextStep);
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 2:
                bubble02NiceMorning.Appear();
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 3:
                bubble03YouAreHungry.Appear(NextStep);
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 4:
                bubble04WaitASecond.Appear(NextStep);
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 5:
                Invoke("NextStep", 0.5f);
                break;

            // Offering Egg
            case 6:
                ObjectsReferrer.instance.msHenController.OfferEgg();
                break;
            case 7:
                bubble05TakeThisEgg.Appear(NextStep);
                ObjectsReferrer.instance.msHenController.Talk();
                break;
            case 8:
                // wait here
                break;
            
            
            // Block repeated infintely :: INI
            case 9: // No More Bead
                ObjectsReferrer.instance.virtualCameraController.TargetMrHorse();
                bubble06NoMoreEgg.Appear();
                ObjectsReferrer.instance.msHenController.Talk();
                step = 8;
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

        bubbleControllers.Add(bubble01Hello);
        bubbleControllers.Add(bubble02NiceMorning);
        bubbleControllers.Add(bubble03YouAreHungry);
        bubbleControllers.Add(bubble04WaitASecond);
        bubbleControllers.Add(bubble05TakeThisEgg);
        bubbleControllers.Add(bubble06NoMoreEgg);
    }

    public bool AnyBubbleActive()
    {
        return bubbleControllers.Any(e => e.IsActive());
    }
}
