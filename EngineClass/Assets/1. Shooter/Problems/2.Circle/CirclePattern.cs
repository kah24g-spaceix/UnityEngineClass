using System;
using System.Collections;
using UnityEngine;

public class CirclePattern : ShooterPattern
{
    private static readonly Int32 BULLET_COUNT = 30;


    /*  TODO :: �ﰢ�Լ��� ����Ͽ� �� ������� ź���� �߻��Ѵ�. 
     *  ź���� ���� ���� BULLET_COUNT�� ����Ѵ�.
     * 
     *  CirclePattern Ŭ������ ShooterPattern Ŭ������ ��ӹ޾ұ� ������
     *  SpawnBullet() �Լ��� ȣ���� �� �ִ�.
     *  SpawnBullet() �Լ��� ȣ���Ͽ� ���� ��ġ�� ź���� �����ϰ�, 
     *  ��ȯ�� ShooterBullet �ν��Ͻ����� SetVelocity() �Լ��� ȣ���Ͽ�
     *  velocity ���� ��������.
     * 
     *  �ӵ��� �ﰢ�Լ��� �̿��Ͽ� ���Ѵ�. 
     */

    /*  �ﰢ�Լ� ����ϱ�
     * 
     *  Mathf.Cos(), Mathf.Sin(), Mathf.Tan()  �Լ��� �̿��ϸ� �ﰢ�Լ��� ����� �� �ִ�.
     *  ��, �� �Լ����� �Ű������� radian�̱� ������ ���� Mathf.Deg2Rad�� ���� ���� �־���� �Ѵ�.
     *  
     *  ��뿹��
     *  float angle = 60;
     *  float rad = 60 * Mathf.Deg2Rad;
     *  Single cos60 = Mathf.Cos(rad);
     *  
     */

    /*  ��� ������ �Լ� ����
     * 
     *  ShooterPattern :: SpawnBullet() 
     *  ���� ��ġ�� ź���� �����Ѵ�. ������ ź���� ��ȯ
     *      ��ȯ�� 
     *      - ShooterBullet
     * 
     *  ShooterBullet :: SetVelocity()
     *  ź���� velocity���� �����Ѵ�. 
     *      �Ű����� 
     *      - velocity: Vector2 
     * 
    */

    /// <summary>
    /// ���� Ŭ�������� ���� ������ ȣ��Ǹ� �� �ڷ�ƾ�� ����ȴ�
    /// </summary>
    protected override IEnumerator Pattern()
    {
        ///////////////////////////////////////////
        // �̰��� �ڵ带 �ۼ�
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
