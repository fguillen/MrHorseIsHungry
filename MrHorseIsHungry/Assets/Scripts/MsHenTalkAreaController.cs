using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsHenTalkAreaController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MrHorse") && ObjectsReferrer.instance.msHenBubblesController.step == 0)
        {
            ObjectsReferrer.instance.msHenController.ContinueTalking();
        }

        if(other.CompareTag("MrHorseMouth"))
        {
            ObjectsReferrer.instance.msHenController.ContinueTalking();
        }
    }
}
