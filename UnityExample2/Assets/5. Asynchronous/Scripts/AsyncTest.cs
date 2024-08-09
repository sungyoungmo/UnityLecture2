using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyProject
{
	public class AsyncTest : MonoBehaviour
	{
		//async void Start()
  //      {
		//	WaitRandom();
		//	print("WaitRandom 호출함");

		//	await Wait3Seconds();
		//	print("Wait3Seconds 호출함");

		//	// 대기했다가 함수가 끝나면 a에 전달 가능
		//	int a = await WaitRandomAndReturn();
		//	print($"{a}초 WaitRandomAndReturn 호출");

		//	await WaitAndCallback(()=> print("니가 먼저 호출되나"));
		//	print("내가 먼저 호출되나");

		//	// 이런 걸로 await 없이 사용 가능했지만 편의를 위해 await 도입
		//	//Task.Run();
		//	//new Task().ContinueWith();


		//	// 대부분의 상황에선 코루틴으로 사용하는 게 맞지만 통신에서 await와 async 키워드를 사용하기에 알려줌 
  //      }

		private void Start()
        {
			// async가 아닌데도 Wait3Seconds()가 끝난 후에 무언가 해야할 경우.
			Wait3Seconds().ContinueWith
			(
				(Task result) =>
                {
                    if (result.IsCanceled || result.IsFaulted) 
					{ 
						print("task 실패");
					}
                    else if (result.IsCompleted)
                    {
						print("task 성공");
                    }
					print("3초후");
				}
			);
			
        }



		// 1. void를 반환하는 async 함수 : 함수 자체는 대기 가능이나, 다른 함수에서 대기 가능 형식으로 호출이 불가능하다.
		// await WaitRandom();같은 것들
		async void WaitRandom()
        {
			print($"대기 시작 {Time.time}");
			await Task.Delay(Random.Range(1000, 2000));
			print($"대기 종료 {Time.time}");
		}

		// 2. Task를 반환하는 async 함수 : 함수 자체도 대기 가능이며, 다른 대기 가능 함수에서 비동기식으로 호출이 가능하다.
		//    return이 없어도 알아서 프로세스를 Task로 묶어 반환함
		async Task Wait3Seconds()
        {
			print($"3초후 시작 {Time.time}");
			await Task.Delay(3000);
			print($"3초후 종료 {Time.time}");
		}

		// 3. Task<T>를 반환하는 async 함수 : 대기 가능 함수인건 Tsak를 반환하는 함수와 같으나, T return이 있어야만 함
		async Task<int> WaitRandomAndReturn()
        {
			int delay = Random.Range(1000, 2000);

			print($"{(float)delay/1000}초 대가 시작 {Time.time}");
			await Task.Delay(delay);
			print($"{(float)delay / 1000}초 대가 종료 {Time.time}");


			return delay;
        }

		async Task WaitAndCallback(Action callback)
        {
			
        }
	}
}
