using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrElephantTalkAreaController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MrHorse") && ObjectsReferrer.instance.mrElephantBubblesController.step == 0)
        {
            ObjectsReferrer.instance.mrElephantController.ContinueTalking();
        }

        if(other.CompareTag("MrHorseMouth"))
        {
            ObjectsReferrer.instance.mrElephantController.ContinueTalking();
        }
    }
}
