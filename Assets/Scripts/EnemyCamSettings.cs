using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCamSettings : MonoBehaviour
{
    //public GameObject enemyCamBorder;//чЄрна€ рамка
    //public GameObject enemyCamImg; //камера транслируетс€ на Image
    public List<GameObject> enemyCamBorders = new List<GameObject>(); //рамки
    public List<GameObject> camImages = new List<GameObject>(); //куда транслировать камеры
    public int currentUnoccupiedCam = 0;


    void Start()
    {
       // enemyCamBorder.SetActive(false);
        //enemyCamImg.SetActive(false);
        //добавить камеру в список:
        References.enemyCams = this;
    }

    private void Update()
    {
        //камера всегда смотрит на игрока, хот€ всЄ ещЄ прив€зана к плечу
        //и может проходить сквозь пол при кувырке
        //! theCam.transform.LookAt(References.thePlayer.transform.position); 
    }
    public void turnOnTheCamera()
    {
        for(int i = 0; i < enemyCamBorders.Count; i++)
        {
            if (!enemyCamBorders[i].activeInHierarchy) //если не активирован
            {
                enemyCamBorders[i].SetActive(true); //активировать первую незан€тую камеру
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
            if (camImages[4].activeInHierarchy == false) //если не активирован
            {
                camImages[4].SetActive(true);
            }
    }
    public void turnOffTheTriggerCamera()
    {
        if (camImages[4].activeInHierarchy) //если активирован
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
