using System;
using System.Collections;
using UnityEngine;


public abstract class ShooterPattern : MonoBehaviour
{
    [SerializeField] private String m_defaultBullet;

    private Boolean m_isPatternRunning;

    /// <summary>
    /// 현재 위치에 탄막을 생성한다. 
    /// </summary>
    /// <param name="pPath">프리팹 경로를 지정한다. 지정하지 않은 경우 기본 경로로 설정된다.</param>
    /// <returns>생성된 탄막의 ShooterBullet 인스턴스</returns>
    protected ShooterBullet SpawnBullet(String pPath = null)
    {
        return SpawnBullet(transform.position,pPath);
    }

    /// <summary>
    /// 지정한 위치에 탄막을 생성한다. 
    /// </summary>
    /// <param name="pPosition">탄막을 생성할 위치를 지정한다.</param>
    /// <param name="pPath">프리팹 경로를 지정한다. 지정하지 않은 경우 기본 경로로 설정된다.</param>
    /// <returns>생성된 탄막의 ShooterBullet 인스턴스</returns>
    protected ShooterBullet SpawnBullet(Vector2 pPosition,String pPath = null)
    {
        if (pPath == null)
            pPath = m_defaultBullet;

        ShooterBullet bulletPrefab = Resources.Load<ShooterBullet>($"Prefabs/{pPath}");
        ShooterBullet bulletInstance = GameObject.Instantiate(bulletPrefab);
        bulletInstance.transform.position = pPosition;

        return bulletInstance;
    }

    public void Shoot()
    {
        if (m_isPatternRunning)
            return;

        StartCoroutine(PatternRoutine());
    }

    private IEnumerator PatternRoutine()
    {
        m_isPatternRunning = true;
        yield return StartCoroutine(Pattern());
        m_isPatternRunning = false;
    }

    protected abstract IEnumerator Pattern();


    /// <summary>
    /// 플레이어의 위치를 찾아낸다.
    /// </summary>
    /// <returns>찾아낸 플레이어의 GameObject (찾지 못했을 경우 null)</returns>
    protected GameObject FindPlayer()
    {
        return GameObject.FindObjectOfType<ShooterPlayerController>()?.gameObject;
    }


    /// <summary>
    /// 타겟의 위치를 찾아낸다.
    /// </summary>
    /// <returns>찾아낸 타겟의 GameObject (찾지 못했을 경우 null)</returns>
    protected GameObject FindTarget()
    {
        return GameObject.Find("Target");
    }
}
