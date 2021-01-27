using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class DBServer : MonoBehaviour
{
    private Socket client = null;

    // Start is called before the first frame update
    private void Awake()
    {
        StartClient("61.81.98.236", 9000);
    }
    //ip수정할것!!
    public bool StartClient(string ip, int port)
    {
        try
        {
            CreateSocket(ip, port);

            //수신 스레드 생성
            Thread thread = new Thread(new ParameterizedThreadStart(RecvThread));
            thread.Start(client);

            Debug.Log("접속성공");
           
            return true;
        }
        catch (Exception)
        {
            Debug.Log("접속실패");
            return false;
        }
    }

    #region 스레드 관련

    private void CreateSocket(string ip, int port)
    {
        client = new Socket(AddressFamily.InterNetwork,
                                      SocketType.Stream, ProtocolType.Tcp);

        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
        client.Connect(ipep);
    }

    private void RecvThread(object obj)
    {
        Socket clientSocket = (Socket)obj;
        while (true)
        {
            byte[] recvbyte = null;
            if (ReceiveData(clientSocket, ref recvbyte) == false)
                break;

            RecvData(recvbyte);
        }
    }

    #endregion

    #region 송수신

    //수신
    private bool ReceiveData(Socket client, ref byte[] data)
    {
        try
        {
            int total = 0;
            int size = 0;
            int left_data = 0;
            int recv_data = 0;

            // 수신할 데이터 크기 알아내기 
            byte[] data_size = new byte[1024];
            recv_data = client.Receive(data_size, 0, 1024, SocketFlags.None);
            size = BitConverter.ToInt32(data_size, 0);
            left_data = size;

            data = new byte[size];

            // 실제 데이터 수신
            while (total < size)
            {
                recv_data = client.Receive(data, total, left_data, 0);
                if (recv_data == 0) break;
                total += recv_data;
                left_data -= recv_data;
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
  

    //전송
    private bool SendData(Socket client, byte[] data)
    {
        try
        {
            int total = 0;
            int size = data.Length;
            int left_data = size;
            int send_data = 0;

            // 전송할 데이터의 크기 전달
            byte[] data_size = new byte[1024];
            data_size = BitConverter.GetBytes(size);
            send_data = client.Send(data_size);

            // 실제 데이터 전송
            while (total < size)
            {
                send_data = client.Send(data, total, left_data, SocketFlags.None);
                total += send_data;
                left_data -= send_data;
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion

    #region 송신정보

    //송신
    public bool SendData(string buf)
    {
        byte[] data = new byte[1024];
        data = Encoding.Default.GetBytes(buf);
        if (SendData(client, data) == false)
            return false;
        return true;
    }

    //회원가입
    public void SendInsertUser(string id, string pw)
    {
        //전송
        string packet = InsertUser(id, pw);
        SendData(packet);
    }
    public static string InsertUser(string id, string pw)
    {
        string msg = null;
        msg += "C_InsertUser"+"\a";    // 회원 가입 요청 메시지
        msg += id.Trim() + "#";
        msg += pw.Trim();
        return msg;
    }

    //로그인
    public void SendLoginUser(string id, string pw)
    {
        //전송
        string packet = LoginUser(id, pw);
        SendData(packet);
    }
    public static string LoginUser(string id, string pw)
    {
        string msg = null;
        msg += "C_UserLogin" + "\a";    // 회원 가입 요청 메시지
        msg += id.Trim() + "#";
        msg += pw.Trim();
        return msg;
    }

    #endregion

    #region 수신정보

    public void RecvData(byte[] data)
    {
        string msg = Encoding.Default.GetString(data);
        GameManager.Instance.PushData(msg);
    }
    #endregion
}
