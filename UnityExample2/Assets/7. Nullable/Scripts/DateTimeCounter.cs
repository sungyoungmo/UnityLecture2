using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject
{
	public class DateTimeCounter : MonoBehaviour
	{
		// DisplayDateTime() ȣ�� �� ���õ� �ð��� ����ϴµ�, ������ ���� �ʾ��� ��� ���õ��� �ʾҴٴ� ������ print �ϵ��� �ٲ㺸����.

		public DateTime? dateTime;



		public void DisplayDateTime()
        {
			print(dateTime ?. ToString() ?? "�ð� ���õ��� ����");
        }
	}
}
