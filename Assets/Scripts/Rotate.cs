using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 10f;
    //public Vector3 rotation;
    //public Transform target;
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        //transform.localEulerAngles = rotation;
        //transform.localRotation = Quaternion.Euler(rotation);
        //transform.LookAt(target);
    }
}

