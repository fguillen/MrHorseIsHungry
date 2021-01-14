using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLayerController : MonoBehaviour
{
    [SerializeField] BuildingController[] buildingTemplates;
    [SerializeField] Transform limitLeft;
    [SerializeField] Transform limitRight;
    [SerializeField] int renderLayerOrder;

    // Start is called before the first frame update
    void Start()
    {
        BuildLayer();
    }

    void BuildLayer()
    {
        BuildingController lastBuilding = BuildBuilding(limitLeft.position);

        while(lastBuilding.transform.position.x < limitRight.position.x)
        {
            lastBuilding = BuildBuilding(lastBuilding.anchorRight.position);
        }
    }

    BuildingController BuildBuilding(Vector3 position)
    {
        var buildingControllerTemplate = buildingTemplates[UnityEngine.Random.Range(0, buildingTemplates.Length)];
        BuildingController newBuildingController = buildingControllerTemplate.PlaceBuilding(position, renderLayerOrder);

        newBuildingController.PlaceBuilding(position, renderLayerOrder);

        return newBuildingController;
    }

}

