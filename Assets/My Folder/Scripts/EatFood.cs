using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spoon"))
        {
            Transform liquid = other.transform.Find("Liquid");
            if (liquid != null)
            {
                if (liquid.gameObject.activeSelf)
                {
                    Debug.Log("Eating food");
                    liquid.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("No food");
                }
            }
            else
            {
                Debug.LogWarning("Liquid not found.");
            }
        }
    }
}
