using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    public Transform plane;

    [SerializeField]
    public GameObject m_Insect;

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
        while (currentInsectNumber < maxInsectSpawn)
        {
            SpawnInsect();
            yield return new WaitForSeconds(timer);
        }
    }

    void SpawnInsect()
    {
        if (currentInsectNumber < maxInsectSpawn)
        {
            var enemy = Instantiate(m_Insect, new Vector3(Random.Range(plane.position.x - x_dim, plane.position.x + x_dim), 0.2f, Random.Range(plane.position.z - z_dim, plane.position.z + z_dim)), Quaternion.identity);
            enemy.transform.parent = gameObject.transform;
            currentInsectNumber++;
        }
    }
}
