using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    private GameObject enemyPrefab;
    public GameObject spawnPoint;
    public float secondsBetweenSpawns;
    float secondsSinceLastSpawn;

    public int enemiesToSpawn;

    private void OnEnable()
    {
        References.spawners.Add(this);
    }
    // Update is called once per frame
    private void OnDisable()
    {
        References.spawners.Remove(this);

    }

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastSpawn = 0;
        //int randomEnemyIndex = Random.Range(0, References.levelGenerator.swarmerTypes.Count);
        //enemyPrefab = References.levelGenerator.swarmerTypes[randomEnemyIndex];
    }


    void FixedUpdate()
    {
        if (References.alarmManager.AlarmHasSounded() && enemiesToSpawn > 0)
        {
            secondsSinceLastSpawn += Time.fixedDeltaTime;
            if (secondsSinceLastSpawn >= secondsBetweenSpawns)
            {
                References.alarmManager.enemiesHaveSpawned = true;
                Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                secondsSinceLastSpawn = 0;
                enemiesToSpawn--;
            }
        }
    }
}
