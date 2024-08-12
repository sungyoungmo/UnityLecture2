using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject
{
	public class DateTimeCounter : MonoBehaviour
	{
		// DisplayDateTime() 호출 시 세팅된 시간을 출력하는데, 세팅이 되지 않았을 경우 세팅되지 않았다는 문구를 print 하도록 바꿔보세요.

		public DateTime? dateTime;



		public void DisplayDateTime()
        {
			print(dateTime ?. ToString() ?? "시간 세팅되지 않음");
        }
	}
}
