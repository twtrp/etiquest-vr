using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSteak : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ForkTool fork = other.GetComponent<ForkTool>();
        if (fork != null && fork.HasFoodStuck())
        {
            Vector3 foodSize = fork.GetStuckFoodSize();
            Debug.Log($"Food size: {foodSize}");

            if (foodSize.x < 0.07f && foodSize.y < 0.07f && foodSize.z < 0.07f)
            {
                fork.EatFood();
                Debug.LogWarning("Yummy");
            }
            else
            {
                Debug.LogWarning("Too big");
            }
        }
        else
        {
            Debug.LogWarning("Food not found");
        }
    }
    
}
