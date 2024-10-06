using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    int health;
    bool isTakingDamage = false;

    Coroutine c_TakeDamageMagnifyingGlass;
    Coroutine c_TakeDamageDynamite;
    Coroutine c_TakeDamageWater;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            KillSpider();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MagnifyingGlass"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("MagnifyingGlass"))
        {
            c_TakeDamageMagnifyingGlass = StartCoroutine(TakeDamageOverTime(1));
        }
        else if (other.gameObject.CompareTag("Explosion"))
        {
            c_TakeDamageDynamite = StartCoroutine(TakeDamageOverTime(200));
        }
        else if (other.gameObject.CompareTag("Water"))
        {
            c_TakeDamageWater = StartCoroutine(TakeDamageOverTime(5));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MagnifyingGlass"))
        {
            StopCoroutine(c_TakeDamageMagnifyingGlass);
            isTakingDamage = false;
        }
        else if (other.gameObject.CompareTag("Explosion"))
        {
            StopCoroutine(c_TakeDamageDynamite);
            isTakingDamage = false;
        }
        else if (other.gameObject.CompareTag("Water"))
        {
            StopCoroutine(c_TakeDamageWater);
            isTakingDamage = false;
        }
    }

    // Coroutine to handle damage over time
    IEnumerator TakeDamageOverTime(int damage)
    {
        isTakingDamage = true;

        while (isTakingDamage)
        {
            health -= damage;
            Debug.Log("Spider health: " + health);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void KillSpider()
    {
        FindObjectOfType<ScoreManager>().AddScore(1);
        Destroy(transform.gameObject);
    }
}
