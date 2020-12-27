using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsReferrer : MonoBehaviour
{
    // Start is called before the first frame update
    public MsGiraffeController msGirafferController;
    public static ObjectsReferrer instance;
    
    void Awake()
    {
        instance = this;
    }
}
