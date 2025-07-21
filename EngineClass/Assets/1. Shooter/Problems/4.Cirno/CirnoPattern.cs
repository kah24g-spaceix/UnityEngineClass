using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirnoPattern : ShooterPattern
{
    private static readonly Int32 BULLET_COUNT = 30;

    /*  TODO :: ���ݱ��� �� �� ��� ������ ����������Ʈ�� ĳ���� "ġ����"�� ������ �ִ��� ����ϰ� �����غ���
     * 
     *  ġ������ ������ ������ ���� �����ϸ� ����ϰ� ������ �� �ִ�.
     *  
     *  1. �� ������� ź���� �߻��Ѵ�. 
     *  2. n�� ���
     *  3. �߻�� ź������ �� �ڸ����� ��� �ٽ� ���� ���ϰ� �Ͽ� �� ������ �߻��Ѵ�.
     *  
     *  �� �߻�� ź������ �׻� ź���� ���ϴ� ������ �ٶ󺸰� �־�� �Ѵ�.
     *  
     *  �� �߻��� ź������ n�� �Ŀ� �ٽ� �����ؾ� �ϹǷ�, �߻��� ź������ List ���� �ݷ��ǿ� ��� �־�� �Ѵ�.
     *     �� �ڵ忡���� List�� ��ƺ���.
     *     
     *  �� ź���� �ٶ󺸴� ������ �� ������ �ϹǷ�, �� �ڵ忡�� SpawnBullet()�� ����ϸ�
     *     ������ �ٸ� ������ ź���� �߻�ȴ�.
     *     
     *  ��Ʈ.
     *  ���ݱ��� �ۼ��� �ڵ���� ��� ����Ѵ�. 
     *  ��� �ڵ�� �ڵ� ���ٵ� �����ϴ�!
     */

    /*  ��� ������ �Լ� ����
     * 
     *  ShooterPattern :: SpawnBullet() 
     *  ���� ��ġ�� ź���� �����Ѵ�. ������ ź���� ��ȯ
     *      ��ȯ�� 
     *      - ShooterBullet
     *      
     *  ShooterPattern :: FindTarget() 
     *  Ÿ���� ã�Ƴ���. ã�Ƴ� Ÿ���� ��ȯ (ã�� ������ ��� null)
     *      ��ȯ�� 
     *      - GameObject
     * 
     *  ShooterBullet :: SetVelocity()
     *  ź���� velocity���� �����Ѵ�. 
     *      �Ű����� 
     *      - velocity: Vector2 
    */


    //�̰��� �߻�� ź���� ��´�.
    //������ ������ ���� �����ؾ� �ϹǷ� ���� ���� �� Clear()�� ȣ���� �ʱ�ȭ�ϴ°��� ���� �� ��.
    private List<ShooterBullet> m_bullets = new List<ShooterBullet>();

    /// <summary>
    /// ���� Ŭ�������� ���� ������ ȣ��Ǹ� �� �ڷ�ƾ�� ����ȴ�
    /// </summary>
    protected override IEnumerator Pattern()
    {
        ///////////////////////////////////////////
        // �̰��� �ڵ带 �ۼ�
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
