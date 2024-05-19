using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneToLoad; // Le nom de la sc�ne � charger

    // Fonction pour charger une nouvelle sc�ne
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
