using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public Button startGameButton;

    void Start()
    {
        startGameButton.onClick.AddListener(SetPlayerName);
    }

    void SetPlayerName()
    {
        string playerName = playerNameInput.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        
    }
}
