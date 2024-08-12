using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;

namespace MyProject
{
    public class WebRequestByDotnet : MonoBehaviour
    {
        public string url;
        public Image image;

        async void Start()
        {
            //HttpClient client = new HttpClient();
            // httpClient ��� �Ŀ� �޸� ������ �ʿ�
            // C++�� ��� ~HttpClient(); ���� ������ �Ҹ��ڸ� ȣ��

            // C#�� ��� �Ʒ��� ���� dispose�� ȣ��
            //client.Dispose();

            // �Լ� ���� using ���� ���� Ư�� ��� �ȿ����� ���ǰ� ��� �ۿ����� �ڵ����� �����Ǵ� IDisposable Ŭ������ �����Ͽ� ���
            // ��Ϲۿ����� client�� ���� �Ұ���
            using (HttpClient client = new HttpClient())
            {
                byte[] response = await client.GetByteArrayAsync(url);
                // byte[]�� Unity���� Ȱ���� �� �ִ� Texture Instance�� ��ȯ
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(response);

                image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
    }
}
