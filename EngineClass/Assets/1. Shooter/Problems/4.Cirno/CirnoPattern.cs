using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirnoPattern : ShooterPattern
{
    private static readonly Int32 BULLET_COUNT = 30;

    /*  TODO :: 지금까지 한 걸 모두 동원해 동방프로젝트의 캐릭터 "치르노"의 패턴을 최대한 비슷하게 모작해보자
     * 
     *  치르노의 패턴은 다음과 같이 구현하면 비슷하게 구현할 수 있다.
     *  
     *  1. 원 모양으로 탄막을 발사한다. 
     *  2. n초 대기
     *  3. 발사된 탄막들을 그 자리에서 모두 다시 적을 향하게 하여 더 빠르게 발사한다.
     *  
     *  ※ 발사된 탄막들은 항상 탄막이 향하는 방향을 바라보고 있어야 한다.
     *  
     *  ※ 발사한 탄막들을 n초 후에 다시 관리해야 하므로, 발사한 탄막들을 List 등의 콜렉션에 담고 있어야 한다.
     *     이 코드에서는 List에 담아보자.
     *     
     *  ※ 탄막이 바라보는 방향이 잘 보여야 하므로, 이 코드에서 SpawnBullet()을 사용하면
     *     이전과 다른 형태의 탄막이 발사된다.
     *     
     *  힌트.
     *  지금까지 작성한 코드들을 모두 사용한다. 
     *  몇몇 코드는 코드 복붙도 가능하다!
     */

    /*  사용 가능한 함수 설명
     * 
     *  ShooterPattern :: SpawnBullet() 
     *  현재 위치에 탄막을 생성한다. 생성된 탄막을 반환
     *      반환형 
     *      - ShooterBullet
     *      
     *  ShooterPattern :: FindTarget() 
     *  타겟을 찾아낸다. 찾아낸 타겟을 반환 (찾지 못했을 경우 null)
     *      반환형 
     *      - GameObject
     * 
     *  ShooterBullet :: SetVelocity()
     *  탄막의 velocity값을 설정한다. 
     *      매개변수 
     *      - velocity: Vector2 
    */


    //이곳에 발사된 탄막을 담는다.
    //패턴은 여러번 재사용 가능해야 하므로 패턴 시작 시 Clear()를 호출해 초기화하는것을 잊지 말 것.
    private List<ShooterBullet> m_bullets = new List<ShooterBullet>();

    /// <summary>
    /// 상위 클래스에서 패턴 실행이 호출되면 이 코루틴이 실행된다
    /// </summary>
    protected override IEnumerator Pattern()
    {
        ///////////////////////////////////////////
        // 이곳에 코드를 작성
        m_bullets.Clear();
        GameObject target = FindTarget();
        float n = 1;
        int round = 360;
        int radius = round / BULLET_COUNT;
        for (int i = 0; i < BULLET_COUNT; i++)
        {
            ShooterBullet bullet = SpawnBullet();
            float cos = Mathf.Cos(i * radius * Mathf.Deg2Rad);
            float sin = Mathf.Sin(i * radius * Mathf.Deg2Rad);

            Vector2 bulletRotate = new Vector2(cos, sin);
            Vector2 bulletVelocity = bulletRotate.normalized * 5;
            m_bullets.Add(bullet);
            bullet.transform.LookAt2D((Vector2)bullet.transform.position + bulletVelocity);
            bullet.SetVelocity(bulletVelocity);
            
        }
        
        yield return new WaitForSeconds(n);
        foreach (ShooterBullet m_bullet in m_bullets)
        {
            
            m_bullet.SetVelocity(new Vector2(0, 0));
        }
        yield return new WaitForSeconds(n);
        foreach (ShooterBullet m_bullet in m_bullets)
        {
            m_bullet.transform.LookAt2D(target.transform.position);
            Vector2 bulletPos = target.transform.position - m_bullet.transform.position;
            Vector2 bulletVelocity = bulletPos.normalized * 10;

            m_bullet.SetVelocity(bulletVelocity);
        }
        ///////////////////////////////////////////
        yield break;
    }
}
