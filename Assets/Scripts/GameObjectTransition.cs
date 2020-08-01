﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectTransition
{
    public GameObject gameObj;
    public Vector3 dest;
    private float transitionSpeed = 10F;

    public GameObjectTransition(GameObject gameObj, Vector3 dest)
    {
        this.gameObj = gameObj;
        this.dest = dest;
    }

    public GameObjectTransition(GameObject gameObj, Vector3 dest, float transitionSpeed)
    {
        this.gameObj = gameObj;
        this.dest = dest;
        this.transitionSpeed = transitionSpeed;
    }

    public Boolean transitionX()
    {
        return transition(0, Vector3.right);
    }

    public Boolean transitionY()
    {
        return transition(1, Vector3.up);
    }

    private Boolean transition(int i, Vector3 shift)
    {
        if (Mathf.Approximately(dest[i], gameObj.transform.position[i]))
        {
            return false;
        }
        if (dest[i] > gameObj.transform.position[i])
        {
            Vector3 newPos = gameObj.transform.position + shift * Time.deltaTime * transitionSpeed;
            if (dest[i] > newPos[i]) gameObj.transform.position = newPos;
            else gameObj.transform.position = dest;
            return true;
        }
        if (dest[i] < gameObj.transform.position[i])
        {
            Vector3 newPos = gameObj.transform.position - shift * Time.deltaTime * transitionSpeed;
            if (dest[i] < newPos[i]) gameObj.transform.position = newPos;
            else gameObj.transform.position = dest;
            return true;
        }
        return false;
    }
}
