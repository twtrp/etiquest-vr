using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoopCheck : MonoBehaviour
{
    public Vector3 awayFromUserDirection = new Vector3(0, 0, -1); 

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

            if (alignment > 0.7f) // 0.7 = roughly 45? cone away from user
            {
                Debug.Log("? Valid scoop — spoon moved away from user in soup zone.");
                // Trigger soup scooping effect here
            }
            else
            {
                Debug.Log("? Invalid scoop — wrong direction.");
            }
        }
    }
}
