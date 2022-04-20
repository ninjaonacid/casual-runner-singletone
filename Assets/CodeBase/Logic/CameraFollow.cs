using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 camPosition;
    
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    
    void LateUpdate()
    {
        transform.position = player.transform.position + camPosition;
    }
}
