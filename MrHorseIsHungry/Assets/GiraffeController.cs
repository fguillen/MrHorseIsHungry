using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraffeController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Fire
        if(Input.GetButtonDown("Jump"))
        {
            OfferApple();
        }
    }

    void OfferApple()
    {
        animator.SetTrigger("offeringApple");
    }
}
