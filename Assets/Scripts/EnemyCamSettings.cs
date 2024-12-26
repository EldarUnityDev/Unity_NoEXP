using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCamSettings : MonoBehaviour
{
    //public GameObject enemyCamBorder;//������ �����
    //public GameObject enemyCamImg; //������ ������������� �� Image
    public List<GameObject> enemyCamBorders = new List<GameObject>(); //�����
    public List<GameObject> camImages = new List<GameObject>(); //���� ������������� ������
    public int currentUnoccupiedCam = 0;


    void Start()
    {
       // enemyCamBorder.SetActive(false);
        //enemyCamImg.SetActive(false);
        //�������� ������ � ������:
        References.enemyCams = this;
    }

    private void Update()
    {
        //������ ������ ������� �� ������, ���� �� ��� ��������� � �����
        //� ����� ��������� ������ ��� ��� �������
        //! theCam.transform.LookAt(References.thePlayer.transform.position); 
    }
    public void turnOnTheCamera()
    {
        for(int i = 0; i < enemyCamBorders.Count; i++)
        {
            if (!enemyCamBorders[i].activeInHierarchy) //���� �� �����������
            {
                enemyCamBorders[i].SetActive(true); //������������ ������ ��������� ������
                camImages[i].SetActive(true);
                currentUnoccupiedCam = i;
                break;
            }
            else 
            { 
                currentUnoccupiedCam = camImages.Count + 1;
            }
        }
    }
    public void turnOnTheTriggerCamera()
    {
            if (camImages[4].activeInHierarchy == false) //���� �� �����������
            {
                camImages[4].SetActive(true);
            }
    }
    public void turnOffTheTriggerCamera()
    {
        if (camImages[4].activeInHierarchy) //���� �����������
        {
            camImages[4].SetActive(false);
        }
    }
    public void turnOffTheCamera(int n)
    {
        enemyCamBorders[n].SetActive(false);
        camImages[n].SetActive(false);
    }
    public int GiveTicket()
    {
        return currentUnoccupiedCam;
    }
}
