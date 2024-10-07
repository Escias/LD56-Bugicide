using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;
    

    private Vector3 pointToLook;
    private GameObject hitObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray cameraRay = m_Camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraRay, out hit, rayLength))
            {
                pointToLook = hit.point;
                hitObject = hit.collider.gameObject;
            }
        }
    }

    public Vector3 GetPointToLook()
    {
        return pointToLook;
    }

    public GameObject GetHitObject()
    {
        return hitObject;
    }
}
