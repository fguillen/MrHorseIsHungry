using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Transform anchorLeft;
    [SerializeField] public Transform anchorRight;
    
    public BuildingController PlaceBuilding(Vector3 position, int renderLayerOrder)
    {
        var finalPosition = position - anchorLeft.localPosition;

        GameObject building = Instantiate(gameObject, finalPosition, Quaternion.identity);
        building.transform.Find("Figure").GetComponent<SpriteRenderer>().sortingOrder = renderLayerOrder;

        return building.GetComponent<BuildingController>();
    }
}
