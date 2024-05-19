using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneToLoad; // Le nom de la scène à charger

    // Fonction pour charger une nouvelle scène
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
