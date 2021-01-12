using System.Collections;
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
    [SerializeField] ParticleSystem particlesBiteBread;
    Animator animator;
    public int breadNumBites;

    [SerializeField] string state;

    [SerializeField] AudioClip[] clipsBreadBite;
    AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        breadNumBites = 0;
        DeactivateBreadColliders();
        RenderBread();
        state = "idle";
    }

    public void BreadBitten(Collider2D collider2D){
        breadNumBites ++;
        audioSource.PlayOneShot(clipsBreadBite[UnityEngine.Random.Range(0, clipsBreadBite.Length)]);
        Instantiate(particlesBiteBread, collider2D.transform.position, Quaternion.identity);
        RenderBread();
        Invoke("Idle", 0.2f);
    }

    public void Idle()
    {
        state = "idle";
        animator.SetBool("idle", true);
        DeactivateBreadColliders();
    }

    void RenderBread()
    {
        DeactivateBreadColliders();

        switch (breadNumBites)
        {
            case 0:
                breadSpriteRenderer.sprite = breadSpriteBites0;
                break;
            case 1:
                breadSpriteRenderer.sprite = breadSpriteBites1;
                break;
            case 2:
                breadSpriteRenderer.sprite = breadSpriteBites2;
                break;
            case 3:
                breadSpriteRenderer.sprite = breadSpriteBites3;
                break;
            case 4:
                breadSpriteRenderer.sprite = null;
                break;
            default:
                throw new ArgumentException("Invalid breadNumBites: " + breadNumBites);
        }
    }

    void ActivateBreadCollider()
    {
        switch (breadNumBites)
        {
            case 0:
                breadColliderBites0.SetActive(true);
                break;
            case 1:
                breadColliderBites1.SetActive(true);
                break;
            case 2:
                breadColliderBites2.SetActive(true);
                break;
            case 3:
                breadColliderBites3.SetActive(true);
                break;
            default:
                throw new ArgumentException("Invalid breadNumBites in ActivateBreadCollider: " + breadNumBites);
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
        ActivateBreadCollider();
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
            print("Continue Talking... NextStep");
            bubblesController.NextStep();
        }
    }

}
