using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    [FormerlySerializedAs("healthPoints")]
    public float maxHealth;
    public float currentHealth;
    public Rigidbody ourRigidBody;
    public NavMeshAgent agent;

    public GameObject healthBarPrefab;
    public GameObject deathEffectPrefab;
    public GameObject explosiveDeath;

    public GameObject lootDrop;

    HealthBarBehaviour myHealthBar;

    public float bounty;
    public float chanceOfBounty;
    public float secondsForBoutyToDecay;
    float decayRate;

    public bool readyToDie;
    public bool startSelfDestruct;

    public bool showScoreMenu;

    public float selfDestructTimer;
    private void Start()
    {
        readyToDie = true;
        currentHealth = maxHealth;
        //Create our health panel on the canvas
        GameObject healthBarObject = Instantiate(healthBarPrefab, References.canvas.gameUIParent);
        myHealthBar = healthBarObject.GetComponent<HealthBarBehaviour>();

        if (Random.value > chanceOfBounty) //у всех по умолчанию есть награда, но мы её стираем
        {
            bounty = 0;
        }
        if (secondsForBoutyToDecay != 0)
        {
            decayRate = bounty / secondsForBoutyToDecay;
        }

        myHealthBar.bountyText.text = bounty.ToString();
        ourRigidBody = GetComponent<Rigidbody>(); //чтобы управлять физикой
        agent = GetComponent<NavMeshAgent>();     //чтобы управлять физикой

        selfDestructTimer = 3;
    }

    public void KillMe()
    {
        TakeDamage(currentHealth);
    }
    public void ReplenishHealth()
    {
        currentHealth = maxHealth;
    }

    //public - другие скрипты увидят эту функцию, void - не возвращает значений
    public void TakeDamage(float damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                if (!readyToDie)
                {
                    TurnIntoRagdoll(); //если не готовы умереть, do we need "else"?
                }
                if (readyToDie)
                {
                    if (GetComponent<EnemyBehaviour>() != null)
                    {
                        if (deathEffectPrefab != null && !GetComponent<EnemyBehaviour>().explodeOnTouch)
                        {
                            Instantiate(deathEffectPrefab, transform.position, transform.rotation); //обычная смерть
                        }
                        else //будет ошибка, если нет ни того ни другого
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

                    Destroy(gameObject);
                }



                //References.scoreManager.IncreaseScore(Mathf.FloorToInt(bounty));
                /*if (showScoreMenu)
                {
                    References.canvas.ShowScoreMenu();
                }*/
                //Destroy(gameObject);
                //enabled = false; //отключаем скрипт
            }
        }
    }

    public void TurnIntoRagdoll()
    {
        if (agent != null)
        {
            agent.enabled = false; //выключаем навигацию
        }
        transform.parent = null;
        ourRigidBody.isKinematic = false; //чтобы можно было влиять на физику
        ourRigidBody.constraints = RigidbodyConstraints.None;
        GetComponent<KnockBackScript>().knockbackTime = 100;

        GetComponent<KnockBackScript>().GetKnockedBack(-gameObject.transform.forward, 15);
        startSelfDestruct = true;
    }

    private void OnDestroy()
    {
        //don't create anything in ONDESTROY - it's only for cleaning up
        if (myHealthBar != null) //это только ради юнити, чтобы не было ошибки по окончанию сессии
        {
            Destroy(myHealthBar.gameObject);
        }
    }


    private void Update()
    {
        //make our healthbar reflect our health - ShowHealth();
        myHealthBar.ShowHealthFraction(currentHealth / maxHealth);
        //make our healthbar follow us
        //функция WorldToScreenPoint - конвертирует 3д в 2д
        myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);

        if (bounty > 0) //если есть награда
        {
            bounty -= decayRate * Time.deltaTime;
            myHealthBar.bountyText.enabled = true;
            myHealthBar.bountyText.text = Mathf.FloorToInt(bounty).ToString();

        }
        else            //если нет награды
        {
            myHealthBar.bountyText.enabled = false;
        }

        //самоуничтожение, как у пули
        if (startSelfDestruct)
        {
            selfDestructTimer -= Time.deltaTime;
            if (selfDestructTimer < 1)
            {
                transform.localScale *= selfDestructTimer;

            }
            if (selfDestructTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }



}
