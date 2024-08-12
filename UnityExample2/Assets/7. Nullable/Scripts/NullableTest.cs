using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject
{
	public class NullableTest : MonoBehaviour
	{
		// nullable �ڷ��� : ������� null ���� �� �� ���� Ÿ�� (���ͷ� Ÿ��, enum, struct)�� null ���� �Ҵ��� �� �ֵ��� �ϴ� ���� ���

		private int normalInt;      // �ʵ忡�� ������ ���ͷ�Ÿ���� ���, �ʱ�ȭ �Ҵ��� ���� �ʾƵ� �⺻������ �Ҵ�.
        private int? nullableInt;   // nullable ������ ���, ���۷��� Ÿ�԰� ���� �⺻���� null;        
        
        private Vector3 vector3;    // ���ͷ�Ÿ���̱� ������, �ʱⰪ �Ҵ��� ���� �ʾƵ� ��
        private GameObject obj;     // ���۷��� Ÿ���̱� ������ �ʱⰪ �Ҵ��� ���� ���� ��� null


        private void Start()
        {
            //print($"normalInt : {normalInt}");
            //print($"nullableInt : {nullableInt}");
            //
            //
            //vector3.x = 1f;
            //
            //print(vector3);

            // ?. ?? : null ���� �� �� �ִ� ������ ������ null���� �ƴ��� üũ�ϴ� ������.
            // [����]?.[��]??[������ null�� ��� ��ȯ�� ��];
            // [�븮��]?.[�Լ�](); : �븮�� �Ǵ� Ŭ������ null�� ��� �Լ��� ȣ������ ����

            //StartPointer sp = new GameObject().AddComponent<StartPointer>();

            //sp.startPoint = Vector3.zero;
            //sp.DisplayPoint();

            DateTimeCounter dtc = new GameObject().AddComponent<DateTimeCounter>();
            dtc.dateTime = System.DateTime.Now;
            dtc.DisplayDateTime();
        }
    }
}
