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

    public List<WeaponBehaviour> weapons = new List<WeaponBehaviour>();
    public int selectedWeaponIndex = 0;

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
        if (weapons.Count > 0 && Input.GetButton("Fire1"))
        {
            weapons[selectedWeaponIndex].Fire(cursorPosition);
        }

        if (Input.GetButtonDown("Fire2")) //�� ������ ��������
        {
            ChangeWeaponIndex(selectedWeaponIndex + 1);
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

    public void SelectLatestWeapon()
    {
        ChangeWeaponIndex(weapons.Count - 1);
    }
    private void ChangeWeaponIndex(int index)
    {
        selectedWeaponIndex = index;
        if (selectedWeaponIndex >= weapons.Count)
        {
            selectedWeaponIndex = 0;
        }
        //��� ������� ������
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeaponIndex)
            {
                //���� �� ��� �������, ������ ���
                weapons[i].gameObject.SetActive(true);

            }
            else
            {
                //���� �� �������, �����������
                weapons[i].gameObject.SetActive(false);
            }
        }
    }
}
