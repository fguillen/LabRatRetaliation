using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    int screenUnits = 12;
    [SerializeField] Transform playerTransform;

    void Update()
    {
        int screenNumber = Mathf.FloorToInt(playerTransform.position.x / screenUnits);
        int cameraPositionX = (screenNumber * screenUnits) + (screenUnits / 2);
        transform.position = new Vector3(cameraPositionX, transform.position.y, transform.position.z);
    }
}
