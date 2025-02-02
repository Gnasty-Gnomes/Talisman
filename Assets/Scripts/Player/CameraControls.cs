using UnityEngine;

public class CameraControls : MonoBehaviour
{    
    float m_xRotation;
    float m_yRotation;
    public Transform m_parentTransform;

    private void Start()
    {
        m_xRotation = transform.rotation.eulerAngles.x; 
        m_yRotation = transform.rotation.eulerAngles.y;
    }

    public void SetRotation(Vector3 rotation)
    {
        m_xRotation = rotation.x;
        m_yRotation = rotation.y;
        m_parentTransform.rotation = Quaternion.Euler(0, m_yRotation, 0);
        transform.localRotation = Quaternion.Euler(m_xRotation, 0, 0);
    }

    public void MoveCamera(Vector2 mouseMove, float sensitivity)
    {           
        m_xRotation -= mouseMove.y * Time.deltaTime * sensitivity;
        m_xRotation = Mathf.Clamp(m_xRotation, -90, 90);
        m_yRotation += mouseMove.x * Time.deltaTime * sensitivity;
        m_parentTransform.rotation = Quaternion.Euler( 0, m_yRotation, 0);   
        transform.localRotation = Quaternion.Euler(m_xRotation,0, 0);   
    }
}
