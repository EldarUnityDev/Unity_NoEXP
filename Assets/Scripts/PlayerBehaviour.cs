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
        References.thePlayer = this; //референсы всегда надо в Awake. чтобы на старте к ним могли обратитьс€
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
        //ѕ–»÷≈Ћ»¬јЌ»≈
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        //чтобы сделать плоскость, юнити надо узнать, (где "верх", и откуда начинать)
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        //пускаем луч и выплЄвываем его длину до точки на плоскости
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        //конвертируем в вектор 1-направление через ray и длину через дистанцию по лучу 
        //или по-другому вот_по_этому_лучу.найди¬ектор(вот“акойƒлины).
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);



        Vector3 lookAtPosition = cursorPosition; //можно удалить?
        transform.LookAt(lookAtPosition);
        if (weapons.Count > 0 && Input.GetButton("Fire1"))
        {
            weapons[selectedWeaponIndex].Fire(cursorPosition);
        }

        if (Input.GetButtonDown("Fire2")) //не всегда работает
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
        //дл€ каждого оружи€
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeaponIndex)
            {
                //если мы его выбрали, включи его
                weapons[i].gameObject.SetActive(true);

            }
            else
            {
                //если не выбрано, деактивируй
                weapons[i].gameObject.SetActive(false);
            }
        }
    }
}
