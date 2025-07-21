using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissilePattern : ShooterPattern
{
    /*  TODO :: 벡터 연산을 이용하여 적을 향해 날아가는 탄막을 발사한다
     * 
     *  CirclePattern 클래스는 ShooterPattern 클래스를 상속받았기 때문에
     *  
     *  SpawnBullet(), FindTarget() 함수를 호출할 수 있다.
     *  FindTarget() 함수를 호출하여 타겟을 찾아내고,
     *  벡터 연산을 통해 찾아낸 타겟까지의 방향을 구해 velocity를 구한다.
     *  
     *  SpawnBullet() 함수를 호출하여 현재 위치에 탄막을 생성하고, 
     *  반환된 ShooterBullet 인스턴스에서 SetVelocity() 함수를 호출하여
     *  velocity 값을 설정하자.
     *  
     *  ※ Target이 움직여도 탄막이 발사되었을때 당시의 위치로만 발사되면 된다.
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
     * 
    */

    /// <summary>
    /// 상위 클래스에서 패턴 실행이 호출되면 이 코루틴이 실행된다
    /// </summary>
    protected override IEnumerator Pattern()
    {
        ///////////////////////////////////////////
        // 이곳에 코드를 작성
        ShooterBullet bullet = SpawnBullet();
        GameObject target = FindTarget();
        bullet.SetVelocity(new Vector2(0, 1));

        Vector2 bulletPos 
            = target.transform.position - bullet.transform.position;
        Vector2 bulletVelocity
            = bulletPos.normalized * 10;

        bullet.SetVelocity(bulletVelocity);
        ///////////////////////////////////////////
        yield break;
    }
}
