using UnityEngine;
using System.Collections;
using System;
using System.Globalization;
public class UdpTest : MonoBehaviour
{
    string editString = "hello wolrd";//Edit box text
    GameObject cube;

    UdpClientHandler udpClient;
    //Use this for initialization
    void Start()
    {
        //Initialize the network
        udpClient = gameObject.AddComponent<UdpClientHandler>();
        udpClient.InitSocket();

        //Find the cube
        cube = GameObject.Find("Cube");
    }

    void OnGUI()
    {
        editString = GUI.TextField(new Rect(10, 10, 100, 20), editString);
        GUI.Label(new Rect(10, 30, 300, 20), udpClient.GetRecvStr().ToString());
        if (GUI.Button(new Rect(10, 50, 60, 20), "send"))
            udpClient.SocketSend(editString);
    }

    //Update is called once per frame
    void Update()
    {
        if (udpClient.GetRecvStr() != null)
        {
            Debug.Log(udpClient.GetRecvStr());

            float value = float.Parse(udpClient.GetRecvStr(), CultureInfo.InvariantCulture.NumberFormat);
            Debug.Log("float deðeri: "+value);
        }
    }

    void OnApplicationQuit()
    {
        //Close the connection when exiting
        udpClient.SocketQuit();
    }
}