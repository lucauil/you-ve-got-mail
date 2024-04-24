using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHandler : MonoBehaviour
{

    bool isOn;
    GameObject toggleCircle;
    Vector3 onPosition = new Vector3(23f, 0f, 0f), offPosition = new Vector3(-23f, 0f, 0f);
    float lerpTime = 0.15f, currentLerpTime;
    public GameObject GO;
    public string scriptName, functionToExecute;

    void Start()
    {
        toggleCircle = transform.GetChild(0).gameObject;
        
        if (PlayerPrefs.GetInt("CurrentTheme", 1) == 2)
        {
            GO.GetComponent(scriptName).SendMessage("ToggleTheme");
            Toggle();
        }
    }

    public void Toggle()
    {
        isOn = !isOn;

        // Delete this if used for other purposes vv
        int currentTheme = PlayerPrefs.GetInt("CurrentTheme", 1);
        string onOff = isOn ? "On" : "Off";
        string spritePath = $"Sprites/Theme{currentTheme}/t_ToggleBackground{onOff}";
        string newSpritePath = $"Sprites/Theme{currentTheme}/t_ToggleHandle{onOff}";
        GetComponent<Image>().sprite = Resources.Load<Sprite>(spritePath);
        transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(newSpritePath);
        // Delete this if used for other purposes ^^

        currentLerpTime = 0f;
        GO.GetComponent(scriptName).SendMessage(functionToExecute);
    }

    void Update()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime) { currentLerpTime = lerpTime; }
        float t = currentLerpTime / lerpTime;
        toggleCircle.transform.localPosition = isOn ? Vector3.Lerp(offPosition, onPosition, t) : Vector3.Lerp(onPosition, offPosition, t);
    }
}
