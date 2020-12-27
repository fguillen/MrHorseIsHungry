using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GiraffeController : MonoBehaviour
{
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



    int numOfApplesInCest;
    int numOfBites;

    string state;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        numOfApplesInCest = 5;
        numOfBites = 0;
        RenderApplesInCest();
        Idle();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsIdle() && SomeAppleMissingInCest() && Input.GetButtonDown("Jump"))
        {
            OfferApple();
        }
    }

    void OfferApple()
    {
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
        numOfBites += 1;

        if(numOfBites < 3)
        {
            RenderApple();
        } else
        {
            Idle();
        }
    }

    void Idle()
    {
        state = "idle";
        animator.SetBool("idle", true);
    }

    bool IsIdle()
    {
        return state == "idle";
    }

    bool SomeAppleMissingInCest()
    {
        return numOfApplesInCest > 0;
    }
}
