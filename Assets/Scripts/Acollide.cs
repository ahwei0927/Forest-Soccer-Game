using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acollide : MonoBehaviour
{
    public GameObject ball;
    private bool isScaling = false; // Flag to track if scaling is in progress
    private float scalingDuration = 10f; // Duration of scaling process
    private float scalingFactor = 1.5f; // Scaling factor for the ball

    private void Awake()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }
    private void Update()
    {
        // Store the initial scale of the ball
        Vector3 initialScale = ball.transform.localScale;

        // Calculate the target scale after scaling
        Vector3 targetScale = initialScale * scalingFactor;

        if (isScaling)
        {
            float elapsedTime = 0f;

            while (elapsedTime < scalingDuration)
            {
               
                ball.transform.localScale = Vector3.Lerp(initialScale, targetScale, 1);

                elapsedTime += Time.deltaTime;
                
            }
            // Ensure the ball reaches the target scale exactly
            ball.transform.localScale = targetScale;

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
