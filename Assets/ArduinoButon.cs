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
    public Sprite redSprite;
    public Slider energySlider;
    public GameObject sliderGameObect;
    public TextMeshProUGUI powerText;
    private int power = 0;
    public TextMeshProUGUI percentageText;
    private SerialPort serialPort;
    private float elapsedTime;
    public bool isTimerRunning = false;
    private float startTime;
    List<int> powerList = new List<int>();

    public static float cumulativeEnergy = 0f;

    void Start()
    {
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
            elapsedTime = Time.time - startTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
            string timeText = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
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
        float neededEnergy = float.Parse(energyText.text);
        CloseSerialPort();
        if (powerList.Count > 0)
        {
            float averagePower = powerList.Sum() / (float)powerList.Count;
            float producedEnergy = averagePower * elapsedTime / 3600;
            Debug.Log("Port closed, " + averagePower + "Kw, " + elapsedTime + "s");
            Debug.Log(producedEnergy + "Kwh");
            if (producedEnergy >= neededEnergy)
            {
                Debug.Log("Energie completed");
            }
            else
            {
                Debug.Log("Energy not completed");
            }
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
        startTime = Time.time;
    }

    void UpdateEnergy()
    {
        float deltaTime = Time.time - startTime;
        startTime = Time.time;
        cumulativeEnergy += power * deltaTime / 3600f; // Update cumulative energy
    }

    public static float energyPercentage;

    void UpdateEnergySlider()
    {
        float maxEnergy = 1f; // Define the maximum energy value according to your game
        energyPercentage = cumulativeEnergy / maxEnergy;

        // Update the progress bar value
        energySlider.SetValueWithoutNotify(energyPercentage);
        percentageText.text = Mathf.RoundToInt(energySlider.value * 100) + "%";
    }
}
