using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.UI;


public class ShowFeatures : MonoBehaviour
{
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI rentText;
    public TextMeshProUGUI taskText;
    public Image imageStart;
    public TextMeshProUGUI textStart;
    public ArduinoButon arduinoButon;
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI timePauseText;
    public int maxPause;

    public void Start()
    {
        textStart.enabled = false;
        imageStart.enabled = false;
    }
    public void ShowCofee()
    {
        double energy = 0.005; // Puissance en kW, temps en minutes et en kWh
        //double energy = Math.Ceiling(en * 100) / 100; // Arrondi au dixième supérieur
        energyText.text = energy+" KWh";    
        timeText.text = "6 min";
        pauseText.text = "1";
        maxPause = 1;
        timePauseText.text = "/40'";
        rentText.text = "15";
        taskText.text = "Cofee";
        imageStart.enabled = true;
        textStart.enabled = true;
        arduinoButon.winorfailText.enabled = false ;
    }
    public void ShowDishes()
    {
        double energy = 0.92;
        //double energy = Math.Ceiling(en * 10) / 10; // Arrondi au dixième supérieur
        energyText.text = energy + " KWh";
        timeText.text = "180 min";
        maxPause = 10;
        pauseText.text = "10";
        timePauseText.text = "/60'";
        rentText.text = "15";
        taskText.text = "Dishes (1 cycle)";
        imageStart.enabled = true;
        textStart.enabled = true;
        arduinoButon.winorfailText.enabled = false;
    }
    public void ShowWarm()
    {
        double en = 60 / (48*1.5*60/3);
        double energy = Math.Ceiling(en * 10) / 10; // Arrondi au dixième supérieur
        energyText.text = energy + " KWh";
        timeText.text = "60 min";
        pauseText.text = "5";
        timePauseText.text = "/40'";
        maxPause = 5;
        rentText.text = "15";
        taskText.text = "Cooking";
        imageStart.enabled = true;
        textStart.enabled = true;
        arduinoButon.winorfailText.enabled = false;
    }
    public void ShowWashing()
    {
        float energy = 0.9f; //enerie en kwh / nbre cycles
        //double energy = Math.Ceiling(en * 10) / 10; // Arrondi au dixième supérieur
        energyText.text = energy+" KWh";
        timeText.text = "180 min";
        maxPause = 10;
        pauseText.text = "10";
        timePauseText.text = "/50'";
        rentText.text = "15";
        taskText.text = "Washing (1 cycle)";
        imageStart.enabled = true;
        textStart.enabled = true;
        arduinoButon.winorfailText.enabled = false;
    }
}
