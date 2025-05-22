using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoopCheck : MonoBehaviour
{
    public Vector3 awayFromUserDirection = new Vector3(0, 0, 1); 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spoon"))
        {
            ScoopMovement movement = other.GetComponent<ScoopMovement>();
            if (movement == null)
            {
                Debug.LogWarning("No SpoonMovement component found on spoon.");
                return;
            }

            Vector3 moveDir = movement.MovementDirection.normalized;
            float alignment = Vector3.Dot(moveDir, awayFromUserDirection.normalized);

            if (alignment > 0.7f) 
            {
                Debug.Log("moving away correct direction");
                Transform liquid = other.transform.Find("Liquid");
                if (liquid != null)
                {
                    liquid.gameObject.SetActive(true); 
                }
                else
                {
                    Debug.LogWarning("Liquid not found.");
                }
            }
            else
            {
                Debug.Log("wrong direction");
                Transform liquid = other.transform.Find("Liquid");
                if (liquid != null)
                {
                    liquid.gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("Liquid not found.");
                }
            }
        }
    }
}
