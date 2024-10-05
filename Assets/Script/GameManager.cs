using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject magnifyingGlass;

    // Start is called before the first frame update
    void Start()
    {
        magnifyingGlass.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
