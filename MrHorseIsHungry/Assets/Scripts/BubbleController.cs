using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();       
    }

    public void Appear()
    {
        animator.SetTrigger("appear");
    }

    public void Disappear()
    {
        animator.SetTrigger("disappear");
    }

}
