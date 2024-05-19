using UnityEngine;
using UnityEngine.UI;

public class cafe : MonoBehaviour
{
    public Button bouton; // Référence au bouton dans l'interface utilisateur
    public Text messageText; // Référence au texte à afficher sur le Canvas

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

        // Mettre à jour le texte sur le Canvas avec le nouveau message
        messageText.text = message;
    }
}
