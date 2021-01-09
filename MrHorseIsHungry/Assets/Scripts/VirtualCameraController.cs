using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TargetMrHorse()
    {
        print("VirtualCamera target MrHorse");
        animator.SetTrigger("mrHorse");
    }

    public void TargetMsGiraffe()
    {
        print("VirtualCamera target MrGiraffe");
        animator.SetTrigger("msGiraffe");
    }

    public void TargetMrElephant()
    {
        print("VirtualCamera target MrElephant");
        animator.SetTrigger("mrElephant");
    }

    public void TargetMsHen()
    {
        print("VirtualCamera target MsHen");
        animator.SetTrigger("msHen");
    }

    public void TargetEnd()
    {
        print("VirtualCamera target End");
        animator.SetTrigger("end");
    }
}
