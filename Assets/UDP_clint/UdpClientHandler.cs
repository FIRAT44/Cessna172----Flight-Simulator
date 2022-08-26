using UnityEngine;
using System.Collections;
//Import library
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UdpClientHandler : MonoBehaviour
{
    //The following are private members by default
    Socket socket;//Target socket
    EndPoint serverEnd;//Server
    IPEndPoint ipEnd;//Server port
    string recvStr;//Received string
    string sendStr;//The string sent
    byte[] recvData = new byte[1024];//The received data must be bytes
    byte[] sendData = new byte[1024];//The data sent must be bytes
    int recvLen;//The received data length
    Thread connectThread;//Connect thread

    //initialization
    public void InitSocket()
    {
        //Define the connected server ip and port, which can be local ip, LAN, Internet
        ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
        //Define the socket type, defined in the main thread
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //Define the server
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        serverEnd = (EndPoint)sender;
        print("waiting for sending UDP dgram");

        //Establish an initial connection, this sentence is very important, the first connection to initialize the serverEnd to receive the message
        SocketSend("hello");

        //Open a thread connection, necessary, otherwise the main thread is stuck
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
    }

    public void SocketSend(string sendStr)
    {
        //Empty the sending buffer
        sendData = new byte[1024];
        //Data type conversion
        sendData = Encoding.ASCII.GetBytes(sendStr);
        //Send to the designated server
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
    }

    //The server receives
    void SocketReceive()
    {
        //Enter the receiving loop
        while (true)
        {
            //Clear data
            recvData = new byte[1024];
            //Get the client, get the server-side data, use the reference to assign the server-side value, in fact the server-side has been defined and does not need to be assigned
            recvLen = socket.ReceiveFrom(recvData, ref serverEnd);
            print("message from: " + serverEnd.ToString());//Print server information
                                                           //Output the received data
            recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
            print(recvStr);
        }
    }

    //Return the received string  
    public string GetRecvStr()
    {
        string returnStr;
        //Lock to prevent the string from being changed  
        lock (this)
        {
            returnStr = recvStr;
        }
        return returnStr;
    }

    //The connection is closed
    public void SocketQuit()
    {
        //Close the thread
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //Finally close the socket
        if (socket != null)
            socket.Close();
    }

}