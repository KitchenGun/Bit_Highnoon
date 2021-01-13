using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class DBServer : MonoBehaviour
{
    TcpListener Server;
    TcpClient Client;

    string serverIP = "127.0.0.1";
    int port = 9000;
    byte[] recevBuffer;

    // Start is called before the first frame update
    void Start()
    {
        DBSend();
    }

    public void DBReceive()
    {
        Client = new TcpClient(serverIP, port);
        NetworkStream stream;
        stream = Client.GetStream();

        recevBuffer = new byte[14]; // "Do you hear me" 길이 = 14
        stream.Read(recevBuffer, 0, recevBuffer.Length); // stream에 있던 바이트배열 내려서 새로 선언한 바이트배열에 넣기
        string msg = Encoding.UTF8.GetString(recevBuffer, 0, recevBuffer.Length); // byte[] to string
        Debug.Log(msg);
    }

    public void DBSend()
    {
        Client = new TcpClient(serverIP, port);
        NetworkStream stream;
        stream = Client.GetStream();

        string msg = "1"; // test Text
        int byteCount = Encoding.UTF8.GetByteCount(msg); // msg 바이트크기

        byte[] sendBuffer = new byte[byteCount];
        sendBuffer = Encoding.UTF8.GetBytes(msg); // 바이트배열에 msg담기
        stream.Write(sendBuffer, 0, sendBuffer.Length); // stream에 바이트배열 실어보내기
    }

    
}
