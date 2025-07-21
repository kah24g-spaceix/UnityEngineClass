using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Single m_rotationTime;
    private CharacterController m_characterController;
    private Single m_rotationVelocity;

    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
    }
    public void Move(Vector3 pMovement)
    {
        m_characterController.Move(pMovement * Time.deltaTime);

        Single targetRotation = Mathf.Atan2(
            pMovement.x,
            pMovement.z) * Mathf.Rad2Deg;

        Single dampedRotation = Mathf.SmoothDampAngle(
            transform.localEulerAngles.y,
            targetRotation,
            ref m_rotationVelocity,
            m_rotationTime);
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            dampedRotation,
            transform.localEulerAngles.z);
    }
}