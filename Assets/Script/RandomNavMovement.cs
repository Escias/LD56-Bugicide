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
        timer += Time.deltaTime; // Incr�mente le timer

        if (timer >= wanderTimer)
        {
            // Choisit une nouvelle position al�atoire dans le rayon sp�cifi�
            Vector3 newPosition = RandomNavSphere(transform.position, wanderRadius, -1);

            // V�rifie si la nouvelle position est valide et sur le NavMesh
            if (NavMesh.SamplePosition(newPosition, out NavMeshHit navHit, wanderRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(newPosition);
            }
            else
            {
                // Invert the position if it's not valid on the NavMesh
                Vector3 invertedPosition = transform.position - (newPosition - transform.position);
                agent.SetDestination(invertedPosition);
            }

            timer = 0; // R�initialise le timer
        }
    }

    private Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        // G�n�re une position al�atoire dans une sph�re autour de l'origine
        Vector3 randDirection = Random.insideUnitSphere * distance;
        randDirection += origin;

        // Cette partie v�rifie si la position est valide
        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, distance, layermask);
        return navHit.position; // Retourne la position navigable la plus proche
    }
}
