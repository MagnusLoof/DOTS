using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }
    public Vector3 MousePosition;

    private void Awake()
    {
        if(Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        transform.position = new Vector3(CameraMovementSystem.Instance.CameraPosition.x, CameraMovementSystem.Instance.CameraPosition.y, transform.position.z);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -transform.position.z;
        MousePosition = Camera.main.ScreenToViewportPoint(mousePos);
    }
}
