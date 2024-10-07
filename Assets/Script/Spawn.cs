using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    public Transform plane;

    [SerializeField]
    public GameObject m_Ant;
    [SerializeField]
    public GameObject m_Spider;
    [SerializeField]
    public GameObject m_Beetle;

    public float timer = 0.5f;

    public int maxInsectSpawn;
    private int currentInsectNumber;

    GameManager gameManager;

    float x_dim;
    float z_dim;

    // Start is called before the first frame update
    void Start()
    {
        currentInsectNumber = 0;
        gameManager = FindObjectOfType<GameManager>();
        x_dim = plane.GetComponent<MeshRenderer>().bounds.size.x;
        z_dim = plane.GetComponent<MeshRenderer>().bounds.size.z;
        x_dim /= 2;
        z_dim /= 2;

        StartCoroutine(SpawnInsectsWithInterval());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnInsectsWithInterval()
    {
        while (true)
        {
            if (currentInsectNumber < maxInsectSpawn)
            {
                SpawnRateInsect();
            }
            yield return new WaitForSeconds(timer);
        }
    }

    void SpawnRateInsect()
    {
        if (currentInsectNumber < maxInsectSpawn)
        {
            float rate = Random.Range(0.0f, 100.0f);
            Vector3 randomPosition = new Vector3(
                Random.Range(plane.position.x - x_dim, plane.position.x + x_dim),
                plane.position.y,
                Random.Range(plane.position.z - z_dim, plane.position.z + z_dim)
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas))
            {
                if (rate < 96) 
                {
                    SpawnInsect(m_Ant, hit);
                }
                else if (rate >= 96 && rate < 99)
                {
                    SpawnInsect(m_Spider, hit);
                }
                else if (rate >= 99)
                {
                    SpawnInsect(m_Beetle, hit);
                }
            }
        }
    }

    void SpawnInsect(GameObject insect, NavMeshHit hit)
    {
        var enemy = Instantiate(insect, hit.position, Quaternion.identity);
        enemy.transform.parent = gameObject.transform;
        currentInsectNumber++;
    }

    public void DecreaseInsectNumber()
    {
        currentInsectNumber--;
        if (currentInsectNumber < 0)
        {
            currentInsectNumber = 0;
        }
    }
}
