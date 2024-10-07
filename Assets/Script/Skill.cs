using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

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
    public GameObject m_Water;
    [SerializeField]
    public GameObject water;
    [SerializeField]
    public GameObject waterZone;
    [SerializeField]
    public LineRenderer lineRenderer;
    [SerializeField]
    public CameraTarget target;
    [SerializeField]
    public GameObject m_gameManager;
    Spawn spawn;
    TimerUI timerUI;
    GameManager gameManager;
    Vector3 pointToLook;
    GameObject pointObject;

    Coroutine c_Light;
    Coroutine c_Dynamite;
    Coroutine c_Water;

    bool startLight = false;
    bool startWater = false;
    bool day;
    bool skillLightActive = false;
    bool skillDynamiteActive = false;
    bool skillWaterActive = false;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = m_gameManager.GetComponent<GameManager>();
        timerUI = gameManager.GetComponent<TimerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerUI.IsTimerRunning())
        {
            pointToLook = target.GetPointToLook();
            pointObject = target.GetHitObject();
            if (Input.GetKeyDown(KeyCode.Alpha1) && !skillLightActive)
            {
                c_Light = StartCoroutine(SkillLightCoroutine());
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && !skillDynamiteActive)
            {
                c_Dynamite = StartCoroutine(SkillDynamiteCoroutine());
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && !skillWaterActive)
            {
                c_Water = StartCoroutine(SkillWaterCoroutine());
            }
            if (startLight)
            {
                SkillLight();
            }
            if (startWater)
            {
                SkillWater();
            }
        }
    }

    IEnumerator SkillLightCoroutine()
    {
        skillLightActive = true;
        gameManager.SetImageColor(gameManager.skill1, 100, 100, 100, 255);
        magnifyingGlass.SetActive(true);
        startLight = true;
        yield return new WaitForSeconds(5f);
        startLight = false;
        magnifyingGlass.SetActive(false);
        yield return new WaitForSeconds(1f);
        skillLightActive = false;
        gameManager.SetImageColor(gameManager.skill1, 255, 255, 255, 255);
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
    }

    IEnumerator SkillDynamiteCoroutine()
    {
        skillDynamiteActive = true;
        gameManager.SetImageColor(gameManager.skill2, 100, 100, 100, 255);
        Vector3 spawnPosition = new Vector3(pointToLook.x, pointToLook.y + 50, pointToLook.z); // Position with y increased by 20
        Quaternion randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        GameObject dynamite = Instantiate(m_Dynamite, spawnPosition, randomRotation);
        yield return new WaitForSeconds(5f);
        Destroy(dynamite);
        GameObject explosion = Instantiate(m_Explosion, dynamite.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        yield return new WaitForSeconds(0.01f);
        SphereCollider sphereCollider = explosion.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(1.3f);
        Destroy(explosion);
        yield return new WaitForSeconds(3f);
        skillDynamiteActive = false;
        gameManager.SetImageColor(gameManager.skill2, 255, 255, 255, 255);
        StopCoroutine(c_Dynamite);
    }

    IEnumerator SkillWaterCoroutine()
    {
        skillWaterActive = true;
        gameManager.SetImageColor(gameManager.skill3, 100, 100, 100, 255);
        m_Water.SetActive(true);
        SphereCollider sphereCollider = m_Water.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        startWater = true;
        ParticleSystem waterParticleSystem = water.GetComponent<ParticleSystem>();
        ParticleSystem waterZoneParticleSystem = waterZone.GetComponent<ParticleSystem>();
        var emissionWater = waterParticleSystem.emission;
        var emissionWaterZone = waterZoneParticleSystem.emission;
        emissionWater.rateOverTime = 20f;
        emissionWaterZone.rateOverTime = 0f;
        yield return new WaitForSeconds(3.5f);
        emissionWaterZone.rateOverTime = 500f;
        sphereCollider.enabled = true;
        yield return new WaitForSeconds(10f);
        startWater = false;
        emissionWater.rateOverTime = 0f;
        yield return new WaitForSeconds(3.5f);
        emissionWaterZone.rateOverTime = 0f;
        m_Water.SetActive(false);
        yield return new WaitForSeconds(2f);
        skillWaterActive = false;
        gameManager.SetImageColor(gameManager.skill3, 255, 255, 255, 255);
        StopCoroutine(c_Water);
    }

    private void SkillWater()
    {
        m_Water.transform.position = pointToLook;
        water.transform.position = new Vector3(m_Water.transform.position.x - 1, m_Water.transform.position.y + 88, m_Water.transform.position.z);
        waterZone.transform.position = m_Water.transform.position;
    }
}
