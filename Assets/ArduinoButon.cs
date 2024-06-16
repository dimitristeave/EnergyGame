using System;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using Photon.Pun.Demo.PunBasics;

public class ArduinoButon : MonoBehaviour
{
    public Button startButton;
    public Button pauseButton;
    public Button stopButton;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI energyText;
    string powerString;
    public Image image;
    public TextMeshProUGUI titleText;
    public Sprite redSprite;
    public Slider energySlider;
    public GameObject sliderGameObect;
    public TextMeshProUGUI powerText;
    private int power = 0;
    public TextMeshProUGUI percentageText;
    private SerialPort serialPort;
    private float elapsedTime;
    public bool isTimerRunning = false;
    private float startTimeChrono;
    private float startTimeAction;
    List<int> powerList = new List<int>();
    public int maxEnergy;
    public static string timeText;
    public TextMeshProUGUI winorfailText;
    public int score = 0;
    public static float cumulativeEnergy = 0f;
    public Image pauseImage;
    public Image stopImage;
    public Image startImage;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI stopText;
    private int pauseCount = 0;
    private float pauseDuration = 0;
    private int maxPauses = 0;
    private float maxPauseDuration = 0;
    private int pauseIndex;
    private float startTimePause;
    //private PlayerLeaderboard leaderboard;
    GameManagerDb gameManagerDb;
    ShowFeatures showFeatures;

    void Start()
    {
        winorfailText.enabled = false;
        sliderGameObect.SetActive(false);
        startButton.onClick.AddListener(StartTimer);
        pauseButton.onClick.AddListener(PauseTimer);
        stopButton.onClick.AddListener(StopTimer);
        OpenSerialPort();
        string playerName = PlayerPrefs.GetString("PlayerName", "default_player");

        // Initially disable pause and stop buttons
        pauseImage.enabled = false;
        stopImage.enabled = false;
        pauseText.enabled=false;
        stopText.enabled = false;

        gameManagerDb = FindObjectOfType<GameManagerDb>();
        //leaderboard = FindObjectOfType<PlayerLeaderboard>();
        //leaderboard.UpdateLeaderboard();

        showFeatures = FindObjectOfType<ShowFeatures>();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (!serialPort.IsOpen)
            {
                isTimerRunning = false;
                Debug.Log("Connexion d�truite, arr�t du chronom�tre.");
                return;
            }
            elapsedTime = Time.time - startTimeChrono;
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
            timeText = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
            timerText.text = timeText;

            if (serialPort != null && serialPort.IsOpen && serialPort.BytesToRead > 0)
            {
                string data = serialPort.ReadLine();
                powerString = data;
                powerText.text = data;
                power = int.Parse(powerString);
                if (!powerList.Contains(power))
                {
                    powerList.Add(power);
                    Debug.Log(powerString);
                }
                UpdateEnergy();
                UpdateEnergySlider();
            }
        }
    }

    public void DestroyConnection()
    {
        CloseSerialPort();

        if (powerList.Count > 0)
        {
            if (energySlider.value >= 1 && elapsedTime < time)
            {
                winorfailText.text = "You've Win!";
                score = 15;
            }
            else
            {
                winorfailText.text = "Failed!";
                score = 0;
            }
            winorfailText.enabled = true;
            powerList.Clear();
        }
        // Update player data in JSON
        UpdatePlayerData();
    }

    public void OpenSerialPort()
    {
        string portName = "COM3";
        int baudRate = 9600;

        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();
    }

    void CloseSerialPort()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    void StartTimer()
    {
        isTimerRunning = true;
        startTimeChrono = Time.time;
        startTimeAction = Time.time;
        startTimePause = Time.time;
        OpenSerialPort();
        sliderGameObect.SetActive(true);
        cumulativeEnergy = 0;
        pauseImage.enabled = true;
        stopImage.enabled = true;
        pauseText.enabled = true;
        stopText.enabled = true;
        pauseButton.interactable = true;
        pauseCount = 0;

        // Disable start button
        startImage.enabled = false;
        startText.enabled = false;
        //startButton.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }

    void PauseTimer()
    {
        
        if (isTimerRunning)
        {
            float startTimePause = Time.time;
            if (pauseCount < maxPauses )
            {
                isTimerRunning = false;
                pauseText.text = "Reset";
                pauseCount++;
                pauseIndex=maxPauses-pauseCount;
                showFeatures.pauseText.text = pauseIndex.ToString();
            }
            else
            {
                // Disable pause button when max pauses or duration is reached
                pauseButton.interactable = false;
            }
        }
        else
        {
            float endTimePause = Time.time;
            pauseDuration = endTimePause - startTimePause;          
            TimeSpan timeSpan = TimeSpan.FromSeconds(pauseDuration);
            float total = (float)timeSpan.TotalSeconds;
            //Debug.Log(total);
            if (total > maxPauseDuration)
            {
                pauseCount++;
            }
            isTimerRunning = true;
            startTimeChrono = Time.time - elapsedTime;
            startTimeAction = Time.time;
            pauseText.text = "Pause";
        }
        pauseImage.enabled = true;
    }

    void StopTimer()
    {
        isTimerRunning = false;
        DestroyConnection();

        // Disable pause and stop buttons
        pauseImage.enabled = false;
        stopImage.enabled = false;
        pauseText.text = "Pause";
        pauseButton.interactable = true;
        pauseText.enabled = false;
        stopText.enabled=false;    
    }

    void UpdateEnergy()
    {
        if (isTimerRunning)
        {
            float deltaTime = Time.time - startTimeAction;
            startTimeAction = Time.time;
            cumulativeEnergy += power * deltaTime / 3600f;
        }
       
    }

    public static float energyPercentage;
    int time = 0;
    

    void UpdateEnergySlider()
    {
        if (titleText.text == "Cofee")
        {
            maxEnergy = 5;
            time = 6*60;
            maxPauses = 1;
            maxPauseDuration = 40f;
        }
        else if (titleText.text == "Dishes (1 cycle)")
        {
            maxEnergy = 920;
            time = 3*60*60;
            maxPauses = 10;
            maxPauseDuration = 60f;
        }
        else if (titleText.text == "Cooking")
        {
            maxEnergy = 100;
            time = 1*60*60;
            maxPauses = 5;
            maxPauseDuration = 40f;
        }
        else if (titleText.text == "Washing (1 cycle)")
        {
            maxEnergy = 900;
            time = 3*60*60;
            maxPauses = 10;
            maxPauseDuration = 50f;
        }
        energyPercentage = cumulativeEnergy / maxEnergy;

        energySlider.SetValueWithoutNotify(energyPercentage);
        percentageText.text = Mathf.RoundToInt(energySlider.value * 100) + "%";
    }

    // Method to update player data in JSON
    
    void UpdatePlayerData()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "default_player");     
        PlayerDatabase db = gameManagerDb.LoadPlayerDatabase();
        Dictionary<string, PlayerInfo> players = db.ToDictionary();

        if (!players.ContainsKey(playerName))
        {
            players[playerName] = new PlayerInfo { score_total = 0, energie_total = 0 };
        }

        players[playerName].score_total += score;
        players[playerName].energie_total += cumulativeEnergy;

        db.FromDictionary(players);
        bool saveSuccess = gameManagerDb.SavePlayerDatabase(db);
        if (saveSuccess)
        {
            Debug.Log("Player data updated and saved successfully");
        }
        else
        {
            Debug.LogError("Failed to update and save player data");
        }
    }

}

