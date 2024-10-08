using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System;

namespace MyProject
{
	public class ServerManager : MonoBehaviour
	{
		public Button connetButton;
		public Text messagePrefab;
		public RectTransform textArea;

		public string ipAddress = "127.0.0.1"; //IPv6와의 호환성을 위해 string을 주로 사용한다.

		// 이런 식으로 사용 가능
		//public byte[] ipAddressArray = { 127, 0, 0, 1 }; 

		// 0~65,535 => ushort 사이즈의 숫자만 취급할 수 있으나(port 주소는 2바이트의 부호 없는 정수를 사용). C#에서는 int 사용
		public int port = 9999; // 80 이전의 포트는 사실상 거의 선점이 되어 있음, 1000번대는 의도치 않게 겹칠 수 있음 9천번대 이상 사용 권장

		
		private bool isConnected = false;

		private Thread serverMainThread;

        private List<ClientHandler> clients = new List<ClientHandler>();
        private List<Thread> threads = new List<Thread>();

		public static Queue<string> log = new Queue<string>(); // 모든 스레드가 접근할 수 있는 Data 영역의 Queue

        private void Awake()
        {
			connetButton.onClick.AddListener(ServerConnectButtonClick);
        }

        public void ServerConnectButtonClick()
        {
            //  == !isConnected
            if (false == isConnected)
            {
                // 여기선 서버를 열고
                serverMainThread = new Thread(ServerThread);
                serverMainThread.IsBackground = true;
                serverMainThread.Start();
                isConnected = true;
            }
            else
            {
                // 여기선 서버를 닫는다.
                serverMainThread.Abort();
                isConnected = false;
            }

            //serverThreads.Add(new Thread(ServerThread));
            //serverThreads[serverThreads.Count - 1].IsBackground = true;
            //serverThreads[serverThreads.Count - 1].Start();
        }

		// 통신에도 활용되지만, 데이터의 입출력 등 데이터의 전송을 책임지는 Input, Output 스트림이 필요함
		private StreamReader reader;
		private StreamWriter writer;
		//private StreamReader reader1;
		//private StreamWriter writer1;
        private int clientID = 0;

		private void ServerThread() // 멀티스레드로 생성이 되어야 함
        {
            // try / catch 문의 용도: Excception 발생 시 메세지를 수동적으로 활용할 수 있도록 함.
            // 잘 제어된 if-else문과 비슷하다.
            try  // if 블록 안의 구문에서 에러가 없을 경우
            {
                TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);


                tcpListener.Start();    // tcp 서버 가동
                //Text logText = Instantiate(messagePrefab, textArea);
                //logText.text = "서버 시작";
                log.Enqueue("서버 시작");


                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();

                    ClientHandler handler = new ClientHandler();
                    handler.Connect(clientID++, this, client);

                    clients.Add(handler);

                    Thread clientThread = new Thread(handler.Run);
                    clientThread.IsBackground = true;
                    clientThread.Start();

                    threads.Add(clientThread);

                    log.Enqueue($"{handler.id}번 클라이언트가 접속됨.");
                }

                


                //List<TcpClient> clients = new List<TcpClient>();
                //clients.Add(tcpListener.AcceptTcpClient());
                //clients.Add(tcpListener.AcceptTcpClient());

                ////TcpClient tcpClient = tcpListener.AcceptTcpClient();    // return이 올때까지 대기가 걸린다.

                ////Text logText2 = Instantiate(messagePrefab, textArea);
                ////logText2.text = "클라이언트 연결됨";
                //log.Enqueue("클라이언트 연결됨");


                //reader = new StreamReader(clients[0].GetStream());
                //writer = new StreamWriter(clients[0].GetStream());
                //reader1 = new StreamReader(clients[1].GetStream());
                //writer1 = new StreamWriter(clients[1].GetStream());


                //// 선택 사항
                //// true면 writer에 무언가를 쓸 때마다 보낸다.
                //writer.AutoFlush = true;
                //writer1.AutoFlush = true;

                //// 이렇게 되어 있으면 4개를 한번에 보내는 상황
                ////writer.WriteLine("메시지");
                ////writer.WriteLine("메시지");
                ////writer.WriteLine("메시지");
                ////writer.WriteLine("메시지");
                ////writer.Flush();





                //while (clients[0].Connected && clients[1].Connected)
                //{
                //    string readString = reader.ReadLine();
                //    string readString1 = reader1.ReadLine();

                //    if (string.IsNullOrEmpty(readString) && string.IsNullOrEmpty(readString1))
                //    {
                //        continue;
                //    }

                //    //Text messageText = Instantiate(messagePrefab, textArea);
                //    //messageText.text = readString;

                //    // 받은 메시지를 그대로 Write에 쓴다
                //    writer.WriteLine($"1이 쓴 메시지: {readString}");
                //    writer1.WriteLine($"2이 쓴 메시지: {readString1}");

                //    log.Enqueue($"client1 message: {readString}");
                //    log.Enqueue($"client2 message: {readString1}");

                //}

                //log.Enqueue("클라이언트 연결 종료");
            }
            catch (ArgumentException e)
            {
                // 여러 개 사용가능 
                log.Enqueue("파라미터 에러 발생");
                log.Enqueue(e.Message);
            }
            catch (NullReferenceException e)
            {
                //
                log.Enqueue("null pointer 에러 발생");
                log.Enqueue(e.Message);
            }
            catch (Exception e) // try 문 내의 구문중에 어디서든 에러가 발생할 시 호출
            {
                log.Enqueue("에러 발생");
                log.Enqueue(e.Message);
            }
            finally // try문 내에서 에러가 발생해도 실행되고, 안해도 실행됨.
                    // 중간에 흐름이 끊기지 않고 생성된 객체를 해제하는 등의 반드시 필요한 절차를 수행함
            {
                foreach (var thread in threads)
                {
                    thread?.Abort();
                }
            }
        }

        public void Disconnect(ClientHandler client)
        {
            clients.Remove(client);
        }

        public void BroadcastToClients(string message)
        {
            log.Enqueue(message);

            foreach (ClientHandler client in clients)
            {
                client.MessageToClient(message);
            }
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

// 클라이언트가 TCP 접속 요청을 할 때마다 해당 클라이언트를 붙들고 있는 객체를 생성한다.
public class ClientHandler
{
    public int id;
    public MyProject.ServerManager server;
    public TcpClient tcpClient;
    public StreamReader reader;
    public StreamWriter writer;


    public void Connect(int id, MyProject.ServerManager server, TcpClient tcpClient)
    {
        this.id = id;
        this.server = server;
        this.tcpClient = tcpClient;

        reader = new StreamReader(tcpClient.GetStream());
        writer = new StreamWriter(tcpClient.GetStream());

        writer.AutoFlush = true;
    }

    public void Disconnect()
    {
        writer.Close();
        reader.Close();
        tcpClient.Close();
        server.Disconnect(this);
    }

    public void MessageToClient(string message)
    {
        writer.WriteLine(message);
    }

    public void Run()
    {
        try
        {
            while (tcpClient.Connected)
            {
                string readString = reader.ReadLine();

                if (string.IsNullOrEmpty(readString))
                {
                    continue;
                }

                // 읽어온 메시지가 있으면 서버에게 전달.


                server.BroadcastToClients($"{id}님의 말: {readString}");
            }

        }
        catch(Exception e)
        {
            MyProject.ServerManager.log.Enqueue($"{id} 번 클라이언트 오류 발생: {e.Message}");
        }
        finally
        {
            Disconnect();
        }
    }

}