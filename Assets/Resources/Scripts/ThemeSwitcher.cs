using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSwitcher : MonoBehaviour
{

    public void ToggleTheme()
    {
        int previousTheme = PlayerPrefs.GetInt("PreviousTheme", 1);
        int newTheme = previousTheme == 1 ? 2 : 1;
        SwitchTheme(newTheme);
    }

    // public void ForceInvertColors() {
    //     foreach (GameObject panel in GameObject.FindGameObjectsWithTag("Panel")) {
    //         panel.GetComponent<Image>().color = InvertColor(panel.GetComponent<Image>().color);
    //     }

    //     // Invert the color of all texts
    //     foreach (var text in FindObjectsOfType<Text>()) {
    //         text.color = InvertColor(text.color);
    //     }

    //     // Invert the color of all TextMeshProUGUI texts
    //     foreach (var text in FindObjectsOfType<TextMeshProUGUI>()) {
    //         text.color = InvertColor(text.color);
    //     }
    // }

    public void SwitchTheme(int theme)
    {
        string themeName = "Theme" + theme;

        Sprite[] themeSprites = Resources.LoadAll<Sprite>(themeName);


        int previousTheme = PlayerPrefs.GetInt("PreviousTheme", 1);

        PlayerPrefs.SetInt("CurrentTheme", theme);

        foreach (Image image in FindObjectsOfType<Image>())
        {
            if (image.sprite != null && image.sprite.name.Contains("t_"))
            {
                string spritePath = "Sprites/Theme" + previousTheme + "/" + image.sprite.name;

                if (Resources.Load<Sprite>(spritePath) != null)
                {
                    string newSpritePath = "Sprites/Theme" + theme + "/" + image.sprite.name;

                    image.sprite = Resources.Load<Sprite>(newSpritePath);
                }
            }
        }
    PlayerPrefs.SetInt("PreviousTheme", theme);
    }
}
