using UnityEngine;
using UnityEngine.UI;

public class lessive : MonoBehaviour
{
    public Button bouton; // R�f�rence au bouton dans l'interface utilisateur

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

        // Vous pouvez �galement afficher le message dans une fen�tre de dialogue � l'�cran
        // en utilisant les fonctionnalit�s de l'interface utilisateur de Unity
        // par exemple, en mettant � jour un texte sur un canvas.
    }
}
