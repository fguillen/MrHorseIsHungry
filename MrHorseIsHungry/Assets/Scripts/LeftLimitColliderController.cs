using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLimitColliderController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        print("LeftLimitReached: enter");
        if(other.CompareTag("MrHorse"))
        {
            ObjectsReferrer.instance.mrHorseController.LeftLimitEnter();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        print("LeftLimitReached: exit");
        if(other.CompareTag("MrHorse"))
        {
            ObjectsReferrer.instance.mrHorseController.LeftLimitExit();
        }
    }
}
