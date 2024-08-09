using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
	public class AsyncByTask : MonoBehaviour
	{
		public Text text;

		int bread = 0; // 빵 개수
		int patty = 0; // 패티 개수
		int pickle = 0; // 피클 개수
		int lettuce = 0; // 양상추

		FoodMakerTask breadMaker = new FoodMakerTask();
		FoodMakerTask pattyMaker = new FoodMakerTask();
		FoodMakerTask pickleMaker = new FoodMakerTask();
		FoodMakerTask lettuceMaker = new FoodMakerTask();

		

		private void Start()
        {
			breadMaker.StartCook(2);
			pattyMaker.StartCook(2);
			pickleMaker.StartCook(8);
			lettuceMaker.StartCook(4);

			StartCoroutine(CheckHamberger());
		}

		private void Update()
		{
			bread = breadMaker.amount;
			patty = pattyMaker.amount;
			pickle = pickleMaker.amount;
			lettuce = lettuceMaker.amount;

			text.text = $"빵 개수: {bread}, 패티 개수: {patty}, 피클 개수: {pickle}, 양상추 개수: {lettuce}";
		}

		IEnumerator CheckHamberger()
		{
			// 조건이 만족되기 전까지는 아무것도 안하도록 null을 리턴
			yield return new WaitUntil(HambergerReady);
			MakeHamberger();
		}

		bool HambergerReady()
		{
			return bread >= 2 && patty >= 2 && pickle >= 8 && lettuce >= 4;
		}

		void MakeHamberger()
		{
			print($"햄버거가 만들어졌습니다. 소요시간 : {Time.time}");
		}
	}

	public class FoodMakerTask
    {
		public int amount = 0;

		public void StartCook(int count)
        {
			Task<int> cookTask = Cook(count);
			//cookTask.Start();

			//cookTask.ContinueWith(task => { amount = task.Result});
        }

		private async Task<int> Cook(int count)
        {
			int result = 0;

            for (int i = 0; i < count; i++)
            {
				int time = Random.Range(1000, 3000);
				await Task.Delay(time);
				result++;
			}
			return result;
        }
    }
}
