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
            // 스팀 클라이언트 초기화
            // 스팀 개발자에게 제공하는 테스트 앱 ID: 480
            SteamClient.Init(480);

            //print(SteamClient.AppId);
            print(SteamClient.SteamId);
            print(SteamClient.Name);

            // 스팀에 접속한 계정의 친구목록을 로드하여, 초상화와 접속 여부를 표시

            SteamImage? myAvatar =  await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);     // 내 초상화

            UnityImage myAvatarImage = Instantiate(imagePrefab, canvasTransform);

            if (myAvatar.HasValue)
            {
                myAvatarImage.sprite = SteamImageToSprite(myAvatar.Value);
            }
            else
            {
                myAvatarImage.sprite = defaultAvatar;
            }

            // 접속 여부 아이콘 비활성화
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
            // texture2d 인스턴스 생성
            Texture2D texture = new Texture2D((int)image.Width,(int)image.Height, TextureFormat.ARGB32, false);

            // 텍스처 필터모드 변경
            texture.filterMode = FilterMode.Trilinear;

            // Steam Image와 Unity의 sprite 텍스쳐의 픽셀 표시 순서가 다르므로 반전이 필요함.
            // 스팀은 우하향, 유니티는 우상향
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
