using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace MyProject
{
	public class WebRequestTest : MonoBehaviour
	{
		public List<string> ImageURLs;

		public string imageURL;
		public RawImage rawImage;
		public Image image;

		private void Start()
        {
			_ = StartCoroutine(GetWebTextureByOrder(ImageURLs));
			//_ = StartCoroutine(GetWebTexture(imageURL));

			// _ = 는 아래처럼 리턴값이 있는 함수에서 리소스 낭비를 막기 위해 리턴을 받지 않는다는 뜻.
			//Coroutine coroutine = StartCoroutine(GetWebTexture(imageURL));
		}

        IEnumerator GetWebTexture(string url)
        {
			// http로 웹 요청(Request)를 보낼 객체 생성
			UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

			// 비동기식(을 모방한 코루틴)으로 Response를 받을 때까지 대기
			var operation = www.SendWebRequest();
            
			yield return operation;

			if (www.result != UnityWebRequest.Result.Success)
            {
				print(url);
				Debug.LogError($"HTTP 통신 실패: {www.error}");
            }
			else
            {
				Debug.Log("텍스쳐 다운로드 성공!");
				//rawImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

				Texture texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

				Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height),
					new Vector2(0.5f, .5f));

				image.sprite = sprite;

				image.SetNativeSize();
			}
        }

		IEnumerator GetWebTextureByOrder(List<string> url)
        {
            foreach (var item in url)
            {
				yield return StartCoroutine(GetWebTexture(item));
				yield return new WaitForSeconds(1.0f);
				print("호출");
            }
        }


	}
}
