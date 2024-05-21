using System;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ArduinoButon : MonoBehaviour
{
    public Button startButton;
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
    public int score;
    public static float cumulativeEnergy = 0f;

    void Start()
    {
        winorfailText.enabled = false;
        sliderGameObect.SetActive(false);
        startButton.onClick.AddListener(StartTimer);
        OpenSerialPort();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (!serialPort.IsOpen)
            {
                isTimerRunning = false; // Stop the timer
                Debug.Log("Connexion détruite, arrêt du chronomètre.");
                return;
            }
            elapsedTime = Time.time - startTimeChrono;
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
            timeText = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
            timerText.text = timeText;

            // Get the power value sent by Arduino
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
            //float averagePower = powerList.Sum() / (float)powerList.Count;
            //float producedEnergy = averagePower * elapsedTime / 3600;
            
            if (energySlider.value >= 1 && elapsedTime < time)
            {
                winorfailText.text = "You've Win !";
                score = 15;
            }
            else
            {
                winorfailText.text = "Failed !";
                
            }
            winorfailText.enabled = true;
            powerList.Clear();
        }
    }

    public void OpenSerialPort()
    {
        string portName = "COM3"; // Specify the correct COM port used by your Arduino
        int baudRate = 9600; // Same baud rate as specified in your Arduino code

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
    }

    void UpdateEnergy()
    {
        float deltaTime = Time.time - startTimeAction;
        startTimeAction = Time.time;
        cumulativeEnergy += power * deltaTime / 3600f; // Update cumulative energy
    }

    public static float energyPercentage;
    int time = 0;

    void UpdateEnergySlider()
    {
        if (titleText.text == "Cofee")
        {
            maxEnergy = 5;
            time = 60;
        }
        else if (titleText.text == "Dishes (1 cycle)")
        {
            maxEnergy = 920;
            time = 3600;
        }
        else if (titleText.text == "Cooking")
        {
            maxEnergy = 100;
            time = 300;
        }
        else if (titleText.text == "Washing (1 cycle)")
        {
            maxEnergy = 900;
            time = 5400;
        }

        energyPercentage = cumulativeEnergy / maxEnergy;

        // Update the progress bar value
        energySlider.SetValueWithoutNotify(energyPercentage);
        percentageText.text = Mathf.RoundToInt(energySlider.value * 100) + "%";
    }
}
