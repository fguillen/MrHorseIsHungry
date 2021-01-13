using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsController : MonoBehaviour
{
    [SerializeField] Transform targetToFollow;
    float xOffset;

    void Start()
    {
        xOffset = transform.position.x - targetToFollow.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(targetToFollow.position.x + xOffset, transform.position.y, transform.position.z);
        transform.position = position;
    }
}
