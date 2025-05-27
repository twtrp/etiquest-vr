using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EatSteak : MonoBehaviour
{
    public GameObject successPanel;
    int ate = 0;

    private void OnTriggerEnter(Collider other)
    {
        ForkTool fork = other.GetComponent<ForkTool>();
        if (fork != null && fork.HasFoodStuck())
        {
            Vector3 foodSize = fork.GetStuckFoodSize();
            Debug.Log($"Food size: {foodSize}");

            if (foodSize.x <= 0.07f && foodSize.y <= 0.07f && foodSize.z <= 0.07f)
            {
                fork.EatFood();
                Debug.Log("Yummy");
                ate++;
                if (ate >= 2 && SceneManager.GetActiveScene().name == "EtiQuest Salad Test Scene (Three)")
                {
                    ate = 0;
                    StartCoroutine(ShowSuccessAndLoadScene());
                }
                else if (ate >= 2 && SceneManager.GetActiveScene().name == "EtiQuest Steak Test Scene (Three)")
                {
                    Debug.Log("Congrat");
                    if (successPanel != null)
                        successPanel.SetActive(true);
                    ate = 0;
                }
            }
            else
            {
                Debug.Log("Too big");
            }
        }
        else
        {
            Debug.Log("Food not found");
        }
    }
    private IEnumerator ShowSuccessAndLoadScene()
    {
        if (successPanel != null)
            successPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("EtiQuest Steak Test Scene (Three)");
    }
}
