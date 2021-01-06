using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("MrHorseMouth"))
        {
            print("Egg Bitten");
            ObjectsReferrer.instance.msHenController.EggBitten();
        }
    }
}
