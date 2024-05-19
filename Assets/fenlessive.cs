using UnityEngine;
using UnityEngine.UI;

public class fenlessive : MonoBehaviour
{
    // R�f�rence � la fen�tre que vous voulez afficher
    public GameObject windowToDisplay;

    void Start()
    {
        // Assurez-vous que la fen�tre est d�sactiv�e au d�marrage
        windowToDisplay.SetActive(false);
    }

    // Fonction appel�e lorsque le bouton est cliqu�
    public void OnButtonClick()
    {
        // Activer la fen�tre pour l'afficher
        windowToDisplay.SetActive(true);
    }
}
