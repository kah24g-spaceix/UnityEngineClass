using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public static class LookAtExtension 
{

    /*  TODO :: ���Ϳ� �ﰢ�Լ��� ����Ͽ� LookAt2D �Լ��� �����Ѵ�.
     *  
     *  ���Ϳ� �ﰢ�Լ��� ����ϸ� ����Ƽ���� �⺻������ �������� �ʴ�
     *  LookAt2D�Լ��� ������ �� �ִ�.
     *  
     *  ������ ������ �̿��Ͽ� ���� transform���� ���� ��ġ������ ������ ���ϰ�, 
     *  �� ���⿡ �´� ������ ���� �ٲ� ȸ���� ��������.
     *  
     *  ��Ʈ.
     *  �Ϲ����� �ﰢ�Լ��� radian�� �ް� ���� ��ȯ������,
     *  �ﰢ�Լ��� ���Լ��� ����ϸ� ���� �޾� radian�� ��ȯ�Ѵ�.
     */

    /*  �� �ﰢ�Լ� ����ϱ�
     * 
     *  Mathf.Asin(), Mathf.Acos(), Mathf.Atan2() �Լ��� ����ϸ� �� ź��Ʈ �Լ��� ����� �� �ִ�.
     *  ��, �� �Լ����� radian ���� ��ȯ�ϱ� ������ ����� Mathf.Rad2Deg ���� ������� ���� ���� �� �ִ�.
     *  
     *  
     *  ��, ���� �״�� rotation�� �����Ű����
     *  Quaternion.Euler() �Լ��� ����ϸ� �ȴ�.
     *  
     *  ���� ���, ��ü�� 30�� ȸ����Ű����
     *  transform.rotation = Quaternion.Euler(0,0,30);
     */

    /* ���ǻ���
     * 
     * �� ������Ʈ�� ��� ��������Ʈ�� ��(+Y����)�� �ٶ󺸰� �ִ�.
     * ���� �������� ���� ���� 90�� ����� ���ϴ� LookAt�� ������ �� �ִ�.
     * 
     */


    /// <summary>
    /// ������ ��ġ�� �ٶ󺸰� �Ѵ�. ��(+Y����)�� �ٶ󺸰� �ִ� ��������Ʈ ����
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="pTarget">�ٶ󺸰� �� ��ġ</param>
    public static void LookAt2D(this Transform transform,Vector2 pTarget) 
    {
        // Ȯ�� �޼���
        //�ٸ� Ŭ������ �޼��带 
        ///////////////////////////////////////////
        // �̰��� �ڵ带 �ۼ�
        Vector2 targetPos = pTarget - (Vector2)transform.position;
        float rotation = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation - 90);
        ///////////////////////////////////////////
    }
}
