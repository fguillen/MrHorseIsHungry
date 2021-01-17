using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLeftLimitColliderController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        print("LeftLimitReached: enter");
        if(other.CompareTag("MrHorse"))
        {
            ObjectsReferrer.instance.mrHorseController.CharacterLeftLimitEnter();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        print("LeftLimitReached: exit");
        if(other.CompareTag("MrHorse"))
        {
            ObjectsReferrer.instance.mrHorseController.CharacterLeftLimitExit();
        }
    }
}
