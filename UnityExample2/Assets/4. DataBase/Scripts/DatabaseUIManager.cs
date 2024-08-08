using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
	public class DatabaseUIManager : MonoBehaviour
	{
		public GameObject loginPanel;
		public GameObject infoPanel;
		public GameObject registerPanel;
		public GameObject modifyPanel;

		[Space(10)]
		public InputField emailInput;
		public InputField pwInput;
		public Button signUpButton;
		public Button loginButton;
		public Button registerButton;
		public Button modifyButton;
		

		[Space(10)]
		public InputField register_EmailInput;
		public InputField register_PWInput;
		public InputField register_ClassInput;

		[Space(10)]
		public Button register_Register_Button;
		public Button register_Cancel_Button;

		[Space(10)]
		public InputField modify_NameInput;
		public InputField modify_TextInput;
		public InputField modify_ClassInput;

		[Space(10)]
		public Button modify_Modify_Button;
		public Button modify_Cancel_Button;
		public Button deleteButton;

		[Space(10)]
		public Text infoText;
		public Text levelText;

		private UserData userData;

        private void Awake()
        {
			loginButton.onClick.AddListener(LoginButtonClick);
			registerButton.onClick.AddListener(RegisterButtonClick);
			modifyButton.onClick.AddListener(ModifyButtonClick);
			deleteButton.onClick.AddListener(OnDeleteButtonClick);

			register_Register_Button.onClick.AddListener(OnRegisterRegisterButtonClick);
			register_Cancel_Button.onClick.AddListener(OnCancelButtonClick);

			modify_Modify_Button.onClick.AddListener(OnModifyModifyButtonClick);
			modify_Cancel_Button.onClick.AddListener(OnCancelButtonClickInModify);
        }

		public void RegisterButtonClick()
        {
			loginPanel.SetActive(false);
			registerPanel.SetActive(true);
        }

		private void OnCancelButtonClick()
		{
			registerPanel.SetActive(false);
			loginPanel.SetActive(true);
		}

		private void ModifyButtonClick()
        {
			infoPanel.SetActive(false);
			modifyPanel.SetActive(true);

			modify_NameInput.text = null;
			modify_TextInput.text = null;
			modify_ClassInput.text = null;
		}

		private void OnCancelButtonClickInModify()
        {
			modifyPanel.SetActive(false);
			infoPanel.SetActive(true);

        }

		private void OnModifyModifyButtonClick()
        {
			DatabaseManager.Instance.Modify(userData.email, modify_NameInput.text,modify_TextInput.text, (CharClass)int.Parse(modify_ClassInput.text), OnModifySuccess, OnModifyFailure);
        }

		private void OnDeleteButtonClick()
        {
			DatabaseManager.Instance.Delete(userData.email, OnDeleteSuccess, OnDelteFailure);
        }

		private void OnRegisterRegisterButtonClick()
		{
			DatabaseManager.Instance.Register(register_EmailInput.text, register_PWInput.text, (CharClass)int.Parse(register_ClassInput.text), OnRegisterSuccess, OnRegisterFailure);
		}

		public void LoginButtonClick()
        {
			DatabaseManager.Instance.Login(emailInput.text, pwInput.text, OnLoginSuccess, OnLoginFailure);
        }

		public void OnLevelButtonClick()
        {
			DatabaseManager.Instance.LevelUp(userData, OnLevelSuccess);
        }

		private void OnLevelSuccess()
        {
			levelText.text = $"레벨: {userData.level}";
		}

		private void OnLoginSuccess(UserData data)
        {
			print("로그인 성공");

			userData = data;
			loginPanel.SetActive(false);
			infoPanel.SetActive(true);

			StringBuilder sb = new StringBuilder();

			sb.AppendLine($"안녕하세요, {data.name}");
			sb.AppendLine($"이메일: {data.email}");
			sb.AppendLine($"직업: {data.charClass}");
			sb.AppendLine($"소개글: {data.profileText}");

			infoText.text = sb.ToString();

			levelText.text = $"레벨: {data.level}";
		}

		private void OnLoginFailure()
		{
			print("로그인 실패");
		}

		
		

		private void OnRegisterSuccess()
        {
			print("회원가입 성공");
        }

		private void OnRegisterFailure()
        {
			print("회원가입 실패");
		}

		private void OnModifySuccess(UserData data)
		{
			print("정보 수정 성공");

			userData = data;
			OnCancelButtonClickInModify();

			StringBuilder sb = new StringBuilder();

			sb.AppendLine($"안녕하세요, {data.name}");
			sb.AppendLine($"이메일: {data.email}");
			sb.AppendLine($"직업: {data.charClass}");
			sb.AppendLine($"소개글: {data.profileText}");

			infoText.text = sb.ToString();

		}

		private void OnModifyFailure()
		{
			print("정보 수정 실패");
		}

		private void OnDeleteSuccess()
		{
			print("정보 수정 성공");
		}

		private void OnDelteFailure()
		{
			print("정보 수정 실패");
		}
	}
}
