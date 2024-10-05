using UnityEngine;
using UnityEngine.AI;

public class RandomNavMovement : MonoBehaviour
{
    public float wanderRadius = 10f; // Rayon de mouvement
    public float wanderTimer = 5f; // Intervalle entre les mouvements

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer; // Initialiser le timer
    }

    void Update()
    {
        timer += Time.deltaTime; // Incrémente le timer

        if (timer >= wanderTimer)
        {
            // Choisit une nouvelle position aléatoire dans le rayon spécifié
            Vector3 newPosition = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPosition); // Définit la destination de l'agent
            timer = 0; // Réinitialise le timer
        }
    }

    private Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        // Génère une position aléatoire dans une sphère autour de l'origine
        Vector3 randDirection = Random.insideUnitSphere * distance;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, distance, layermask);
        return navHit.position; // Retourne la position navigable
    }
}
