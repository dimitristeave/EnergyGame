using UnityEngine;
using UnityEngine.UI;

public class fenlessive : MonoBehaviour
{
    // Référence à la fenêtre que vous voulez afficher
    public GameObject windowToDisplay;

    void Start()
    {
        // Assurez-vous que la fenêtre est désactivée au démarrage
        windowToDisplay.SetActive(false);
    }

    // Fonction appelée lorsque le bouton est cliqué
    public void OnButtonClick()
    {
        // Activer la fenêtre pour l'afficher
        windowToDisplay.SetActive(true);
    }
}
