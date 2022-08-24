using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField]
    GameObject CheckListPanel;

    [SerializeField]
    GameObject RudderCheck;

    [SerializeField]
    GameObject ElevatorCheck;


    AirplaneController airplaneController;


    private void Awake()
    {
        CheckListPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void Start()
    {
        airplaneController = Object.FindObjectOfType<AirplaneController>();   
    }

    public void AVcheck()
    {
        if (RudderCheck.GetComponent<Toggle>().isOn == true)
        {
            CheckListPanel.SetActive(false);
            
            Time.timeScale = 1;
            airplaneController.flightCheck = true;
        }

        
    }

    public void BackPlane()
    {
        CheckListPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void CheckListOpen()
    {
        CheckListPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
