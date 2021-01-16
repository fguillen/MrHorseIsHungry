using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorFollower : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Transform target;
    [SerializeField] Vector2 targetPosition0;
    [SerializeField] Vector2 limitDimensions;
    [SerializeField] Transform limitSW;
    [SerializeField] Transform limitNE;

    Vector3 lastMousePosition;
    
    void Start()
    {
        targetPosition0 = new Vector2(limitSW.position.x, limitNE.position.y);
        limitDimensions = new Vector2(limitNE.position.x - limitSW.position.x, limitNE.position.y - limitSW.position.y);
    }
    
    void Update()
    {
        if(
            lastMousePosition != Input.mousePosition &&
            Input.mousePosition.x > 0 &&
            Input.mousePosition.x < Screen.width &&
            Input.mousePosition.y > 0 &&
            Input.mousePosition.y < Screen.height
        )
        {
            UpdatePosition();    
        }
    }

    void UpdatePosition()
    {
        lastMousePosition = Input.mousePosition;

        float targetPositionX = lastMousePosition.x * limitDimensions.x / Screen.width;
        float targetPositionY = (lastMousePosition.y * limitDimensions.y / Screen.height) - limitDimensions.y;
        Vector2 targetPosition2D = new Vector2(targetPositionX, targetPositionY);
        targetPosition2D += targetPosition0;

        target.position = new Vector3(targetPosition2D.x , targetPosition2D.y, target.position.z);
    }
}
