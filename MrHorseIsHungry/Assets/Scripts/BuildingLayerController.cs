using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLayerController : MonoBehaviour
{
    [SerializeField] BuildingController[] buildingTemplates;
    [SerializeField] Transform limitLeft;
    [SerializeField] Transform limitRight;
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
        print("BuildBuilding at: " + position);
        var buildingController = buildingTemplates[UnityEngine.Random.Range(0, buildingTemplates.Length)];
        // buildingController.gameObject.transform.parent = this.gameObject.transform;
        return buildingController.PlaceBuilding(position);
    }

}

