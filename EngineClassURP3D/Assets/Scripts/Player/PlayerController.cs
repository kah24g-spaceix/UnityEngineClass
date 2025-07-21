using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Single m_reloadTime;
    [SerializeField] private Single m_reloadAnimationTime;

    [SerializeField] private Single m_rotateTime;

    [SerializeField] private Single m_walkSpeed;
    [SerializeField] private Single m_runSpeed;
    [SerializeField] private Single m_accel;

    [SerializeField] private Single m_cameraTopClamp;
    [SerializeField] private Single m_cameraBottomClamp;
    [SerializeField] private GameObject m_cameraTarget;

    private Single m_currentSpeed;
    private Single m_currentHorizSpeed;
    private Single m_currentVertSpeed;

    private Single m_rotationVelocity;

    private Single m_cameraTargetYaw;
    private Single m_cameraTargetPitch;

    private Boolean m_isArmed;
    private Single m_armedValue;

    private Boolean m_isAiming;
    private Single m_aimingValue;

    private Animator m_animator;
    private CharacterController m_characterController;


    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_characterController = GetComponent<CharacterController>();

        m_cameraTargetYaw 
            = m_cameraTarget.transform.eulerAngles.y;
        m_cameraTargetPitch 
            = m_cameraTarget.transform.eulerAngles.x;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        Single mouseX = Input.GetAxisRaw("Mouse X");
        Single mouseY = - Input.GetAxisRaw("Mouse Y");

        m_cameraTargetPitch += mouseY;
        m_cameraTargetYaw += mouseX;

        m_cameraTargetPitch = Mathf.Clamp(
            m_cameraTargetPitch,
            m_cameraBottomClamp,
            m_cameraTopClamp
            );

        m_cameraTarget.transform.rotation
            = Quaternion.Euler(
                m_cameraTargetPitch, 
                m_cameraTargetYaw, 
                0);
    }

    private void Update()
    {
        if (m_isArmed && Input.GetKeyDown(KeyCode.R))
        {
            Single reloadSpeed = m_reloadAnimationTime / m_reloadTime;
            m_animator.SetFloat("reloadSpeed", reloadSpeed);
            m_animator.SetTrigger("reload");
        }
        if(Input.GetKeyDown(KeyCode.F))
            m_isArmed = !m_isArmed;

        m_armedValue = Mathf.Lerp(
            m_armedValue,
            m_isArmed ? 1 : 0,
            10 * Time.deltaTime
            );
        m_animator.SetFloat("armed", m_armedValue);

        if (Input.GetKeyDown(KeyCode.Mouse1) && m_isArmed)
            m_isAiming = true;

        if (Input.GetKeyUp(KeyCode.Mouse1))
            m_isAiming = false;

        m_aimingValue = Mathf.Lerp(
            m_aimingValue,
            m_isAiming ? 1 : 0,
            10 * Time.deltaTime);

        m_animator.SetFloat("aiming", m_aimingValue);

        if (m_isAiming)
            m_cameraTarget.transform.localPosition =
                new Vector3(0.5f, 0, 0);
        else
            m_cameraTarget.transform.localPosition =
                new Vector3(0, 0, 0);


        Single horiz = Input.GetAxisRaw("Horizontal");
        Single vert = Input.GetAxisRaw("Vertical");

        Boolean isRunning 
            = Input.GetKey(KeyCode.LeftShift) && !m_isAiming;

        Vector3 input = new Vector3(horiz, 0, vert);

        m_animator.SetFloat("speed", m_currentSpeed);

        m_animator.SetFloat("speedHoriz", m_currentHorizSpeed);
        m_animator.SetFloat("speedVert", m_currentVertSpeed);

        m_currentHorizSpeed = Mathf.Lerp(
            m_currentHorizSpeed,
            horiz,
            10 * Time.deltaTime);

        m_currentVertSpeed = Mathf.Lerp(
            m_currentVertSpeed,
            vert,
            10 * Time.deltaTime);

        if (m_isAiming)
            transform.localEulerAngles
                = new Vector3(transform.localEulerAngles.x,
                m_cameraTarget.transform.eulerAngles.y,
                transform.localEulerAngles.z);

        //magnitude : 크기
        //sqrMagnitude : 루트 안씌운 크기
        if (input.sqrMagnitude == 0)
        {
            m_currentSpeed -= m_accel * Time.deltaTime;
            if (m_currentSpeed <= 0)
                m_currentSpeed = 0;

            return;
        }

        Single maxSpeed = isRunning ? m_runSpeed : m_walkSpeed;

        if(m_currentSpeed <= maxSpeed)
        {
            m_currentSpeed += m_accel * Time.deltaTime;
            m_currentSpeed 
                = Mathf.Clamp(m_currentSpeed, 0, maxSpeed);
            //현재값 최소 최대
            //현재 값이 최소보다 작으면 최소로 고정
            // 최대보다 크면 최대로 고정
        }
        else //최대 속도보다 크다?
        //달리고 잇다가 달리기에서 떼는데 여전히 앞으로 가고있는 상황
        {
            m_currentSpeed -= m_accel * Time.deltaTime;
            m_currentSpeed
                = Mathf.Clamp(m_currentSpeed, 0, Single.MaxValue);
        }

        Single targetRotation = Mathf.Atan2(
            input.x,
            input.z
            ) * Mathf.Rad2Deg 
            + m_cameraTarget.transform.eulerAngles.y; //

        Vector3 targetDirection
            = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;

        m_characterController.Move(
            targetDirection * m_currentSpeed * Time.deltaTime);

        Single dampedRotation = Mathf.SmoothDampAngle(
            transform.localEulerAngles.y,
            targetRotation,
            ref m_rotationVelocity,
            m_rotateTime
            );

        if(!m_isAiming)
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            dampedRotation,
            transform.localEulerAngles.z);
    }
}
