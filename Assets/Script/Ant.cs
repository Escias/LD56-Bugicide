using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Explosion") || other.gameObject.CompareTag("Water"))
        {
            Spawn spawn = transform.parent.gameObject.GetComponent<Spawn>();
            spawn.DecreaseInsectNumber();
            KillAnt();
        }
    }

    public void KillAnt()
    {
        FindObjectOfType<ScoreManager>().AddScore(1);
        Destroy(transform.gameObject);
    }
}
