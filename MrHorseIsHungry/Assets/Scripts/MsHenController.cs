using System;
using UnityEngine;

public class MsHenController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer eggSpriteRenderer;
    [SerializeField] Sprite eggSpriteBites0;
    [SerializeField] Sprite eggSpriteBites1;
    [SerializeField] Sprite eggSpriteBites2;
    [SerializeField] Sprite eggSpriteBites3;
    [SerializeField] Sprite eggSpriteBites4;

    [SerializeField] SpriteRenderer eggFloorSpriteRenderer;

    [SerializeField] Sprite eggFloorSpriteBites1;
    [SerializeField] Sprite eggFloorSpriteBites2;
    [SerializeField] Sprite eggFloorSpriteBites3;
    [SerializeField] Sprite eggFloorSpriteBites4;

    [SerializeField] MsHenBubblesController bubblesController;

    Animator animator;

    public int eggNumBites;

    [SerializeField] string state;

    void Start()
    {
        animator = GetComponent<Animator>();
        eggNumBites = 0;
        RenderEgg();
        state = "idle";
    }

    public void EggBitten(){
        eggNumBites ++;
        RenderEgg();

        if(eggNumBites == 5)
        {
            Invoke("Idle", 0.5f);
        }
    }

    public void Idle()
    {
        print("MsHen Idle");
        state = "idle";
        animator.SetBool("idle", true);
    }

    void RenderEgg()
    {

        switch (eggNumBites)
        {
            case 0:
                eggSpriteRenderer.sprite = eggSpriteBites0;
                eggFloorSpriteRenderer.sprite = null;
                break;
            case 1:
                eggSpriteRenderer.sprite = eggSpriteBites1;
                eggFloorSpriteRenderer.sprite = eggFloorSpriteBites1;
                break;
            case 2:
                eggSpriteRenderer.sprite = eggSpriteBites2;
                eggFloorSpriteRenderer.sprite = eggFloorSpriteBites2;
                break;
            case 3:
                eggSpriteRenderer.sprite = eggSpriteBites3;
                eggFloorSpriteRenderer.sprite = eggFloorSpriteBites3;
                break;
            case 4:
                eggSpriteRenderer.sprite = eggSpriteBites4;
                eggFloorSpriteRenderer.sprite = eggFloorSpriteBites4;
                break;
            case 5:
                eggSpriteRenderer.sprite = null;
                break;
            default:
                throw new ArgumentException("Invalid eggNumBites: " + eggNumBites);
        }
    }

    public void Talk()
    {
        animator.SetTrigger("talking");
    }

    public void OfferEgg()
    {
        print("MsHen OfferEgg");
        animator.SetBool("idle", false);
        animator.SetTrigger("offeringEgg");
        state = "offeringEgg";
    }

    public bool IsIdle()
    {
        return state == "idle";
    }

    public void ContinueTalking()
    {
        print("MsHen Continue Talking");

        if(IsIdle())
        {
            bubblesController.NextStep();
        }
    }
}
