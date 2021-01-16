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
    [SerializeField] BubbleController bubbleTutorial01Bite;
    [SerializeField] BubbleController bubbleTutorial02Walk;
    
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

        print("MrHorse step: " + step);

        switch (step)
        {
            case 1:
                bubble01ItsNice.Appear(NextStep);
                ObjectsReferrer.instance.mrHorseController.Talk();
                break;
            case 2:
                bubble02NoHungry.Appear(ObjectsReferrer.instance.mrHorseController.EndScene);
                ObjectsReferrer.instance.mrHorseController.Talk();
                break;
            case 3:
                Invoke("NextStep", 1f);
                break;
            case 4:
                bubble03End.indestructible = true;
                bubble03End.Appear();
                ObjectsReferrer.instance.mrHorseController.Talk();
                break;
 
            default:
                throw new ArgumentException("step not valid: " + step);
        }
    }

    void ShowBubble(BubbleController bubbleController, Action callback = null){
        bubbleController.Appear(callback);
    }

    public void ShowLeftLimitBubble()
    {
        ShowBubble(bubble00AMyHouseDirection);
    }

    public void ShowImHungryBubble()
    {
        ShowBubble(bubble00BImHungry);
    }

    public void ShowTutorial()
    {
        ShowBubble(bubbleTutorial01Bite, ShowTutorialWalk);
    }

    public void ShowTutorialWalk()
    {
        ShowBubble(bubbleTutorial02Walk);
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

        bubbleControllers.Add(bubble00AMyHouseDirection);
        bubbleControllers.Add(bubble00BImHungry);
        bubbleControllers.Add(bubble01ItsNice);
        bubbleControllers.Add(bubble02NoHungry);
        bubbleControllers.Add(bubble03End);
        bubbleControllers.Add(bubbleTutorial01Bite);
        bubbleControllers.Add(bubbleTutorial02Walk);
    }

    public bool AnyBubbleActive()
    {
        return bubbleControllers.Any(e => e.IsActive());
    }
}
