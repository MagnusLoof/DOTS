using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }
    public Vector3 MousePosition;
    public Vector3 CameraPosition;

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
        CameraPosition = new Vector3(CameraMovementSystem.Instance.CameraPosition.x, CameraMovementSystem.Instance.CameraPosition.y, transform.position.z);
        transform.position = CameraPosition;
        CameraPosition.z = 0;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        MousePosition = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
