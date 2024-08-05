using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace MyProject
{
	public class ServerManager : MonoBehaviour
	{
		public Button connetButton;
		public Text messagePrefab;
		public RectTransform textArea;

		public string ipAddress = "127.0.0.1"; //IPv6���� ȣȯ���� ���� string�� �ַ� ����Ѵ�.

		// �̷� ������ ��� ����
		//public byte[] ipAddressArray = { 127, 0, 0, 1 }; 

		// 0~65,535 => ushort �������� ���ڸ� ����� �� ������(port �ּҴ� 2����Ʈ�� ��ȣ ���� ������ ���). C#������ int ���
		public int port = 9999; // 80 ������ ��Ʈ�� ��ǻ� ���� ������ �Ǿ� ����, 1000����� �ǵ�ġ �ʰ� ��ĥ �� ���� 9õ���� �̻� ��� ����

		
		private bool isConnected = false;

		private Thread serverMainThread;

		private List<Thread> serverThreads = new List<Thread>();

		public static Queue<string> log = new Queue<string>(); // ��� �����尡 ������ �� �ִ� Data ������ Queue

        private void Awake()
        {
			connetButton.onClick.AddListener(ServerConnectButtonClick);
        }

        public void ServerConnectButtonClick()
        {
            //  == !isConnected
            if (false == isConnected)
            {
                // ���⼱ ������ ����
                serverMainThread = new Thread(ServerThread);
                serverMainThread.IsBackground = true;
                serverMainThread.Start();
                isConnected = true;
            }
            else
            {
                // ���⼱ ������ �ݴ´�.
                serverMainThread.Abort();
                isConnected = false;
            }

            //serverThreads.Add(new Thread(ServerThread));
            //serverThreads[serverThreads.Count - 1].IsBackground = true;
            //serverThreads[serverThreads.Count - 1].Start();


        }

		// ��ſ��� Ȱ�������, �������� ����� �� �������� ������ å������ Input, Output ��Ʈ���� �ʿ���
		private StreamReader reader;
		private StreamWriter writer;
		private StreamReader reader1;
		private StreamWriter writer1;

		private void ServerThread() // ��Ƽ������� ������ �Ǿ�� ��
        {
			// ���� ����
			// ���� �����带 List�� �����Ͽ�
			// ���� ������ ������ ������ ��������

			TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);




			tcpListener.Start();    // tcp ���� ����

			//Text logText = Instantiate(messagePrefab, textArea);
			//logText.text = "���� ����";
			log.Enqueue("���� ����");


			List<TcpClient> clients = new List<TcpClient>();
			clients.Add(tcpListener.AcceptTcpClient());
			clients.Add(tcpListener.AcceptTcpClient());

			//TcpClient tcpClient = tcpListener.AcceptTcpClient();    // return�� �ö����� ��Ⱑ �ɸ���.

			//Text logText2 = Instantiate(messagePrefab, textArea);
			//logText2.text = "Ŭ���̾�Ʈ �����";
			log.Enqueue("Ŭ���̾�Ʈ �����");

            
			reader = new StreamReader(clients[0].GetStream());
			writer = new StreamWriter(clients[0].GetStream());
			reader1 = new StreamReader(clients[1].GetStream());
			writer1 = new StreamWriter(clients[1].GetStream());


			// ���� ����
			// true�� writer�� ���𰡸� �� ������ ������.
			writer.AutoFlush = true;
			writer1.AutoFlush = true;

			// �̷��� �Ǿ� ������ 4���� �ѹ��� ������ ��Ȳ
			//writer.WriteLine("�޽���");
			//writer.WriteLine("�޽���");
			//writer.WriteLine("�޽���");
			//writer.WriteLine("�޽���");
			//writer.Flush();





			while (clients[0].Connected && clients[1].Connected)
			{
				string readString = reader.ReadLine();
				string readString1 = reader1.ReadLine();

				if (string.IsNullOrEmpty(readString) && string.IsNullOrEmpty(readString1))
				{
					continue;
				}

				//Text messageText = Instantiate(messagePrefab, textArea);
				//messageText.text = readString;

				// ���� �޽����� �״�� Write�� ����
				writer.WriteLine($"1�� �� �޽���: {readString}");
				writer1.WriteLine($"2�� �� �޽���: {readString1}");

				log.Enqueue($"client1 message: {readString}");
				log.Enqueue($"client2 message: {readString1}");

			}

			log.Enqueue("Ŭ���̾�Ʈ ���� ����");


		}

        private void Update()
        {
            if (log.Count > 0)
            {
				Text logText = Instantiate(messagePrefab, textArea);
				logText.text = log.Dequeue();
            }
        }


    }
}
