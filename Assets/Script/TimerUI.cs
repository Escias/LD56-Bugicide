using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    GameObject endFrame;

    public TextMeshProUGUI timerText; // Référence vers le composant TextMeshPro
    public float startTime = 120f; // Temps de départ en secondes
    private float timeRemaining = 120f; // Timer initialisé à 2 minutes (120 secondes)
    private bool timerIsRunning = false; // Contrôle si le timer tourne
    private bool isFlashing = false; // Pour contrôler le clignotement
    public Color normalColor = Color.white; // Couleur normale du texte
    public Color warningColor = Color.red; // Couleur de l'alerte (rouge)
    private float flashInterval = 0.5f; // Intervalle du clignotement (demi-seconde)

    void Start()
    {
        // Démarrer le timer
        timerIsRunning = true;
        timeRemaining = startTime;
        timerText.color = normalColor;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Compte à rebours
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI(timeRemaining);

                // Déclencher le clignotement si moins de 10 secondes restantes
                if (timeRemaining <= 10 && !isFlashing)
                {
                    isFlashing = true;
                    InvokeRepeating("FlashWarning", 0, flashInterval); // Commence le clignotement
                }
            }
            else
            {
                // Lorsque le temps est écoulé
                timeRemaining = 0;
                timerIsRunning = false;
                endFrame.SetActive(true);
                UpdateTimerUI(timeRemaining);
                CancelInvoke("FlashWarning"); // Arrêter le clignotement
                timerText.color = warningColor; // Fixer la couleur rouge quand le temps est écoulé
            }
        }
    }

    // Fonction pour mettre à jour l'UI
    void UpdateTimerUI(float currentTime)
    {
        // Convertir le temps en minutes et secondes
        //currentTime += 1; // Ajouter 1 pour éviter d'afficher -1 à 0 seconde
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Mettre à jour le texte avec le format mm:ss
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Fonction pour clignoter la couleur de l'UI
    void FlashWarning()
    {
        if (timerText.color == normalColor)
        {
            timerText.color = warningColor; // Passer au rouge
        }
        else
        {
            timerText.color = normalColor; // Revenir à la couleur normale
        }
    }

    // Méthode pour savoir si le timer est en cours
    public bool IsTimerRunning()
    {
        return timerIsRunning;
    }
}
