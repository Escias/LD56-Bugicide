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
    public GameObject Smoke;
    [SerializeField]
    public GameObject m_Dynamite;
    [SerializeField]
    public GameObject m_Explosion;
    [SerializeField]
    public LineRenderer lineRenderer;
    [SerializeField]
    public CameraTarget target;
    Spawn spawn;
    Vector3 pointToLook;
    GameObject pointObject;

    Coroutine c_Light;
    Coroutine c_Dynamite;

    bool startLight = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointToLook = target.GetPointToLook();
        pointObject = target.GetHitObject();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            c_Light = StartCoroutine(SkillLightCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            c_Dynamite = StartCoroutine(SkillDynamiteCoroutine());
        }
        if (startLight)
        {
            SkillLight();
        }
    }

    IEnumerator SkillLightCoroutine()
    {
        magnifyingGlass.SetActive(true);
        startLight = true;
        yield return new WaitForSeconds(5f);
        startLight = false;
        magnifyingGlass.SetActive(false);
        StopCoroutine(c_Light);
    }

    private void SkillLight()
    {
        magnifyingGlassLight.transform.LookAt(pointToLook);
        Smoke.transform.position = pointToLook;
        float distance = Vector3.Distance(magnifyingGlass.transform.position, pointToLook);
        magnifyingGlassLight.range = 200;
        magnifyingGlassLight.intensity = 5;
        lineRenderer.SetPosition(0, magnifyingGlass.transform.position);
        lineRenderer.SetPosition(1, pointToLook);
        lineRenderer.startWidth = 10;
        lineRenderer.endWidth = 1;
        CheckInsect("light");
    }

    IEnumerator SkillDynamiteCoroutine()
    {
        Vector3 spawnPosition = new Vector3(pointToLook.x, pointToLook.y + 50, pointToLook.z); // Position with y increased by 20
        Quaternion randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        GameObject dynamite = Instantiate(m_Dynamite, spawnPosition, randomRotation);
        yield return new WaitForSeconds(5f);
        Destroy(dynamite);
        GameObject explosion = Instantiate(m_Explosion, dynamite.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        yield return new WaitForSeconds(1.3f);
        Destroy(explosion);
        StopCoroutine(c_Dynamite);
    }

    private void CheckInsect(string skill)
    {
        try
        {
            if (pointObject.tag == "Ant" && skill == "light")
            {
                spawn = pointObject.transform.parent.gameObject.GetComponent<Spawn>();
                spawn.DecreaseInsectNumber();
                Destroy(pointObject);
            }
        }
        catch
        {

        }
    }
}
