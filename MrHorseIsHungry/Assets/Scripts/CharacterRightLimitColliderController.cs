using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRightLimitColliderController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        print("RightLimitReached: enter");
        if(other.CompareTag("MrHorse"))
        {
            ObjectsReferrer.instance.mrHorseController.CharacterRightLimitEnter();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        print("RightLimitReached: exit");
        if(other.CompareTag("MrHorse"))
        {
            ObjectsReferrer.instance.mrHorseController.CharacterRightLimitExit();
        }
    }
}
