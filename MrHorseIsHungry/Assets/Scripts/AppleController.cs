using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("MrHorseMouth"))
        {
            print("Bitten");
            ObjectsReferrer.instance.msGiraffeController.AppleBitten();
        }
    }
}
