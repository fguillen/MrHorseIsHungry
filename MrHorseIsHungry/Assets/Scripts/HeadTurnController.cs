using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTurnController : MonoBehaviour
{
    // Update is called once per frame
    Animator animator;
    bool headPositionBackwards;

    void Start()
    {
        animator = GetComponent<Animator>();
        headPositionBackwards = false;
    }

    void Update()
    {
        CheckHeadPosition();
    }

    void CheckHeadPosition()
    {
        float distanceToMrHorse = ObjectsReferrer.instance.mrHorseController.gameObject.transform.position.x - gameObject.transform.position.x;

        if(distanceToMrHorse > 5 && !headPositionBackwards)
        {
            animator.SetBool("headBackwards", true);
            headPositionBackwards = true;
            
        } else if(distanceToMrHorse < -2 && headPositionBackwards)
        {
            headPositionBackwards = false;
            animator.SetBool("headBackwards", false);
        }
    }
}
