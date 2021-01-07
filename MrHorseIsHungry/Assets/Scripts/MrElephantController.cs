﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MrElephantController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer breadSpriteRenderer;
    [SerializeField] Sprite breadSpriteBites0;
    [SerializeField] Sprite breadSpriteBites1;
    [SerializeField] Sprite breadSpriteBites2;
    [SerializeField] Sprite breadSpriteBites3;
    [SerializeField] GameObject breadColliderBites0;
    [SerializeField] GameObject breadColliderBites1;
    [SerializeField] GameObject breadColliderBites2;
    [SerializeField] GameObject breadColliderBites3; 
    [SerializeField] MrElephantBubblesController bubblesController;

    Animator animator;

    public int breadNumBites;

    [SerializeField] string state;

    void Start()
    {
        animator = GetComponent<Animator>();
        breadNumBites = 0;
        DeactivateBreadColliders();
        RenderBread();
        state = "idle";
    }

    public void BreadBitten(){
        breadNumBites ++;
        RenderBread();
        Idle();
    }

    public void Idle()
    {
        state = "idle";
        animator.SetBool("idle", true);
    }

    void RenderBread()
    {
        DeactivateBreadColliders();

        switch (breadNumBites)
        {
            case 0:
                breadSpriteRenderer.sprite = breadSpriteBites0;
                breadColliderBites0.SetActive(true);
                break;
            case 1:
                breadSpriteRenderer.sprite = breadSpriteBites1;
                breadColliderBites1.SetActive(true);
                break;
            case 2:
                breadSpriteRenderer.sprite = breadSpriteBites2;
                breadColliderBites2.SetActive(true);
                break;
            case 3:
                breadSpriteRenderer.sprite = breadSpriteBites3;
                breadColliderBites3.SetActive(true);
                break;
            case 4:
                breadSpriteRenderer.sprite = null;
                break;
            default:
                throw new ArgumentException("Invalid breadNumBites: " + breadNumBites);
        }
    }

    void DeactivateBreadColliders(){
        breadColliderBites0.SetActive(false);
        breadColliderBites1.SetActive(false);
        breadColliderBites2.SetActive(false);
        breadColliderBites3.SetActive(false);
    }

    public void Talk()
    {
        animator.SetTrigger("talking");
    }

    public void OfferBread()
    {
        animator.SetBool("idle", false);
        animator.SetTrigger("offeringBread");
        state = "offeringBread";
    }

    public bool IsIdle()
    {
        return state == "idle";
    }

    public void ContinueTalking()
    {
        print("Continue Talking");

        if(IsIdle())
        {
            bubblesController.NextStep();
        }
    }

}