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
            fork.EatFood();
            Debug.LogWarning("Yummy");
        }
        else
        {
            Debug.LogWarning("Food not found");
        }
    }
    
}
