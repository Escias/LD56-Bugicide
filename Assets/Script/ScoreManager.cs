using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // R�f�rence vers le composant TextMeshPro
    private int score = 0; // Score initial
    public TimerUI timer; // R�f�rence au script du TimerUI pour v�rifier l'�tat du timer

    void Start()
    {
        // Initialisation du texte au score de d�part
        UpdateScoreUI();
    }

    void Update()
    {
        // Pas besoin de faire des op�rations dans Update sauf si c'est n�cessaire pour autre chose.
        // La mise � jour de l'UI est d�clench�e chaque fois que le score change.
    }

    // Fonction pour incr�menter le score
    public void AddScore(int amount)
    {
        // V�rifier si le timer est encore en cours
        if (timer != null && timer.IsTimerRunning()) // Si le timer est actif
        {
            score += amount; // Augmenter le score avec le montant pass� en param�tre
            UpdateScoreUI(); // Mettre � jour l'UI apr�s chaque modification du score
        }
    }

    // Fonction pour mettre � jour l'UI
    void UpdateScoreUI()
    {
        scoreText.text = score.ToString(); // Met � jour le texte avec la nouvelle valeur du score
    }
}
