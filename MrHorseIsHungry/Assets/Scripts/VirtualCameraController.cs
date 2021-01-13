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
        ObjectsReferrer.instance.audioManagerController.PlayBackgroundMusic();
        animator.SetTrigger("mrHorse");
    }

    public void TargetMsGiraffe()
    {
        print("VirtualCamera target MrGiraffe");
        ObjectsReferrer.instance.audioManagerController.PlayBackgroundMusicMsGiraffe();
        animator.SetTrigger("msGiraffe");
    }

    public void TargetMrElephant()
    {
        print("VirtualCamera target MrElephant");
        ObjectsReferrer.instance.audioManagerController.PlayBackgroundMusicMrElephant();
        animator.SetTrigger("mrElephant");
    }

    public void TargetMsHen()
    {
        print("VirtualCamera target MsHen");
        ObjectsReferrer.instance.audioManagerController.PlayBackgroundMusicMsHen();
        animator.SetTrigger("msHen");
    }

    public void TargetEnd()
    {
        print("VirtualCamera target End");
        ObjectsReferrer.instance.audioManagerController.PlayBackgroundMusicEndScene();
        animator.SetTrigger("end");
    }
}
