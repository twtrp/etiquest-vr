using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EatFood : MonoBehaviour
{
    public GameObject successPanel;
    int ate = 0;

    public GameObject hintPanel;

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
                    ate++;
                    if (ate >= 3)
                    {
                        successPanel.SetActive(true);
                        ate = 0;
                        StartCoroutine(ShowSuccessAndLoadScene());
                    }
                }
                else
                {
                    Debug.Log("No food");
                }
            }
            else
            {
                Debug.LogWarning("Liquid not found");
            }
        }
    }

    private IEnumerator ShowSuccessAndLoadScene()
    {
        if (successPanel != null)
        {
            successPanel.SetActive(true);
            hintPanel.SetActive(false);
        }

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("EtiQuest Salad Test Scene (Three)");
    }
}
