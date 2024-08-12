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

			// _ = �� �Ʒ�ó�� ���ϰ��� �ִ� �Լ����� ���ҽ� ���� ���� ���� ������ ���� �ʴ´ٴ� ��.
			//Coroutine coroutine = StartCoroutine(GetWebTexture(imageURL));
		}

        IEnumerator GetWebTexture(string url)
        {
			// http�� �� ��û(Request)�� ���� ��ü ����
			UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

			// �񵿱��(�� ����� �ڷ�ƾ)���� Response�� ���� ������ ���
			var operation = www.SendWebRequest();
            
			yield return operation;

			if (www.result != UnityWebRequest.Result.Success)
            {
				print(url);
				Debug.LogError($"HTTP ��� ����: {www.error}");
            }
			else
            {
				Debug.Log("�ؽ��� �ٿ�ε� ����!");
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
				print("ȣ��");
            }
        }


	}
}
