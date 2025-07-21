using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public static class LookAtExtension 
{

    /*  TODO :: 벡터와 삼각함수를 사용하여 LookAt2D 함수를 구현한다.
     *  
     *  벡터와 삼각함수를 사용하면 유니티에서 기본적으로 제공하지 않는
     *  LookAt2D함수를 구현할 수 있다.
     *  
     *  벡터의 연산을 이용하여 현재 transform에서 목적 위치까지의 방향을 구하고, 
     *  그 방향에 맞는 각도를 구해 바꿔 회전에 적용하자.
     *  
     *  힌트.
     *  일반적인 삼각함수는 radian을 받고 값을 반환하지만,
     *  삼각함수의 역함수를 사용하면 값을 받아 radian을 반환한다.
     */

    /*  역 삼각함수 사용하기
     * 
     *  Mathf.Asin(), Mathf.Acos(), Mathf.Atan2() 함수를 사용하면 역 탄젠트 함수를 사용할 수 있다.
     *  단, 이 함수들은 radian 값을 반환하기 때문에 결과에 Mathf.Rad2Deg 값을 곱해줘야 각을 구할 수 있다.
     *  
     *  
     *  또, 각을 그대로 rotation에 적용시키려면
     *  Quaternion.Euler() 함수를 사용하면 된다.
     *  
     *  예를 들어, 물체를 30도 회전시키려면
     *  transform.rotation = Quaternion.Euler(0,0,30);
     */

    /* 유의사항
     * 
     * 이 프로젝트의 모든 스프라이트는 위(+Y방향)를 바라보고 있다.
     * 위의 과정에서 구한 각에 90을 빼줘야 원하는 LookAt을 구현할 수 있다.
     * 
     */


    /// <summary>
    /// 지정된 위치를 바라보게 한다. 위(+Y방향)를 바라보고 있는 스프라이트 기준
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="pTarget">바라보게 할 위치</param>
    public static void LookAt2D(this Transform transform,Vector2 pTarget) 
    {
        // 확장 메서드
        //다른 클래스에 메서드를 
        ///////////////////////////////////////////
        // 이곳에 코드를 작성
        Vector2 targetPos = pTarget - (Vector2)transform.position;
        float rotation = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation - 90);
        ///////////////////////////////////////////
    }
}
