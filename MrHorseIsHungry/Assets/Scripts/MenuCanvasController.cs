using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CreditsShow()
    {
        animator.SetBool("creditsShown", true);
    }

    public void CreditsHide()
    {
        animator.SetBool("creditsShown", false);
    }
}
