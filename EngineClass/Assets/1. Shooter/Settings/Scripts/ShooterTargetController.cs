using System;
using UnityEngine;

public class ShooterTargetController : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private Single m_rotateSpeed;

    private Vector3 m_initialLocalPos;

    private void Awake()
    {
        m_initialLocalPos = m_target.localPosition;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, m_rotateSpeed * Time.deltaTime));

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_target.SetParent(null);
            m_target.localEulerAngles = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = 0;
            m_target.transform.position = worldPoint;
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            m_target.SetParent(transform);
            m_target.localEulerAngles = new Vector3(0, 0, 0);
            m_target.localPosition = m_initialLocalPos;
        }
    }
}
