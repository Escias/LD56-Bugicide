using System.Collections;
using UnityEngine;

public class Spider : MonoBehaviour
{
    int health;
    bool isTakingDamage = false;

    Coroutine c_TakeDamageMagnifyingGlass;
    Coroutine c_TakeDamageWater;

    Material material;

    // Start is called before the first frame update
    void Start()
    {
        health = 200;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Explosion"))
        {
            TakeDamage(200);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("MagnifyingGlass"))
        {
            c_TakeDamageMagnifyingGlass = StartCoroutine(TakeDamageOverTime(1));
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
            TakeDamage(damage);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(ShowHitEffect());
    }

    public void KillSpider()
    {
        FindObjectOfType<ScoreManager>().AddScore(50);
        Destroy(transform.gameObject);
    }

    IEnumerator ShowHitEffect()
    {
        material = transform.gameObject.GetComponent<Renderer>().materials[0];
        material.SetFloat("_Blend", 1f);
        yield return new WaitForSeconds(0.2f);
        material.SetFloat("_Blend", 0f);
    }
}
