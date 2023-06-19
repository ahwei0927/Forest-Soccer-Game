using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Penalty : MonoBehaviour
{
    public Transform ballSpawnPoint;
    public GameObject player;
    private bool resetPosition = false;

    [HideInInspector] public bool isPenalty = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            ballRb.velocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
            other.gameObject.transform.position = ballSpawnPoint.position;


        }
    }


}
