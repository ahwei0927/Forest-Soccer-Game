using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCollide : MonoBehaviour
{
    public GameObject post;
    private bool isScaling = false; // Flag to track if scaling is in progress
    private float scalingDuration = 10f; // Duration of scaling process
    private float scalingFactor = 2f; // Scaling factor for the ball

    private void Awake()
    {
        post = GameObject.FindGameObjectWithTag("Post");
    }
    private void Update()
    {
        // Store the initial scale of the ball
        Vector3 initialScale = post.transform.localScale;

        // Calculate the target scale after scaling
        Vector3 targetScale = initialScale * scalingFactor;

        if (isScaling)
        {
            float elapsedTime = 0f;

            while (elapsedTime < scalingDuration)
            {

                post.transform.localScale = Vector3.Lerp(initialScale, targetScale, 1);

                elapsedTime += Time.deltaTime;

            }
            // Ensure the ball reaches the target scale exactly
            post.transform.localScale = targetScale;

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
