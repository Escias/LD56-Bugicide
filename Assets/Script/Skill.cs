using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    public Light magnifyingGlassLight;
    [SerializeField]
    public GameObject magnifyingGlass;
    [SerializeField]
    public LineRenderer lineRenderer;
    [SerializeField]
    public CameraTarget target;
    Vector3 pointToLook;

    Coroutine c_Light;
    bool startLight = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointToLook = target.pointToLook;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            c_Light = StartCoroutine(SkillLight());
            FindObjectOfType<ScoreManager>().AddScore(10); // TEST : ajouter 10 points
        }
        if (startLight)
        {
            magnifyingGlassLight.transform.LookAt(pointToLook);
            float distance = Vector3.Distance(magnifyingGlass.transform.position, pointToLook);
            magnifyingGlassLight.range = 200;
            magnifyingGlassLight.intensity = 5;
            lineRenderer.SetPosition(0, magnifyingGlass.transform.position);
            lineRenderer.SetPosition(1, pointToLook);
            lineRenderer.startWidth = 10;
            lineRenderer.endWidth = 1;
        }
    }

    IEnumerator SkillLight()
    {
        magnifyingGlass.SetActive(true);
        startLight = true;
        yield return new WaitForSeconds(5f);
        startLight = false;
        magnifyingGlass.SetActive(false);
        StopCoroutine(c_Light);
    }
}
