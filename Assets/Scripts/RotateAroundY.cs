using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotateAroundY : MonoBehaviour
{
    public float rotationSpeed = 10f; // Speed of rotation around the y-axis

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}