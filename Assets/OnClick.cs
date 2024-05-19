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
            //arduinoButon.energySlider.fillRect.GetComponent<Image>().enabled=false;
            //arduinoButon.energySlider.value = 0;
            arduinoButon.energySlider.SetValueWithoutNotify(arduinoButon.energyPercentage);
            arduinoButon.isTimerRunning = false;
            image.sprite = redSprite;
            text.text = "Stop";
            arduinoButon.OpenSerialPort();
        }
        else
        {
            arduinoButon.energySlider.value = 0;
            arduinoButon.energySlider.SetValueWithoutNotify(0);
            arduinoButon.isTimerRunning = false;
            image.sprite= greenSprite;
            text.text = "Start";
            image.enabled = false;
            text.enabled = false;
            arduinoButon.DestroyConnection();
        }
    }
}
