using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    GameObject endFrame;

    public TextMeshProUGUI timerText; // R�f�rence vers le composant TextMeshPro
    public float startTime = 120f; // Temps de d�part en secondes
    private float timeRemaining = 120f; // Timer initialis� � 2 minutes (120 secondes)
    private bool timerIsRunning = false; // Contr�le si le timer tourne
    private bool isFlashing = false; // Pour contr�ler le clignotement
    public Color normalColor = Color.white; // Couleur normale du texte
    public Color warningColor = Color.red; // Couleur de l'alerte (rouge)
    private float flashInterval = 0.5f; // Intervalle du clignotement (demi-seconde)

    void Start()
    {
        // D�marrer le timer
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
                // Compte � rebours
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI(timeRemaining);

                // D�clencher le clignotement si moins de 10 secondes restantes
                if (timeRemaining <= 10 && !isFlashing)
                {
                    isFlashing = true;
                    InvokeRepeating("FlashWarning", 0, flashInterval); // Commence le clignotement
                }
            }
            else
            {
                // Lorsque le temps est �coul�
                timeRemaining = 0;
                timerIsRunning = false;
                endFrame.SetActive(true);
                UpdateTimerUI(timeRemaining);
                CancelInvoke("FlashWarning"); // Arr�ter le clignotement
                timerText.color = warningColor; // Fixer la couleur rouge quand le temps est �coul�
            }
        }
    }

    // Fonction pour mettre � jour l'UI
    void UpdateTimerUI(float currentTime)
    {
        // Convertir le temps en minutes et secondes
        //currentTime += 1; // Ajouter 1 pour �viter d'afficher -1 � 0 seconde
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Mettre � jour le texte avec le format mm:ss
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
            timerText.color = normalColor; // Revenir � la couleur normale
        }
    }

    // M�thode pour savoir si le timer est en cours
    public bool IsTimerRunning()
    {
        return timerIsRunning;
    }
}
