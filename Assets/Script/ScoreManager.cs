using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Référence vers le composant TextMeshPro
    private int score = 0; // Score initial
    public TimerUI timer; // Référence au script du TimerUI pour vérifier l'état du timer

    void Start()
    {
        // Initialisation du texte au score de départ
        UpdateScoreUI();
    }

    void Update()
    {
        // Pas besoin de faire des opérations dans Update sauf si c'est nécessaire pour autre chose.
        // La mise à jour de l'UI est déclenchée chaque fois que le score change.
    }

    // Fonction pour incrémenter le score
    public void AddScore(int amount)
    {
        // Vérifier si le timer est encore en cours
        if (timer != null && timer.IsTimerRunning()) // Si le timer est actif
        {
            score += amount; // Augmenter le score avec le montant passé en paramètre
            UpdateScoreUI(); // Mettre à jour l'UI après chaque modification du score
        }
    }

    // Fonction pour mettre à jour l'UI
    void UpdateScoreUI()
    {
        scoreText.text = score.ToString(); // Met à jour le texte avec la nouvelle valeur du score
    }
}
