using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public Transform snapPosition;
    public string targetTag = "Napkin"; 

    private void OnTriggerEnter(Collider other) // Box Collider's Is Trigger: collect object that trigger
    {
        if (other.CompareTag(targetTag))        // Is rigid body is tagged with "Napkin" [Box Co]
        { 
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;          // Stop physics movement
            }

            other.transform.SetPositionAndRotation(
                snapPosition.position,
                snapPosition.rotation
            );

            var grab = other.GetComponent<OVRGrabbable>();
            if (grab != null)
            {
                grab.enabled = false;
            }
        }
    }
}
