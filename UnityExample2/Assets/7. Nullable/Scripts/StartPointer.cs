using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject
{
	public class StartPointer : MonoBehaviour
	{
		//internal Vector3 startPoint;	// 만일 vector3가 referecne type이었을 경우, 초기값 할당 여부를 null 체크를 통해 가능함.
		//public bool isInitialized;

		//public void SetInitialValue(Vector3 startPoint)
		//      {
		//	this.startPoint = startPoint;
		//	isInitialized = true;
		//      }

		//public void DisplayPoint()
		//      {
		//	if (isInitialized)
		//          {
		//		print(startPoint);
		//	}
		//	else
		//          {
		//		Debug.LogError("StartPoint 값이 할당되지 않았습니다. 먼저 SetInitialValue 함수를 통해 초기값을 세팅해주세요.");
		//          }


		//      }


		// 일종의 boxing이 적용된다.
		internal Vector3? startPoint = null;
		public void DisplayPoint()
        {
			//if (startPoint.HasValue)
			//{
			//    print(startPoint.Value);
			//}
			//else
			//{
			//	Debug.LogError("StartPoint 값이 할당되지 않았습니다. 먼저 startPoint 초기값을 세팅해주세요.");
			//}

			print(startPoint ?. ToString() ?? "startPoint 값이 할당되지 않았습니다.");
        }

    }
}
