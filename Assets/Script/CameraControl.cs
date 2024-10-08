using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;
    [SerializeField]
    public GameObject magnifyingGlass;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    public float speed = 20;

    private float zoomSpeed = 20f;
    private float minFOV = 15f;
    private float defaultFOV;

    // Limites de mouvement de la cam�ra
    public float minX = 65f;
    public float maxX = 140f;
    public float minZ = 15f;
    public float maxZ = 140f;

    // Start is called before the first frame update
    void Start()
    {
        defaultFOV = m_Camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;

        HandleZoom();
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = transform.position + moveVelocity * Time.fixedDeltaTime;

        // Limiter les mouvements de la cam�ra
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        m_Camera.transform.position = newPosition;
        magnifyingGlass.transform.position = new Vector3(newPosition.x, newPosition.y + 20, newPosition.z + 20);
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        m_Camera.fieldOfView -= scroll * zoomSpeed;
        m_Camera.fieldOfView = Mathf.Clamp(m_Camera.fieldOfView, minFOV, defaultFOV);
    }
}
