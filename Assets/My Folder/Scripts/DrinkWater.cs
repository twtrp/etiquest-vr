using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrinkWater : MonoBehaviour
{
    public GameObject successPanel;
    bool drankWater = false;
    bool drankWine = false;

    public GameObject hintPanel;

    private void OnTriggerEnter(Collider other)
    {
        //if (ate >= 2 && SceneManager.GetActiveScene().name == "EtiQuest Steak Test Scene (Three)")
        //{
        //    Debug.Log("Congrat");
        //    if (successPanel != null)
        //        successPanel.SetActive(true);
        //    ate = 0;
        //}
        Debug.Log("on trigger:", other);
        if (other.CompareTag("WaterGlass"))
        {
            Transform liquid = other.transform.Find("Liquid");
            if (liquid != null)
            {
                if (liquid.gameObject.activeSelf)
                {
                    liquid.gameObject.SetActive(false);
                    drankWater = true;
                    if (drankWine)
                    {
                        successPanel.SetActive(true);
                        drankWater = false;
                        drankWine = false;
                        if (successPanel != null)
                        {
                            successPanel.SetActive(true);
                            hintPanel.SetActive(false);
                        };
                    }
                }
            }
        }
        else if (other.CompareTag("WineGlass"))
            {
                Transform liquid = other.transform.Find("Liquid");
                if (liquid != null)
                {
                    if (liquid.gameObject.activeSelf)
                    {
                        liquid.gameObject.SetActive(false);
                        drankWine = true;
                        if (drankWater)
                        {
                            successPanel.SetActive(true);
                            drankWater = false;
                            drankWine = false;
                            if (successPanel != null)
                            {
                                successPanel.SetActive(true);
                                hintPanel.SetActive(false);
                            };
                        }
                    }
                }
            }
    }
}
