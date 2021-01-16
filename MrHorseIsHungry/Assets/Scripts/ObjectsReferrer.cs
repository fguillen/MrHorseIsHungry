using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsReferrer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioManagerController audioManagerController;
    public MrHorseController mrHorseController;
    public MrHorseBubblesController mrHorseBubblesController;
    public MsGiraffeController msGiraffeController;
    public MsGiraffeBubblesController msGiraffeBubblesController;
    public MrElephantController mrElephantController;
    public MrElephantBubblesController mrElephantBubblesController;
    public MsHenController msHenController;
    public MsHenBubblesController msHenBubblesController;
    public VirtualCameraController virtualCameraController;
    public GameMenuController gameMenuController;
    public MenuCanvasController menuCanvasController;
    public static ObjectsReferrer instance;
    
    void Awake()
    {
        instance = this;
    }
}
