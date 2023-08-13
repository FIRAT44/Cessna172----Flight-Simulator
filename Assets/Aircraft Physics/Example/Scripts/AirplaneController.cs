using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AirplaneController : MonoBehaviour
{
    [SerializeField]
    List<AeroSurface> controlSurfaces = null;
    [SerializeField]
    List<WheelCollider> wheels = null;
    [SerializeField]
    float rollControlSensitivity = 0.2f;
    [SerializeField]
    float pitchControlSensitivity = 0.2f;
    [SerializeField]
    float yawControlSensitivity = 0.2f;


    
    [Range(-1, 1)]
    public float Pitch;
    [Range(-1, 1)]
    public float Yaw;
    [Range(-1, 1)]
    public float Roll;
    [Range(0, 1)]
    

  //  public float Pitch;
   // public float Yaw;
    //public float Roll;

    public float Flap;
    [SerializeField]
    Text displayText = null;

    public float thrustPercent;
    public float brakesTorque;

    AircraftPhysics aircraftPhysics;
    Rigidbody rb;


    
    public TMP_Text flapController;

    public bool flightCheck = false;

    private void Start()
    {
        aircraftPhysics = GetComponent<AircraftPhysics>();
        rb = GetComponent<Rigidbody>();

        
    }

    private void Update()
    {
        
        Pitch = Input.GetAxis("Vertical");
        Roll = Input.GetAxis("Horizontal");
        Yaw = Input.GetAxis("Yaw");
        
        
        //Debug.Log(Pitch);
        //Debug.Log(Roll);
        //Debug.Log(Yaw);
        flapController.text = "Flap = "+(Flap*100).ToString();
        /* Eski kod
        if (Input.GetKeyDown(KeyCode.Space))
        {
            thrustPercent = thrustPercent > 0 ? 0 : 0.15f;
        }*/


        // yeni kod
        if (flightCheck)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                thrustPercent = thrustPercent > 0 ? 0 : 0.15f;
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                thrustPercent = thrustPercent = 0.50f;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                thrustPercent = thrustPercent = 1f;
            }
        }
        else
        {
            Debug.Log("Tüm kontrolleri yap");
        }


        
        // yeni kod

        if (Input.GetKeyDown(KeyCode.F))
        {
            //Flap = Flap > 0 ? 0  : 0.3f;

            if(Flap > 0)
            {
                Flap = 0;
                flapController.text = "Flap = OFF";
            }
            else
            {
                Flap = 0.3f;
                flapController.text = "Flap = ON";
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            brakesTorque = brakesTorque > 0 ? 0 : 100f;

        }

        displayText.text = "V: " + ((int)rb.velocity.magnitude).ToString("D3") + " m/s\n";
        displayText.text += "A: " + ((int)transform.position.y).ToString("D4") + " m\n";
        displayText.text += "D(z): " + ((int)transform.position.z).ToString("D4") + " m\n";
        displayText.text += "D(x): " + ((int)transform.position.x).ToString("D4") + " m\n";
        displayText.text += "T: " + (int)(thrustPercent * 100) + "%\n";
        displayText.text += brakesTorque > 0 ? "B: ON" : "B: OFF";
    }

    private void FixedUpdate()
    {
        SetControlSurfecesAngles(Pitch, Roll, Yaw, Flap);
        aircraftPhysics.SetThrustPercent(thrustPercent);
        foreach (var wheel in wheels)
        {
            wheel.brakeTorque = brakesTorque;
            // small torque to wake up wheel collider
            wheel.motorTorque = 0.01f;
        }
    }

    public void SetControlSurfecesAngles(float pitch, float roll, float yaw, float flap)
    {
        foreach (var surface in controlSurfaces)
        {
            if (surface == null || !surface.IsControlSurface) continue;
            switch (surface.InputType)
            {
                case ControlInputType.Pitch:
                    surface.SetFlapAngle(pitch * pitchControlSensitivity * surface.InputMultiplyer);
                    break;
                case ControlInputType.Roll:
                    surface.SetFlapAngle(roll * rollControlSensitivity * surface.InputMultiplyer);
                    break;
                case ControlInputType.Yaw:
                    surface.SetFlapAngle(yaw * yawControlSensitivity * surface.InputMultiplyer);
                    break;
                case ControlInputType.Flap:
                    surface.SetFlapAngle(Flap * surface.InputMultiplyer);
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            SetControlSurfecesAngles(Pitch, Roll, Yaw, Flap);
    }
}
