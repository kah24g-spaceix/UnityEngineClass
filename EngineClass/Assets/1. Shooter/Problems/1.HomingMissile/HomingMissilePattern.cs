using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissilePattern : ShooterPattern
{
    /*  TODO :: ���� ������ �̿��Ͽ� ���� ���� ���ư��� ź���� �߻��Ѵ�
     * 
     *  CirclePattern Ŭ������ ShooterPattern Ŭ������ ��ӹ޾ұ� ������
     *  
     *  SpawnBullet(), FindTarget() �Լ��� ȣ���� �� �ִ�.
     *  FindTarget() �Լ��� ȣ���Ͽ� Ÿ���� ã�Ƴ���,
     *  ���� ������ ���� ã�Ƴ� Ÿ�ٱ����� ������ ���� velocity�� ���Ѵ�.
     *  
     *  SpawnBullet() �Լ��� ȣ���Ͽ� ���� ��ġ�� ź���� �����ϰ�, 
     *  ��ȯ�� ShooterBullet �ν��Ͻ����� SetVelocity() �Լ��� ȣ���Ͽ�
     *  velocity ���� ��������.
     *  
     *  �� Target�� �������� ź���� �߻�Ǿ����� ����� ��ġ�θ� �߻�Ǹ� �ȴ�.
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
     * 
    */

    /// <summary>
    /// ���� Ŭ�������� ���� ������ ȣ��Ǹ� �� �ڷ�ƾ�� ����ȴ�
    /// </summary>
    protected override IEnumerator Pattern()
    {
        ///////////////////////////////////////////
        // �̰��� �ڵ带 �ۼ�
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
