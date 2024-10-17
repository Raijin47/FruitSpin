using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceDownScreen : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            enabled = false;
            return;
        }
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        Vector3 downDirection = -mainCamera.transform.up;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, -downDirection);
    }
}
