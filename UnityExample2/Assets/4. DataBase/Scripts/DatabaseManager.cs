using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace MyProject
{
    public class DatabaseManager : MonoBehaviour
    {
        //private string serverIP = "43.203.218.203";
        private string serverIP = "127.0.0.1";


        private string dbName = "game";
        private string tableName = "users";
        private string rootPasswd = "qwe123"; // �׽�Ʈ �ÿ� Ȱ���� �� ������ ���ȿ� ����ϹǷ� ����

        private MySqlConnection conn; // mysql DB�� ���� ���¸� �����ϴ� ��ü.

        public static DatabaseManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            DBConnect();
        }

        public void DBConnect()
        {
            // port �����ؾ���
            string config = $"server={serverIP};port=3306;database={dbName};" + $"uid=root;pwd={rootPasswd};charset=utf8;";

            conn = new MySqlConnection(config);

            conn.Open();
        }

        // �α����� �Ϸ��� �� ��, �α��� ������ ���� ��� �����Ͱ� ���� ���� �� �����Ƿ�, �α����� �Ϸ�Ǿ��� �� ȣ��� �Լ��� �Ķ���ͷ� �Բ� �޾��ֵ��� ��.
        public void Login(string email, string password, Action<UserData> successCallback, Action failureCallback)
        {
            string pwhash = "";

            SHA256 sha256 = SHA256.Create();
            byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            foreach (byte b in hashArray)
            {
                pwhash += $"{b:X2}";
                //pwhash += b.ToString("X2");
                // �� �� �ƹ����Գ� �������
            }

            sha256.Dispose();

            print(pwhash);



            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"SELECT * FROM {tableName} WHERE email = '{email}' AND pw = '{password}'";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

            DataSet set = new DataSet();

            dataAdapter.Fill(set);

            bool isLoginSuccess = set.Tables.Count > 0 && set.Tables[0].Rows.Count > 0;

            if (isLoginSuccess)
            {
                // �α��� ����(email�� pw ���� ���ÿ� ��ġ�ϴ� ���� ������)
                DataRow row = set.Tables[0].Rows[0];

                //print(row["LEVEL"]);
                //print(row["level"]);

                UserData data = new UserData(row);

                //print(data.email);

                successCallback?.Invoke(data);
            }
            else
            {
                // �α��� ����
                failureCallback?.Invoke();
            }


        }

        public void LevelUp(UserData data, Action successCallback)
        {
            int level = data.level;

            int nextLevel = level + 1;

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = $"UPDATE {tableName} SET level = {nextLevel} WHERE uid = {data.UID}";

            int queryCount = cmd.ExecuteNonQuery();

            if (queryCount > 0)
            {
                // ������ ���������� �����
                data.level = nextLevel;
                successCallback?.Invoke();
            }
            else
            {
                // ���� ���� ����
            }
        }

        public void Register(string email, string password, CharClass charClass, Action successCallback, Action failureCallback)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = $"SELECT email FROM users WHERE email = '{email}'";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

            DataSet set = new DataSet();

            dataAdapter.Fill(set);

            bool isRegisterSuccess = set.Tables.Count > 0 && set.Tables[0].Rows.Count == 0;
            
            if (isRegisterSuccess && ((int)charClass < 4 && (int)charClass >= 0))
            {
                cmd.CommandText = $"INSERT INTO users(email, pw, LEVEL, class) VALUES('{email}', '{password}', 1,{(int)charClass})";
                int queryCount = cmd.ExecuteNonQuery();

                if (queryCount > 0)
                {
                    successCallback?.Invoke();
                }
                else
                {
                    failureCallback?.Invoke();
                }

                
            }
            else
            {
                failureCallback?.Invoke();
            }
        }

        public void Modify(string email, string name, string profile_text, CharClass charClass, Action<UserData> successCallback, Action failureCallback)
        {


            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;

            if (name.Length == 0)
            {
                cmd.CommandText = $"UPDATE {tableName} SET profile_text = '{profile_text}', class = '{(int)charClass}' WHERE email = '{email}'";
            }
            else if (charClass.ToString().Length == 0)
            {
                Debug.Log("alert");
            }
            else
            {
                cmd.CommandText = $"UPDATE {tableName} SET name = '{name}', profile_text = '{profile_text}', class = '{(int)charClass}' WHERE email = '{email}'";
            }
            
            

            int queryCount = cmd.ExecuteNonQuery();

            cmd.CommandText = $"SELECT * FROM {tableName} WHERE email = '{email}'";

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);

            DataSet set = new DataSet();

            dataAdapter.Fill(set);


            if (queryCount > 0)
            {
                // ������ ���������� �����

                DataRow row = set.Tables[0].Rows[0];

                //print(row["LEVEL"]);
                //print(row["level"]);

                UserData data = new UserData(row);

                successCallback?.Invoke(data);
            }
            else
            {
                // ���� ���� ����
                failureCallback?.Invoke();
            }

        }

        public void Delete(string email, Action successCallback, Action failureCallback)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = $"DELETE FROM {tableName} WHERE email = '{email}'";

            int queryCount = cmd.ExecuteNonQuery();

            if (queryCount > 0)
            {
                // ������ ���������� �����

                successCallback?.Invoke();
            }
            else
            {
                // ���� ���� ����
                failureCallback?.Invoke();
            }
        }

    }
}
