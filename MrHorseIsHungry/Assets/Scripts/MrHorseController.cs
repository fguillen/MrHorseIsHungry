using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrHorseController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    float originalScaleX;
    [SerializeField] float speed;
    [SerializeField] MrHorseBubblesController bubblesController;
    [SerializeField] GameObject figure;

    [SerializeField] float tutorialWaitSeconds;
    [SerializeField] bool tutorialShown;
    [SerializeField] bool tutorialShownBite;
    [SerializeField] ParticleSystem particlesBiteAir;
    [SerializeField] GameObject mouth;
    [SerializeField] AudioClip[] clipsWalk;
    [SerializeField] AudioClip clipRainbowBurp;
    AudioSource audioSource; 


    bool endSceneStarted;
    bool leftLimitReached;
    bool imHungryBubbleShown;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        
        originalScaleX = figure.transform.localScale.x;
        endSceneStarted = false;
        leftLimitReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckShowTutorials();

        float horizontal = Input.GetAxisRaw("Horizontal");

        // Not moving if the scene is frozen
        if(IsSceneFrozen())
        {
            horizontal = 0;
        }

        if(horizontal < 0 && leftLimitReached)
        {
            horizontal = 0;
        }

        if(horizontal != 0)
        {
            animator.SetBool("walking", true);
        } else
        {
            animator.SetBool("walking", false);
        }

        if(horizontal < 0)
        {
            figure.transform.localScale = new Vector3(-originalScaleX, figure.transform.localScale.y, figure.transform.localScale.z);
        } else if(horizontal > 0)
        {
            figure.transform.localScale = new Vector3(originalScaleX, figure.transform.localScale.y, figure.transform.localScale.z);
        }

        rb.velocity = new Vector2(horizontal * speed, 0);

        // For debugging
        // if(Input.GetButtonDown("Jump"))
        // {
        //     StartEndScene();
        // }

        // Fire
        if(
            !IsSceneFrozen() &&
            Input.GetButtonDown("Jump"))
        {
            Bite();
        }
    }

    void Bite()
    {
        animator.SetTrigger("bite");
    }

    public void BiteCloseMouthEvent()
    {
        Instantiate(particlesBiteAir, mouth.transform.position, Quaternion.identity);
    }

    public void CheckShowTutorials()
    {
        if(!tutorialShown){
            tutorialWaitSeconds -= Time.deltaTime;
            if(tutorialWaitSeconds < 0)
            {
                bubblesController.ShowTutorial();
                tutorialShown = true;
            }
        }
    }

    public void StartEndScene()
    {
        print("End scene");
        endSceneStarted = true;
        bubblesController.NextStep();
    }

    public void Talk()
    {
        print("Horse Talk");
    }

    public void EndScene()
    {
        animator.SetTrigger("endScene");
    }

    public void EndSceneFinished()
    {
        bubblesController.NextStep();
    }

    public void LeftLimitEnter()
    {
        bubblesController.ShowLeftLimitBubble();
        leftLimitReached = true;
    }

    public void LeftLimitExit()
    {
        leftLimitReached = false;
    }

    public void ShowImHungryBubble()
    {
        if(!imHungryBubbleShown)
        {
            bubblesController.ShowImHungryBubble();
            imHungryBubbleShown = true;
        }
    }

    void RainbowBurpStarts()
    {
        audioSource.clip = clipRainbowBurp;
        audioSource.Play();
    }

    void RainbowBurpEnds()
    {
        audioSource.Stop();
    }

    void WalkStep()
    {
        var clip = clipsWalk[UnityEngine.Random.Range(0, clipsWalk.Length)];
        audioSource.PlayOneShot(clip);
    }

    bool IsSceneFrozen()
    {
        return(
            ObjectsReferrer.instance.msGiraffeBubblesController.AnyBubbleActive() || 
            ObjectsReferrer.instance.mrElephantBubblesController.AnyBubbleActive() || 
            ObjectsReferrer.instance.msHenBubblesController.AnyBubbleActive() ||
            ObjectsReferrer.instance.mrHorseBubblesController.AnyBubbleActive() ||
            endSceneStarted
        );
    }
}
