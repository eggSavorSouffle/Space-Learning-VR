using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool rotateObject = false;
    public float turnSpeed = 10f;

    void Update()
    {
        if (rotateObject)
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
    }

    public void PointerEnter()
    {
        rotateObject = true;
    }

    public void PointerExit()
    {
        rotateObject = false;
    }
}
