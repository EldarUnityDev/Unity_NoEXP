using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    //public float health;

    Rigidbody m_Rigidbody;

    public WeaponBehaviour mainWeapon;
    public WeaponBehaviour secondaryWeapon;
    public int score;
    private void Awake()
    {
        References.thePlayer = this; //��������� ������ ���� � Awake. ����� �� ������ � ��� ����� ����������
    }
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        References.canvas.scoreText.text = score.ToString();
    }

    // Update is called once per frame
    private void Update()
    {

        //Movement
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (inputVector.magnitude > 0)
        {
            m_Rigidbody.velocity = inputVector * speed;
        }
        //������������
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        //����� ������� ���������, ����� ���� ������, (��� "����", � ������ ��������)
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        //������� ��� � ���������� ��� ����� �� ����� �� ���������
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        //������������ � ������ 1-����������� ����� ray � ����� ����� ��������� �� ���� 
        //��� ��-������� ���_��_�����_����.�����������(�������������).
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);



        Vector3 lookAtPosition = cursorPosition; //����� �������?
        transform.LookAt(lookAtPosition);
        if (mainWeapon != null && Input.GetButton("Fire1"))
        {
            mainWeapon.Fire(cursorPosition);
        }

        if (Input.GetButtonDown("Fire2")) //�� ������ ��������
        {
            SwitchWeapons();
        }

        if (Input.GetButtonDown("Use"))
        {
            //use the nearest usable
            Useable nearestUseableSoFar = null;
            float nearestDistance = 3; //max pickup distance
            foreach (Useable thisUseable in References.useables)
            {
                //how far is this one from the player?
                float thisDistance = Vector3.Distance(transform.position, thisUseable.transform.position);
                //is it closer than anything else we've found?
                if (thisDistance <= nearestDistance)
                {
                    //if it's THIS now it's the closest one
                    nearestUseableSoFar = thisUseable;
                    nearestDistance = thisDistance;
                }
                //+++ challenge - check if it's in front of us
            }

            if (nearestUseableSoFar != null)
            {
                nearestUseableSoFar.Use();
            }

        }
    }

    public void PickUpWeapon(WeaponBehaviour weapon)
    {
        if (mainWeapon == null)
        {
            //we don't have a main weapon, use THIS one
            SetAsMainWeapon(weapon);
        }
        else
        {
            //we DO have a main weapon
            if (secondaryWeapon == null)
            {
                //there's nothing in the secondary slot, put it there
                SetAsSecondaryWeapon(weapon);
            }
            else
            {
                //both slots are already full:
                //drop the old main weapon
                mainWeapon.Drop();
                //the new weapon becomes main weapon
                SetAsMainWeapon(weapon);


            }


        }
    }

    void SetAsSecondaryWeapon(WeaponBehaviour weapon)
    {
        secondaryWeapon = weapon;
        References.canvas.secondaryWeaponPanel.AssignWeapon(weapon);
        weapon.gameObject.SetActive(false);
    }
    void SetAsMainWeapon(WeaponBehaviour weapon)
    {
        mainWeapon = weapon;
        References.canvas.mainWeaponPanel.AssignWeapon(weapon);
        weapon.gameObject.SetActive(true);
    }

    private void SwitchWeapons()
    {
        if (mainWeapon != null && secondaryWeapon != null)
        {
            WeaponBehaviour oldMainWeapon = mainWeapon;
            SetAsMainWeapon(secondaryWeapon);
            SetAsSecondaryWeapon(oldMainWeapon);
        }
        // weapons[i].gameObject.SetActive(true);
        //weapons[i].gameObject.SetActive(false);

    }
}
