using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsReferrer : MonoBehaviour
{
    // Start is called before the first frame update
    public MrHorseController mrHorseController;
    public MrHorseBubblesController mrHorseBubblesController;
    public MsGiraffeController msGiraffeController;
    public MsGiraffeBubblesController msGiraffeBubblesController;
    public MrElephantController mrElephantController;
    public MrElephantBubblesController mrElephantBubblesController;
    public MsHenController msHenController;
    public MsHenBubblesController msHenBubblesController;
    public VirtualCameraController virtualCameraController;
    public static ObjectsReferrer instance;
    
    void Awake()
    {
        instance = this;
    }
}
