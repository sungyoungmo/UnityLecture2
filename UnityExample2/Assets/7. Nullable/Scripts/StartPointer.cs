using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject
{
	public class StartPointer : MonoBehaviour
	{
		//internal Vector3 startPoint;	// ���� vector3�� referecne type�̾��� ���, �ʱⰪ �Ҵ� ���θ� null üũ�� ���� ������.
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
		//		Debug.LogError("StartPoint ���� �Ҵ���� �ʾҽ��ϴ�. ���� SetInitialValue �Լ��� ���� �ʱⰪ�� �������ּ���.");
		//          }


		//      }


		// ������ boxing�� ����ȴ�.
		internal Vector3? startPoint = null;
		public void DisplayPoint()
        {
			//if (startPoint.HasValue)
			//{
			//    print(startPoint.Value);
			//}
			//else
			//{
			//	Debug.LogError("StartPoint ���� �Ҵ���� �ʾҽ��ϴ�. ���� startPoint �ʱⰪ�� �������ּ���.");
			//}

			print(startPoint ?. ToString() ?? "startPoint ���� �Ҵ���� �ʾҽ��ϴ�.");
        }

    }
}
