using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrHorseController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    float originalScaleX;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalScaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Not moving if Giraffe talking
        if(
            ObjectsReferrer.instance.msGiraffeBubblesController.bubbleActive ||
            ObjectsReferrer.instance.mrElephantBubblesController.bubbleActive ||
            ObjectsReferrer.instance.msHenBubblesController.bubbleActive
        )
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
            transform.localScale = new Vector3(-originalScaleX, transform.localScale.y, transform.localScale.z);
        } else if(horizontal > 0)
        {
            transform.localScale = new Vector3(originalScaleX, transform.localScale.y, transform.localScale.z);
        }

        rb.velocity = new Vector2(horizontal * speed, 0);

        // Fire
        if(
            !ObjectsReferrer.instance.msGiraffeBubblesController.bubbleActive && 
            !ObjectsReferrer.instance.mrElephantBubblesController.bubbleActive && 
            !ObjectsReferrer.instance.msHenBubblesController.bubbleActive &&
            Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("bite");
        }
    }
}
