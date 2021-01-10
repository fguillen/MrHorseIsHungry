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

    [SerializeField] float tutorialWaitSecondsWalk;
    [SerializeField] float tutorialWaitSecondsBite;
    [SerializeField] bool tutorialShownWalk;
    [SerializeField] bool tutorialShownBite;
    [SerializeField] bool tutorialShownCloseDialogue;
    [SerializeField] ParticleSystem particlesBiteAir;
    [SerializeField] GameObject mouth;


    bool endSceneStarted;
    bool leftLimitReached;
    bool imHungryBubbleShown;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalScaleX = figure.transform.localScale.x;
        endSceneStarted = false;
        leftLimitReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckShowTutorials();

        float horizontal = Input.GetAxisRaw("Horizontal");

        // Not moving if Giraffe talking or End Scene started
        if(
            ObjectsReferrer.instance.msGiraffeBubblesController.bubbleActive ||
            ObjectsReferrer.instance.mrElephantBubblesController.bubbleActive ||
            ObjectsReferrer.instance.msHenBubblesController.bubbleActive ||
            ObjectsReferrer.instance.mrHorseBubblesController.IsBubbleActive() || 
            endSceneStarted
        )
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

        // Fire
        if(
            !ObjectsReferrer.instance.msGiraffeBubblesController.bubbleActive && 
            !ObjectsReferrer.instance.mrElephantBubblesController.bubbleActive && 
            !ObjectsReferrer.instance.msHenBubblesController.bubbleActive &&
            !ObjectsReferrer.instance.mrHorseBubblesController.IsBubbleActive() &&
            !endSceneStarted &&
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
        if(!tutorialShownWalk){
            tutorialWaitSecondsWalk -= Time.deltaTime;
            if(tutorialWaitSecondsWalk < 0)
            {
                bubblesController.ShowTutorialWalk();
                tutorialShownWalk = true;
            }
        }

        if(!tutorialShownBite){
            tutorialWaitSecondsBite -= Time.deltaTime;
            if(tutorialWaitSecondsBite < 0)
            {
                bubblesController.ShowTutorialBite();
                tutorialShownBite = true;
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

    public void Burp()
    {
        animator.SetTrigger("burp");
    }

    public void BurpEnds()
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
}
