using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MsGiraffeController : MonoBehaviour
{
    [SerializeField] MsGiraffeBubblesController bubblesController;
    Animator animator;
    [SerializeField] Sprite applesCest5;
    [SerializeField] Sprite applesCest4;
    [SerializeField] Sprite applesCest3;
    [SerializeField] Sprite applesCest2;
    [SerializeField] Sprite applesCest1;
    [SerializeField] SpriteRenderer applesCest;

    [SerializeField] SpriteRenderer apple;
    [SerializeField] Sprite appleBites0;
    [SerializeField] Sprite appleBites1;
    [SerializeField] Sprite appleBites2;



    [SerializeField] public int numOfApplesInCest;
    [SerializeField] int numOfBites;

    [SerializeField] string state;

    [SerializeField] ParticleSystem particlesBiteApple;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        bubblesController = GetComponent<MsGiraffeBubblesController>();
        numOfApplesInCest = 5;
        numOfBites = 0;
        RenderApplesInCest();
        Idle();
    }


    public void OfferApple()
    {
        print("MsGiraffe state: offeringApple");
        animator.SetBool("idle", false);
        animator.SetTrigger("offeringApple");
        state = "offeringApple";
    }

    public void TakeAppleFromCest()
    {
        print("TakeAppleFromCest");

        if(numOfApplesInCest > 0)
        {
            numOfApplesInCest -= 1;
            RenderApplesInCest();

            numOfBites = 0;
            RenderApple();
        }
    }

    void RenderApplesInCest(){
        print("RenderApplesInCest");

        switch (numOfApplesInCest)
        {
            case 5:
                applesCest.sprite = applesCest5;
                break;
            case 4:
                applesCest.sprite = applesCest4;
                break;
            case 3:
                applesCest.sprite = applesCest3;
                break;
            case 2:
                applesCest.sprite = applesCest2;
                break;
            case 1:
                applesCest.sprite = applesCest1;
                break;
            case 0:
                applesCest.sprite = null;
                break;
            default:
                throw new ArgumentException("Wrong number of apples: " + numOfApplesInCest);
        }
    }

    void RenderApple(){
        print("RenderApple");

        switch (numOfBites)
        {
            case 0:
                apple.sprite = appleBites0;
                break;
            case 1:
                apple.sprite = appleBites1;
                break;
            case 2:
                apple.sprite = appleBites2;
                break;
            default:
                throw new ArgumentException("Wrong number of bites: " + numOfBites);
        }
    }

    public void AppleBitten()
    {
        print("AppleBitten");

        Instantiate(particlesBiteApple, apple.transform.position, Quaternion.identity);
        
        numOfBites += 1;

        if(numOfBites < 3)
        {
            RenderApple();
        } else if(numOfBites == 3)
        {
            print("Ms Giraffe Apple finished");
            apple.sprite = null;
            bubblesController.NextStep();
            Invoke("Idle", 0.5f);
        } else 
        {
            print("Error: wrong number of numOfBites: " + numOfBites);
        }
    }

    public void Idle()
    {
        print("MsGiraffe state: idle");
        state = "idle";
        animator.SetBool("idle", true);
    }

    public void Talk()
    {
        animator.SetTrigger("talking");
    }

    bool IsIdle()
    {
        return state == "idle";
    }

    bool SomeAppleMissingInCest()
    {
        return numOfApplesInCest > 0;
    }

    public void ContinueTalking()
    {
        print("Ms Giraffe Continue Talking");

        if(IsIdle())
        {
            print("Ms Giraffe Continue Talking... NextStep");
            bubblesController.NextStep();
        }
    }

}
