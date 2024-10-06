using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject magnifyingGlass;
    [SerializeField]
    public GameObject m_Water;
    [SerializeField]
    public Image skill1;
    [SerializeField]
    public Image skill2;
    [SerializeField]
    public Image skill3;

    // Start is called before the first frame update
    void Start()
    {
        magnifyingGlass.SetActive(false);
        m_Water.SetActive(false);
        SetImageColor(skill1, 255, 255, 255, 255);
        SetImageColor(skill2, 255, 255, 255, 255);
        SetImageColor(skill3, 100, 100, 100, 255);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetImageColor(Image image, float r, float g, float b, float a)
    {
        Color newColor = new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        image.color = newColor;
    }
}
