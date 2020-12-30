using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsGiraffeTalkAreaController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MrHorse"))
        {
            ObjectsReferrer.instance.msGiraffeController.ContinueTalking();
        }

        if(other.CompareTag("MrHorseMouth"))
        {
            ObjectsReferrer.instance.msGiraffeController.ContinueTalking();
        }
    }
}
