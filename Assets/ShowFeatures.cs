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
    public int energy;
    private float powerCofee = 1.5f;
    private float powerDishes = 2.4f;
    private float powerWashing = 2.5f;
    private float powerWarm = 1.2f;
    private float timeCofee = 5f;
    private float timeDishes = 90f;
    private float timeWashing = 90f;
    private float timeWarm = 3f;

    public void Start()
    {
        textStart.enabled = false;
        imageStart.enabled = false;
    }
    public void ShowCofee()
    {
        double en = powerCofee * timeCofee / 60;
        energy = (int)Math.Ceiling(en);
        energyText.text = energy.ToString();    
        timeText.text = timeCofee.ToString();
        rentText.text = "15";
        taskText.text = "Cofee";
        imageStart.enabled = true;
        textStart.enabled = true;
    }
    public void ShowDishes()
    {
        double en = powerDishes * timeDishes / 60;
        energy = (int)Math.Ceiling(en);
        energyText.text = energy.ToString();
        timeText.text = timeDishes.ToString();
        rentText.text = "15";
        taskText.text = "Dishes (1 cycle)";
        imageStart.enabled = true;
        textStart.enabled = true;
    }
    public void ShowWarm()
    {
        double en = powerWarm * timeWarm / 60;
        energy = (int)Math.Ceiling(en);
        energyText.text = energy.ToString();
        timeText.text = timeWarm.ToString();
        rentText.text = "15";
        taskText.text = "Cooking";
        imageStart.enabled = true;
        textStart.enabled = true;
    }
    public void ShowWashing()
    {
        double en = powerWashing * timeWashing / 60;
        energy = (int)Math.Ceiling(en);
        energyText.text = energy.ToString();
        timeText.text = timeWashing.ToString();
        rentText.text = "15";
        taskText.text = "Washing (1 cycle)";
        imageStart.enabled = true;
        textStart.enabled = true;
    }
    public int GetEnergy()
    {
        return energy; 
    }
}
