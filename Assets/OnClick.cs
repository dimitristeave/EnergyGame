using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnClick : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Sprite greenSprite;
    public Sprite redSprite;
    public Image image;
    public ArduinoButon arduinoButon;

    public void GreenClick()
    {
        if (image.sprite == greenSprite) 
        {
            arduinoButon.sliderGameObect.SetActive(true);
            arduinoButon.isTimerRunning = false;
            image.sprite = redSprite;
            text.text = "Stop";
            arduinoButon.OpenSerialPort();
            ArduinoButon.cumulativeEnergy = 0;
            //arduinoButon.timerText.text = ArduinoButon.timeText;
        }
        else
        {
            //arduinoButon.energySlider.value = 0;
            //arduinoButon.energySlider.SetValueWithoutNotify(0);
            arduinoButon.isTimerRunning = false;
            image.sprite= greenSprite;
            text.text = "Start";
            image.enabled = false;
            text.enabled = false;
            arduinoButon.DestroyConnection();
        }
    }
}
