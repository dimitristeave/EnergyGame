using UnityEngine;
using UnityEngine.UI;

public class chauffage : MonoBehaviour
{
    public Button bouton; // Référence au bouton dans l'interface utilisateur

    void Start()
    {
        // Ajoutez un écouteur d'événements au bouton
        bouton.onClick.AddListener(AfficherCaracteristiquesTechniques);
    }

    private void AfficherCaracteristiquesTechniques()
    {
        // Remplacez "10KJ" par la valeur réelle que vous souhaitez afficher
        string message = "Il vous faut 10KJ pour effectuer cette action.";
        Debug.Log(message); // Affiche le message dans la console Unity

        // Vous pouvez également afficher le message dans une fenêtre de dialogue à l'écran
        // en utilisant les fonctionnalités de l'interface utilisateur de Unity
        // par exemple, en mettant à jour un texte sur un canvas.
    }
}
