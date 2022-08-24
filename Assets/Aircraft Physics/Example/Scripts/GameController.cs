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

    [SerializeField]
    GameObject camFront,camLeft,camRight;


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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            camFront.SetActive(true);
            camLeft.SetActive(false);
            camRight.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            camFront.SetActive(false);
            camLeft.SetActive(false);
            camRight.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            camLeft.SetActive(true);
            camRight.SetActive(false);
            camFront.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            camRight.SetActive(true);
            camLeft.SetActive(false);
            camFront.SetActive(false);
        }
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
