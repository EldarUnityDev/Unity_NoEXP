using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    [FormerlySerializedAs("healthPoints")]
    public float maxHealth;
    public float currentHealth;

    public GameObject healthBarPrefab;
    public GameObject deathEffectPrefab;
    public GameObject explosiveDeath;

    public GameObject lootDrop;

    HealthBarBehaviour myHealthBar;

    public float bounty;
    public float chanceOfBounty;
    public float secondsForBoutyToDecay;
    float decayRate;

    public bool showScoreMenu;
    private void Start()
    {
        currentHealth = maxHealth;
        //Create our health panel on the canvas
        GameObject healthBarObject = Instantiate(healthBarPrefab, References.canvas.gameUIParent);
        myHealthBar = healthBarObject.GetComponent<HealthBarBehaviour>();

        if (Random.value > chanceOfBounty) //� ���� �� ��������� ���� �������, �� �� � �������
        {
            bounty = 0;
        }
        if (secondsForBoutyToDecay != 0)
        {
            decayRate = bounty / secondsForBoutyToDecay;
        }

        myHealthBar.bountyText.text = bounty.ToString();
    }

    public void KillMe()
    {
        TakeDamage(currentHealth);
    }
    public void ReplenishHealth()
    {
        currentHealth = maxHealth;
    }

    //public - ������ ������� ������ ��� �������, void - �� ���������� ��������
    public void TakeDamage(float damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                if (GetComponent<EnemyBehaviour>() != null)
                {
                    if (deathEffectPrefab != null && !GetComponent<EnemyBehaviour>().explodeOnTouch)
                    {
                        Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                    }
                    else //����� ������, ���� ��� �� ���� �� �������
                    {
                        Instantiate(explosiveDeath, transform.position, transform.rotation);
                    }
                }
                else
                {
                    Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                }

                if (lootDrop != null)
                {
                    Instantiate(lootDrop, transform.position, transform.rotation);
                }
                //References.scoreManager.IncreaseScore(Mathf.FloorToInt(bounty));
                if (showScoreMenu)
                {
                    References.canvas.ShowScoreMenu();
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        //don't create anything in ONDESTROY - it's only for cleaning up
        if (myHealthBar != null) //��� ������ ���� �����, ����� �� ���� ������ �� ��������� ������
        {
            Destroy(myHealthBar.gameObject);
        }
    }


    private void Update()
    {
        //make our healthbar reflect our health - ShowHealth();
        myHealthBar.ShowHealthFraction(currentHealth / maxHealth);
        //make our healthbar follow us
        //������� WorldToScreenPoint - ������������ 3� � 2�
        myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);

        if (bounty > 0) //���� ���� �������
        {
            bounty -= decayRate * Time.deltaTime;
            myHealthBar.bountyText.enabled = true;
            myHealthBar.bountyText.text = Mathf.FloorToInt(bounty).ToString();

        }
        else            //���� ��� �������
        {
            myHealthBar.bountyText.enabled = false;
        }
    }



}
