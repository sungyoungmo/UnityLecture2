using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
	public class AsyncByCoroutine : MonoBehaviour
	{
		public Text text;
		
		// �ܹ��Ÿ� ����� �ʹ�.
		int bread = 0; // �� ����
		int patty = 0; // ��Ƽ ����
		int pickle = 0; // ��Ŭ ����
		int lettuce = 0; // �����

		FoodMakerThread breadMaker = new FoodMakerThread();
		FoodMakerThread patMaker = new FoodMakerThread();
		FoodMakerThread pickleMaker = new FoodMakerThread();
		FoodMakerThread lettuceMaker = new FoodMakerThread();

		private void Start()
        {
			breadMaker.StartCook();
			patMaker.StartCook();
			pickleMaker.StartCook();
			lettuceMaker.StartCook();

			StartCoroutine(CheckHamberger());
		}

        private void Update()
        {
			bread = breadMaker.amount;
			patty = patMaker.amount;
			pickle = pickleMaker.amount;
			lettuce = lettuceMaker.amount;

			text.text = $"�� ����: {bread}, ��Ƽ ����: {patty}, ��Ŭ ����: {pickle}, ����� ����: {lettuce}";
        }

		IEnumerator CheckHamberger()
        {
			// ������ �����Ǳ� �������� �ƹ��͵� ���ϵ��� null�� ����
			yield return new WaitUntil(HambergerReady);
			MakeHamberger();
		}


        bool HambergerReady()
        {
			return bread >= 2 && patty >= 2 && pickle >= 8 && lettuce >= 4;
        }

		void MakeHamberger()
        {
			//bread -= 2;
			//patty -= 2;
			//pickle -= 8;
			//lettuce -= 4;

			print($"�ܹ��Ű� ����������ϴ�. �ҿ�ð� : {Time.time}");
        }
	}

	public class FoodMakerThread
    {
		public int amount; // ����� ��
		private System.Random rand = new System.Random();

		public void StartCook()
        {
			Thread cookThread = new Thread(Cook);
			cookThread.Start();
        }

		private void Cook()
        {
            while (true)
            {
				int time = rand.Next(1000, 3000);
				Thread.Sleep(time);
				amount++;
            }
        }
    }
}