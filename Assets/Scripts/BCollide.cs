using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Goal goalScript = FindObjectOfType<Goal>();
            if (goalScript != null)
            {
                goalScript.score++;
                goalScript.UpdateScore(goalScript.score);
            }
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}
