﻿#if UNITY_EDITOR

#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.AI;
using System.Security.Cryptography;

public class ElevatorScript : MonoBehaviour
{
    public Boolean isBottom;
    public Boolean isMoving;
    public Boolean isMovingUp;
    public GameObject cameraObj;
    private CameraScript cameraScript;
    private Queue<GameObject> personQueue;
    public float eleSpeed = 1f;
    public float yLimit = 3f;
    public int numFloors = 9;
    public int currFloor = 0;
    private TextMeshProUGUI floorNumText;
    public float sizeFloor;
    private int destFloor = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        isBottom = true;
        isMoving = false;
        isMovingUp = false;
        cameraScript = cameraObj.GetComponent<CameraScript>();
        floorNumText = gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>();

    }
    void Start()
    {
        personQueue = cameraScript.personQueue;
        floorNumText.text = "0";
        sizeFloor = 2 * yLimit / numFloors;
    }

    // Update is called once per frame
    void Update()
    {
        updateFloorNum();


        if (isMoving && isMovingUp)
        {
            moveUp();
        }


        if (isMoving && !isMovingUp)
        {
            moveDown();
        }
    }
        
    void updateFloorNum()
    {
        currFloor = Convert.ToInt32((yLimit + transform.position.y) / sizeFloor);
        floorNumText.text = currFloor.ToString();
    }

    public void deliverToFloor(int floorNum)
    {
        destFloor = floorNum;
        if (currFloor < floorNum)
        {
            isMoving = true;
            isMovingUp = true;
        }
        if (currFloor > floorNum) {
            isMoving = true;
            isMovingUp = false;
        }
    }

    void moveUp()
    {
        if (transform.position.y < -yLimit + sizeFloor*destFloor)
        {
            Vector3 newPos = transform.position + Vector3.up * Time.deltaTime * eleSpeed;
            if (newPos.y < -yLimit + sizeFloor * destFloor) transform.position = newPos;
            else transform.position = new Vector3(0, -yLimit + sizeFloor * destFloor, 0);
        }
        if (Mathf.Approximately(transform.position.y, -yLimit + sizeFloor * destFloor))
        {
            isMoving = false;
        }
    }

    void moveDown()
    {
        if (transform.position.y > -yLimit + sizeFloor * destFloor)
        {
            Vector3 newPos = transform.position - Vector3.up * Time.deltaTime * eleSpeed;
            if (newPos.y > -yLimit + sizeFloor * destFloor) transform.position = newPos;
            else transform.position = new Vector3(0, -yLimit + sizeFloor * destFloor, 0);
        }
        if (Mathf.Approximately(transform.position.y, -yLimit + sizeFloor * destFloor))
        {
            isMoving = false;
        }
    }
}
