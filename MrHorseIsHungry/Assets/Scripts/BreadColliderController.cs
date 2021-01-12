using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadColliderController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("MrHorseMouth"))
        {
            print("Bread Bitten");
            ObjectsReferrer.instance.mrElephantController.BreadBitten(other);
        }
    }
}
