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
    [SerializeField] ParticleSystem particlesWalk;
    [SerializeField] GameObject mouth;
    [SerializeField] AudioClip[] clipsWalk;
    [SerializeField] AudioClip[] clipsBite;
    [SerializeField] AudioClip clipRainbowBurp;
    [SerializeField] Transform footPosition;

    AudioSource audioSourceWalk; 
    AudioSource audioSourceBite;


    bool endSceneStarted;
    bool leftLimitReached;
    bool rightLimitReached;
    bool imHungryBubbleShown;
    bool allowJumpingBetweenCameras;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSourceWalk = GetComponents<AudioSource>()[0];
        audioSourceBite = GetComponents<AudioSource>()[1];
        
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

        if(horizontal > 0 && rightLimitReached)
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
            Input.GetButtonDown("Jump")
        )
        {
            Bite();
        }

        if(allowJumpingBetweenCameras && Input.GetButtonDown("Jump"))
        {
            ObjectsReferrer.instance.virtualCameraController.NextRoundRobinCamera();
        }
    }

    void Bite()
    {
        animator.SetTrigger("bite");
    }

    public void BiteCloseMouthEvent()
    {
        var clip = clipsBite[UnityEngine.Random.Range(0, clipsBite.Length)];
        audioSourceBite.PlayOneShot(clip);
        
        var particles = Instantiate(particlesBiteAir, mouth.transform.position, Quaternion.identity);
        Destroy(particles.gameObject, 10);
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
        ObjectsReferrer.instance.virtualCameraController.TargetEnd();
    }

    public void EndSceneFinished()
    {
        bubblesController.NextStep();
        Invoke("AllowJumpingBetweenCameras", 1f);
        Invoke("ShowGameMenu", 3.5f);
    }

    public void AllowJumpingBetweenCameras()
    {
        allowJumpingBetweenCameras = true;
    }

    public void ShowGameMenu()
    {
        ObjectsReferrer.instance.gameMenuController.Appear();
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

    public void CharacterLeftLimitEnter()
    {
        bubblesController.ShowSmellsGoodLeftBubble();
        leftLimitReached = true;
    }

    public void CharacterLeftLimitExit()
    {
        leftLimitReached = false;
    }

    public void CharacterRightLimitEnter()
    {
        bubblesController.ShowSmellsGoodRightBubble();
        rightLimitReached = true;
    }

    public void CharacterRightLimitExit()
    {
        rightLimitReached = false;
    }


    public void ShowImHungryBubble()
    {
        if(!imHungryBubbleShown)
        {
            ObjectsReferrer.instance.audioManagerController.PlayBackgroundMusic();
            bubblesController.ShowImHungryBubble();
            imHungryBubbleShown = true;
        }
    }

    void RainbowBurpStarts()
    {
        audioSourceBite.clip = clipRainbowBurp;
        audioSourceBite.Play();
    }

    void RainbowBurpEnds()
    {
        audioSourceBite.Stop();
    }

    void WalkStep()
    {
        var clip = clipsWalk[UnityEngine.Random.Range(0, clipsWalk.Length)];
        audioSourceWalk.PlayOneShot(clip);

        // Walk particles
        Quaternion rotation = Quaternion.identity;
        if(figure.transform.localScale.x < 0){
            rotation = Quaternion.Euler(0,180f,0);
        }
        var particles = Instantiate(particlesWalk, footPosition.position, rotation);

        Destroy(particles.gameObject, 10f);
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
