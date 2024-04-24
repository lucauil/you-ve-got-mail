using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Robot : MonoBehaviour
{
    [SerializeField]
    private int _hunger;
    [SerializeField]
    private int _happiness;
    [SerializeField]
    private int _coin;
    [SerializeField]
    private string _name;

    private bool _serverTime;
    private int _clickCount;

    public GameObject asielPanel;
    public GameObject Person;
    public GameObject aaiButton;
    public GameObject Happiness;
    public GameObject FoodPanel;
    public GameObject mailButton;

    void Start()
    {
        PlayerPrefs.SetString("then", "10/04/2023 05:00:00");
        updateStatus();

        if (PlayerPrefs.HasKey("name")) { _name = PlayerPrefs.GetString("name"); }
        else { PlayerPrefs.SetString("name", "Robot"); _name = "Robot"; }

        if (PlayerPrefs.HasKey("coin")) { _coin = PlayerPrefs.GetInt("coin"); PlayerPrefs.SetInt("coin", 30); _coin = 30; }
        else { PlayerPrefs.SetInt("coin", 30); _coin = 30; }

        if (PlayerPrefs.HasKey("hunger")) { _hunger = PlayerPrefs.GetInt("hunger"); }
        else { PlayerPrefs.SetInt("hunger", 70); _hunger = 70; }

        if (PlayerPrefs.HasKey("happiness")) { _happiness = PlayerPrefs.GetInt("happiness"); }
        else { PlayerPrefs.SetInt("happiness", 70); _happiness = 70; }   
    }

    void Update()
    {
    }
    
    void updateStatus()
    {
        TimeSpan ts = getTimeSpan();

        if (!PlayerPrefs.HasKey("then"))
        {
            PlayerPrefs.SetString("then", GetStringTime());
            _hunger -= (int)(ts.TotalHours * 2);
        }
        
        _happiness -= (int)((99 - _hunger) * (ts.TotalHours * 10));

        if (_hunger <= 0) { _hunger = 0; }

        if (_happiness <= 0) { _happiness = 0; }

       // if (_happiness == 0) 
        //{
         //   asielPanel.SetActive(true);
         //   Person.SetActive(false);
         //   aaiButton.SetActive(false);
        //}

        if (_serverTime) { updateServer(); }

        else { InvokeRepeating("updateDevice", 0f, 30f); }
    }

    void updateServer()
    {
    }

    void updateDevice()
    {
        PlayerPrefs.SetString("then", GetStringTime());
    }

    TimeSpan getTimeSpan()
    {
        if (_serverTime)
        {
            return new TimeSpan();
        }

        else
        {
            return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("then"));
        }
    }

    string GetStringTime()
    {
        DateTime now = DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
    }

    public int hunger
    {
        get { return _hunger; }
        set { _hunger = value; }
    }

    public int happiness
    {
        get { return _happiness; }
        set { _happiness = value; }
    }

    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int coin
    {
        get { return _coin; }
        set { _coin = value; }
    }

    public void UpdateHappiness(int i)
    {
        happiness += i;

        if (happiness >= 99)
        {
        happiness = 99;
        }
    }

    public void UpdateHunger(int i)
    {
        hunger += i;

        if (hunger >= 99)
        {
        hunger = 99;
        }
    }

    public void Updatecoin(int i)
    {
        coin += i;
        if (coin >= 99)
        {
            coin = 99;
        }

        if (coin <= 0)
        {
            coin = 0;
        }
    }

    public void saveRobot()
    {
        if (!_serverTime)
        {
            updateDevice();
            PlayerPrefs.SetInt("hunger", _hunger);
            PlayerPrefs.SetInt("happiness", _happiness);
            PlayerPrefs.SetInt("coin", _coin);
        }
    }

    public void foodButton(int i)
    {
        switch (i)
        {
            case 0:
            default:
            if (coin >= 5)
            {
                UpdateHappiness(20);
                UpdateHunger(20);
                Updatecoin(-5);
                FoodPanel.SetActive(false);
            }
            break;

            case (1):
            if (coin >= 10)
            {
                UpdateHappiness(35);
                UpdateHunger(40);
                Updatecoin(-10);
                FoodPanel.SetActive(false);
            }
            break;

            case (2):
            if (coin >= 15) {
                UpdateHappiness(50);
                UpdateHunger(60);
                Updatecoin(-15);
                FoodPanel.SetActive(false);
            }
            break;
        }
    }

    public void questButton(int i)
    {
        switch (i)
        {
            case (0):
            default:
                Updatecoin(10);
            break;
        }
    }

    public void asielButton(int i)
    {
        switch (i)
        {
            case 0:
            default:
            if (coin >= 30)
            {
                Updatecoin(-30);
                asielPanel.SetActive(false);
                Person.SetActive(true);
                aaiButton.SetActive(true);
            }
            break;
        }
    }

    private int aaiCount = 0;

    public void AaiButton(int i)
    {
        switch (i)
        {
            case 0:
            default:
                UpdateHappiness(5);
                Updatecoin(1);
                Person.transform.position = new Vector3(-0.23f, 0f, -9.012836f);
                aaiCount++;
                if (aaiCount >= 15)
                {
                    Person.transform.Rotate(0f, 180f, 0f);
                    aaiCount = 0;
                }
            break;
        }
    }

    public void checkMail(int i)
    {
        Application.OpenURL("mailto:");
        mailButton.GetComponent<Button>().interactable = false;


        mailButton.SetActive(false);
        Waiter.Wait(36000, () =>
        {
            if (mailButton != null)
                mailButton.SetActive(true);
        });
    }

    


}