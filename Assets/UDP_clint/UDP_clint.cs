using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDP_clint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UDPTest();
    }

    void Update()
    {
        
    }

    void UDPTest()
    {
        UdpClient clint = new UdpClient(5600);
        try
        {
            clint.Connect("127.0.0.1", 5500);
            byte[] sendBytes = Encoding.ASCII.GetBytes("Hello, from the clieny");
            clint.Send(sendBytes, sendBytes.Length);

            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0); // 0 yerine 5500 yazýlabilir.

            byte[] RECEÝVEBytes = clint.Receive(ref remoteEndPoint);
            string receivedString = Encoding.ASCII.GetString(RECEÝVEBytes);

            print("Message received from the server \n" + receivedString);
        }
        catch (Exception e)
        {

            print("Exception thrown " + e.Message);
        }
    }
}
