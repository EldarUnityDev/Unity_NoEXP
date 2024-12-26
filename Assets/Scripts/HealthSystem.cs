using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    [FormerlySerializedAs ("healthPoints")]
    public float maxHealth;
    float currentHealth;

    public GameObject healthBarPrefab;
    public GameObject deathEffectPrefab;

    HealthBarBehaviour myHealthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        //Create our health panel on the canvas
        GameObject healthBarObject = Instantiate(healthBarPrefab, References.canvas.transform);
        myHealthBar = healthBarObject.GetComponent<HealthBarBehaviour>();
    }

    public void KillMe()
    {
        TakeDamage(currentHealth);
    }

    //public - другие скрипты увидят эту функцию, void - не возвращает значений
    public void TakeDamage(float damageAmount)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                if (deathEffectPrefab != null)
                {
                    Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        //don't create anything in ONDESTROY - it's only for cleaning up
        if(myHealthBar != null) //это только ради юнити, чтобы не было ошибки по окончанию сессии
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


    }



}
