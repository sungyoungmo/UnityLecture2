using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

using UnityImage = UnityEngine.UI.Image;
using SteamImage = Steamworks.Data.Image;
using System;

namespace MyProject
{
	public class SteamworksTest : MonoBehaviour
	{
		public UnityImage imagePrefab;
		public Transform canvasTransform;
        public Sprite defaultAvatar;

        async void Start()
        {
            // ���� Ŭ���̾�Ʈ �ʱ�ȭ
            // ���� �����ڿ��� �����ϴ� �׽�Ʈ �� ID: 480
            SteamClient.Init(480);

            //print(SteamClient.AppId);
            print(SteamClient.SteamId);
            print(SteamClient.Name);

            // ������ ������ ������ ģ������� �ε��Ͽ�, �ʻ�ȭ�� ���� ���θ� ǥ��

            SteamImage? myAvatar =  await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);     // �� �ʻ�ȭ

            UnityImage myAvatarImage = Instantiate(imagePrefab, canvasTransform);

            if (myAvatar.HasValue)
            {
                myAvatarImage.sprite = SteamImageToSprite(myAvatar.Value);
            }
            else
            {
                myAvatarImage.sprite = defaultAvatar;
            }

            // ���� ���� ������ ��Ȱ��ȭ
            myAvatarImage.transform.GetChild(0).gameObject.SetActive(false);


            foreach (Friend friend in SteamFriends.GetFriends())
            {
                SteamImage? friendAvatar = await SteamFriends.GetLargeAvatarAsync(friend.Id);
                UnityImage friendAvatarImage = Instantiate(imagePrefab, canvasTransform);

                if (friendAvatar.HasValue)
                {
                    friendAvatarImage.sprite = SteamImageToSprite(friendAvatar.Value);
                }
                else
                {
                    friendAvatarImage.sprite = defaultAvatar;
                }

                friendAvatarImage.transform.GetChild(0).gameObject.SetActive(!friend.IsOnline);
            }


            //SteamFriends.OpenStoreOverlay(480);
        }

        private void OnApplicationQuit()
        {
            SteamClient.Shutdown();
        }

        public Sprite SteamImageToSprite(SteamImage image)
        {
            // texture2d �ν��Ͻ� ����
            Texture2D texture = new Texture2D((int)image.Width,(int)image.Height, TextureFormat.ARGB32, false);

            // �ؽ�ó ���͸�� ����
            texture.filterMode = FilterMode.Trilinear;

            // Steam Image�� Unity�� sprite �ؽ����� �ȼ� ǥ�� ������ �ٸ��Ƿ� ������ �ʿ���.
            // ������ ������, ����Ƽ�� �����
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var p = image.GetPixel(x,y);
                    var color = new Color(p.r / 255f, p.g / 255f, p.b / 255f, p.a / 255f);
                    texture.SetPixel(x, (int)image.Height - y, color);
                }
            }

            texture.Apply();

            Sprite sprite = 
                Sprite.Create(
                    texture, 
                    rect: new Rect(x: 0, y: 0, width: texture.width, height: texture.height), 
                    pivot: new Vector2(x:0.5f, y:0.5f)
                    );

            return sprite;
        }
    }
}
