using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public GameObject EasyButton;
    public GameObject AvergeButton;
    public GameObject DificultButton;

    IEnumerator ActivateAfterDelay(GameObject gameObject, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        gameObject.SetActive(true);
    }

    IEnumerator ReactivateButtonAfterHours(GameObject button, float hours)
    {
        yield return new WaitForSeconds(hours * 60 * 60f); // Convert hours to seconds
        button.SetActive(true);
    }

    void RestoreButtonState(GameObject button, string key)
{
    int isActive = PlayerPrefs.GetInt(key, 0);
    button.SetActive(isActive == 1);
}

    void Start()
    {
        RestoreButtonState(EasyButton, "EasyButtonActiveState");
        RestoreButtonState(AvergeButton, "AverageButtonActiveState");
        RestoreButtonState(DificultButton, "DifficultButtonActiveState");

        // Optionally, start coroutines to reactivate buttons after some hours
        StartCoroutine(ReactivateButtonAfterHours(EasyButton, 3 * 60 * 60f));
        StartCoroutine(ReactivateButtonAfterHours(AvergeButton, 6 * 60 * 60f));
        StartCoroutine(ReactivateButtonAfterHours(DificultButton, 12 * 60 * 60f));
    }

    void SaveButtonStates()
{
    bool easyIsActive = EasyButton.activeSelf;
    bool averageIsActive = AvergeButton.activeSelf;
    bool dificultIsActive = DificultButton.activeSelf;

    PlayerPrefs.SetInt("EasyButtonActiveState", easyIsActive? 1 : 0);
    PlayerPrefs.SetInt("AverageButtonActiveState", averageIsActive? 1 : 0);
    PlayerPrefs.SetInt("DifficultButtonActiveState", dificultIsActive? 1 : 0);

    PlayerPrefs.Save();
}

    

    public void easy(int i)
    {
        Application.OpenURL("mailto:");
       
        
    }

    public void average(int i)
    {
        Application.OpenURL("https://mail.google.com");
       //AvergeButton.GetComponent<Button>().interactable = false;


        // avergeButton.SetActive(false);
        // Waiter.Wait(36000, () =>
        // {
        //     if (avergeButton != null)
        //         avergeButton.SetActive(true);
        // });
    }
    public void dificul(int i)
    {
        Application.OpenURL("mailto:");
        //DificultButton.GetComponent<Button>().interactable = false;


        // dificultButton.SetActive(false);
        // Waiter.Wait(36000, () =>
        // {
        //     if (dificultButton != null)
        //         easyButton.SetActive(true);
        // });
    }

   
}
