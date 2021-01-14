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
    
    [ContextMenu("TargetMrHorse")]
    public void TargetMrHorse()
    {
        print("VirtualCamera target MrHorse");
        ObjectsReferrer.instance.audioManagerController.PlayBackgroundMusic();
        animator.SetInteger("cameraTarget", 0);
    }

    [ContextMenu("TargetMsGiraffe")]
    public void TargetMsGiraffe()
    {
        print("VirtualCamera target MrGiraffe");
        ObjectsReferrer.instance.audioManagerController.PlayBackgroundMusicMsGiraffe();
        animator.SetInteger("cameraTarget", 1);
    }

    public void TargetMrElephant()
    {
        print("VirtualCamera target MrElephant");
        ObjectsReferrer.instance.audioManagerController.PlayBackgroundMusicMrElephant();
        animator.SetInteger("cameraTarget", 2);
    }

    public void TargetMsHen()
    {
        print("VirtualCamera target MsHen");
        ObjectsReferrer.instance.audioManagerController.PlayBackgroundMusicMsHen();
        animator.SetInteger("cameraTarget", 3);
    }

    public void TargetEnd()
    {
        print("VirtualCamera target End");
        ObjectsReferrer.instance.audioManagerController.PlayBackgroundMusicEndScene();
        animator.SetInteger("cameraTarget", 4);
    }
}
