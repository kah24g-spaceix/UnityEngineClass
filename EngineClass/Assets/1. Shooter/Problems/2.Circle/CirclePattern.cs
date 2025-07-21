using System;
using System.Collections;
using UnityEngine;

public class CirclePattern : ShooterPattern
{
    private static readonly Int32 BULLET_COUNT = 30;


    /*  TODO :: 삼각함수를 사용하여 원 모양으로 탄막을 발사한다. 
     *  탄막의 수는 위의 BULLET_COUNT를 사용한다.
     * 
     *  CirclePattern 클래스는 ShooterPattern 클래스를 상속받았기 때문에
     *  SpawnBullet() 함수를 호출할 수 있다.
     *  SpawnBullet() 함수를 호출하여 현재 위치에 탄막을 생성하고, 
     *  반환된 ShooterBullet 인스턴스에서 SetVelocity() 함수를 호출하여
     *  velocity 값을 설정하자.
     * 
     *  속도는 삼각함수를 이용하여 구한다. 
     */

    /*  삼각함수 사용하기
     * 
     *  Mathf.Cos(), Mathf.Sin(), Mathf.Tan()  함수를 이용하면 삼각함수를 사용할 수 있다.
     *  단, 이 함수들의 매개변수는 radian이기 때문에 각에 Mathf.Deg2Rad을 곱한 값을 넣어줘야 한다.
     *  
     *  사용예시
     *  float angle = 60;
     *  float rad = 60 * Mathf.Deg2Rad;
     *  Single cos60 = Mathf.Cos(rad);
     *  
     */

    /*  사용 가능한 함수 설명
     * 
     *  ShooterPattern :: SpawnBullet() 
     *  현재 위치에 탄막을 생성한다. 생성된 탄막을 반환
     *      반환형 
     *      - ShooterBullet
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
        int round = 360;
        int radius = round / BULLET_COUNT;
        for (int i = 0; i < BULLET_COUNT; i++)
        {
            ShooterBullet bullet = SpawnBullet();
            float cos = Mathf.Cos(i * radius * Mathf.Deg2Rad);
            float sin = Mathf.Sin(i * radius * Mathf.Deg2Rad);

            Vector2 bulletPos = new Vector2(cos, sin);
            Vector2 bulletVelocity = bulletPos.normalized * 2;

            bullet.SetVelocity(bulletVelocity);
        }
        
        ///////////////////////////////////////////
        yield break;
    }
}
