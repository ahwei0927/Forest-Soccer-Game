using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCollide : MonoBehaviour
{
    public GameObject[] post;
    private bool isScaling = false; // Flag to track if scaling is in progress
    private float scalingFactor = 1.5f; // Scaling factor for the ball

    private void Awake()
    {
        post[0] = GameObject.FindGameObjectWithTag("Post");
        post[1] = GameObject.FindGameObjectWithTag("post");
    }
    private void Update()
    {
        // Store the initial scale of the ball
        Vector3 initialScale1 = post[0].transform.localScale;
        Vector3 initialScale2 = post[1].transform.localScale;

        // Calculate the target scale after scaling
        Vector3 targetScale1 = initialScale1 * scalingFactor;
        Vector3 targetScale2 = initialScale2 * scalingFactor;

        if (isScaling)
        {
            // Ensure the ball reaches the target scale exactly
            post[0].transform.localScale = targetScale1;
            post[1].transform.localScale = targetScale2;

            isScaling = false;

            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isScaling)
        {
            isScaling = true;
        }
    }

}
