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
                isTimerRunning = false; // Arrêter le chronomètre
                Debug.Log("Connexion détruite, arrêt du chronomètre.");
                return;
            }
            elapsedTime = Time.time - startTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
            string timeText = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
            timerText.text = timeText;

            // Récupérer la valeur de puissance envoyée par Arduino
            if (serialPort != null && serialPort.IsOpen && serialPort.BytesToRead > 0)
            {
                string data = serialPort.ReadLine();
                powerString = data;   
                powerText.text = data;
                power = int.Parse(powerString);
                for (int i = 0; i < powerList.Count + 1; i++)
                {
                    if (!powerList.Contains(power))
                    {
                        powerList.Add(power);
                        Debug.Log(powerString);
                    }
                }
                UpdateEnergySlider();
            }
            
        }
    }
    public void DestroyConnection()
    {
        float needeedEnergy = float.Parse(energyText.text);
        CloseSerialPort();
        if (powerList.Count > 0)
        {
            float averagePower = powerList.Sum() / (powerList.Count);
            float producedEnergy = averagePower * elapsedTime / 3600;
            Debug.Log("Port closed, " + averagePower+ "Kw, " + elapsedTime +"s");
            Debug.Log(producedEnergy + "Kwh");
            if(producedEnergy >= needeedEnergy)
            {
                Debug.Log("Energie completed");
            }
            else
            {
                Debug.Log("Energy not commpleted");
            }
            powerList = new List<int>();
        }
        
        
    }

    public void OpenSerialPort()
    {
        string portName = "COM3"; // Spécifiez le bon port COM utilisé par votre Arduino
        int baudRate = 9600; // Même baud rate que celui spécifié dans votre code Arduino

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
    private float previous;
    public float energyPercentage;

    void UpdateEnergySlider()
    {
        float maxEnergy = 1f; // Définissez la valeur maximale d'énergie en fonction de votre jeu
        float producedEnergy = power * elapsedTime / 3600;
        energyPercentage = producedEnergy / maxEnergy;
        
        if (energyPercentage < previous)
        {
            energySlider.SetValueWithoutNotify(energyPercentage);
        }
        // Mettre à jour la valeur de la barre de progression
        energySlider.SetValueWithoutNotify(energyPercentage);
        percentageText.text = Mathf.RoundToInt(energySlider.value*100) + "%";
        previous = energyPercentage;
    }

}
