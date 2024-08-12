using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;

namespace MyProject
{
	public class MultipleWebRequestByDotnet : MonoBehaviour
	{
		private string url = "https://picsum.photos/500";

		public List<Image> images;

        private void Start()
        {
    //        foreach (var image in images)
    //        {
	//			DownloadImage(image);
    //        }
		
			// 위의 foreach문과 같음
			images.ForEach(DownloadImage);
			print($"다운로드 요청(Request) 완료");

        }

        async void DownloadImage(Image targetImage)
        {
			using (HttpClient client = new HttpClient())
            {
				byte[] response = await client.GetByteArrayAsync(url);
				Texture2D texture = new Texture2D(1, 1);
				texture.LoadImage(response);
				targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			}				
        }
	}
}
