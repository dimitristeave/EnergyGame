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
    public Button startButton;

    public void GreenClick()
    {
        /*if (image.sprite == greenSprite)
        {
            arduinoButon.sliderGameObect.SetActive(true);
            arduinoButon.isTimerRunning = false;
            startButton.enabled = false;
            //image.sprite = redSprite;
            //text.text = "Stop";
            arduinoButon.OpenSerialPort();
            ArduinoButon.cumulativeEnergy = 0;

            // Enable pause and stop buttons
            arduinoButon.pauseImage.enabled = false;
            arduinoButon.stopButton.gameObject.SetActive(true);

            // Disable start button
            arduinoButon.startButton.gameObject.SetActive(false);
        }
        /*else
        {
            arduinoButon.isTimerRunning = false;
            image.sprite = greenSprite;
            text.text = "Start";
            image.enabled = false;
            text.enabled = false;
            arduinoButon.DestroyConnection();
        }*/
    }
}
