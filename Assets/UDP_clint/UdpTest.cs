using UnityEngine;
using System.Collections;
using System;
using System.Globalization;
public class UdpTest : MonoBehaviour
{
    string editString = "hello wolrd";//Edit box text
    GameObject cube;



    public AirplaneController airplaneController = new AirplaneController();
    UdpClientHandler udpClient;
    //Use this for initialization
    void Start()
    {
        //Initialize the network
        udpClient = gameObject.AddComponent<UdpClientHandler>();
        udpClient.InitSocket();

        airplaneController = FindObjectOfType<AirplaneController>();

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
            //Debug.Log(udpClient.GetRecvStr());

            if (udpClient.GetRecvStr().StartsWith("Pow"))
            {
                float value = float.Parse(udpClient.GetRecvStr().Substring(3), CultureInfo.InvariantCulture.NumberFormat);
                airplaneController.thrustPercent = value;
                //Debug.Log("This is Power"+value);
            }
            else if (udpClient.GetRecvStr().StartsWith("Ang"))
            {
                float value = float.Parse(udpClient.GetRecvStr().Substring(3), CultureInfo.InvariantCulture.NumberFormat);
                airplaneController.Flap = value;
                // Debug.Log("This is Angle" + value); ;
            }
            else if (udpClient.GetRecvStr().StartsWith("Ail"))
            {
                float value = float.Parse(udpClient.GetRecvStr().Substring(3), CultureInfo.InvariantCulture.NumberFormat);
                airplaneController.Roll = value;
                //Debug.Log("This is Aileron" + value);
            }
            else if (udpClient.GetRecvStr().StartsWith("ele"))
            {
                float value = float.Parse(udpClient.GetRecvStr().Substring(3), CultureInfo.InvariantCulture.NumberFormat);
                airplaneController.Pitch = value;
                //Debug.Log("This is Elevator" + value);
            }
            else if (udpClient.GetRecvStr().StartsWith("rud"))
            {
                float value = float.Parse(udpClient.GetRecvStr().Substring(3), CultureInfo.InvariantCulture.NumberFormat);
                airplaneController.Yaw = value;
               // Debug.Log("This is Rudder" + value);
            }


        }
    }

    void OnApplicationQuit()
    {
        //Close the connection when exiting
        udpClient.SocketQuit();
    }
}