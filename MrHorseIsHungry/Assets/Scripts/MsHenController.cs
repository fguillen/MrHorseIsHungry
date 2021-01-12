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
    [SerializeField] ParticleSystem particlesBiteEgg;
    [SerializeField] AudioClip[] clipsEggBite;

    Animator animator;
    AudioSource audioSource;

    public int eggNumBites;

    [SerializeField] string state;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        eggNumBites = 0;
        RenderEgg();
        state = "idle";
    }

    public void EggBitten(Collider2D collider2D){
        eggNumBites ++;

        if(eggNumBites >= 5)
        {
            Invoke("Idle", 0.5f);
        } else {
            RenderEgg();

            audioSource.PlayOneShot(clipsEggBite[UnityEngine.Random.Range(0, clipsEggBite.Length)]);
            var particles = Instantiate(particlesBiteEgg, collider2D.transform.position, Quaternion.identity);
            Destroy(particles, 10);
        }
    }

    public void Idle()
    {
        print("MsHen state Idle");
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
        print("MsHen state OfferEgg");
        animator.SetBool("idle", false);
        animator.SetTrigger("offeringEgg");
        state = "offeringEgg";
    }

    public void EggFinished()
    {
        print("Egg finished");
        bubblesController.NextStep();
    }

    public bool IsIdle()
    {
        return state == "idle";
    }

    public void ContinueTalking()
    {
        print("MsHen Continue Talking");

        if(
            IsIdle() 
            &&
            (
                bubblesController.step == 0 ||
                bubblesController.step == 2 ||
                bubblesController.step == 8
            )
        )
        {
            print("MsHen Continue Talking ... NextStep");
            bubblesController.NextStep();
        }
    }
}
