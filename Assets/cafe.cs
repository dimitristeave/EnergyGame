using UnityEngine;
using UnityEngine.UI;

public class cafe : MonoBehaviour
{
    public Button bouton; // R�f�rence au bouton dans l'interface utilisateur
    public Text messageText; // R�f�rence au texte � afficher sur le Canvas

    void Start()
    {
        // Ajoutez un �couteur d'�v�nements au bouton
        bouton.onClick.AddListener(AfficherCaracteristiquesTechniques);
    }

    private void AfficherCaracteristiquesTechniques()
    {
        // Remplacez "10KJ" par la valeur r�elle que vous souhaitez afficher
        string message = "Il vous faut 10KJ pour effectuer cette action.";
        Debug.Log(message); // Affiche le message dans la console Unity

        // Mettre � jour le texte sur le Canvas avec le nouveau message
        messageText.text = message;
    }
}
