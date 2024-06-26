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

    private int _daysOfZeroHappiness = 0;
    private DateTime? _zeroHappinessStartDate = null;
    private DateTime _lastUpdateTime;
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
        LoadGameState(); // Ensure this is called at the start to load saved states including _lastUpdateTime

        DateTime currentTime = DateTime.Now;
        TimeSpan elapsedTime;

        if (_lastUpdateTime!= default(DateTime))
        {
            elapsedTime = currentTime - _lastUpdateTime;
        }
        else
        {
            // Handle the case where _lastUpdateTime hasn't been set yet
            elapsedTime = TimeSpan.Zero;
        }

        // Decrease hunger and happiness based on elapsed time
        int hungerDecrease = (int)(elapsedTime.TotalHours * 2); // Example calculation
        int happinessDecrease = (int)((99 - _hunger) * (elapsedTime.TotalHours * 10)); // Example calculation

        _hunger -= hungerDecrease;
        _happiness -= happinessDecrease;

        // Ensure values stay within bounds
        _hunger = Mathf.Max(_hunger, 0);
        _happiness = Mathf.Max(_happiness, 0);

        // Update _lastUpdateTime for the next cycle
        _lastUpdateTime = currentTime;
        SaveGameState(); // Immediately save the updated state including the new _lastUpdateTime

        if (PlayerPrefs.HasKey("name")) { _name = PlayerPrefs.GetString("name"); }
        else { PlayerPrefs.SetString("name", "Robot"); _name = "geen naam"; }

        if (PlayerPrefs.HasKey("coin"))
        {
            _coin = PlayerPrefs.GetInt("coin");
        }
        else
        {
            _coin = 30; // Default value
        }

        if (PlayerPrefs.HasKey("hunger"))
        {
            _hunger = PlayerPrefs.GetInt("hunger");
        }
        else
        {
            _hunger = 70; // Default value
        }

        // Repeat for other values

        if (PlayerPrefs.HasKey("happiness"))
        {
            _happiness = PlayerPrefs.GetInt("happiness");
        }
        else
        {
            _happiness = 70; // Default value
        }
    }

    void Update()
    {
        PlayerPrefs.SetInt("hunger", _hunger);
        PlayerPrefs.SetInt("happiness", _happiness);
        PlayerPrefs.SetInt("coin", _coin);

    }
    
    void UpdateStatus()
    {
        DateTime currentTime = DateTime.Now;
    }
    
    

    void SaveGameState()
    {
    PlayerPrefs.SetString("LastUpdateTime", _lastUpdateTime.ToString());
    // Save other relevant states like hunger, happiness, etc.
    }

    void LoadGameState()
    {
        if (PlayerPrefs.HasKey("LastUpdateTime"))
        {
            _lastUpdateTime = DateTime.Parse(PlayerPrefs.GetString("LastUpdateTime"));
        }
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

    

    public void foodButton(int i)
    {
        switch (i)
        {
            case 0:
            default:
            if (coin >= 5)
            {
                
                UpdateHunger(20);
                Updatecoin(-5);
                FoodPanel.SetActive(false);
                SaveGameState();
            }
            break;

            case (1):
            if (coin >= 10)
            {
                
                UpdateHunger(40);
                Updatecoin(-10);
                FoodPanel.SetActive(false);
                SaveGameState();
            }
            break;

            case (2):
            if (coin >= 15) {
                
                UpdateHunger(60);
                Updatecoin(-15);
                FoodPanel.SetActive(false);
                SaveGameState();
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
                SaveGameState();
            break;

            case (1):
           
                Updatecoin(25);
                SaveGameState();
            break;

            case (2):
            
                Updatecoin(50);
                SaveGameState();
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
                SaveGameState();
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
                
                Person.transform.position = new Vector3(-0.23f, 0f, -9.012836f);
                aaiCount++;
                if (aaiCount >= 15)
                {
                    Person.transform.Rotate(0f, 180f, 0f);
                    aaiCount = 0;
                }
                SaveGameState();
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
