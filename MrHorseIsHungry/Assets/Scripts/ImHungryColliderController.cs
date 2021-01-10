using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImHungryColliderController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MrHorse"))
        {
            ObjectsReferrer.instance.mrHorseController.ShowImHungryBubble();
        }
    }
}
