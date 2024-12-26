using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    public GameObject enemyPrefab;
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

    }


    void FixedUpdate()
    {
        if (References.alarmManager.AlarmHasSounded() && enemiesToSpawn > 0)
        {
            secondsSinceLastSpawn += Time.fixedDeltaTime;
            if (secondsSinceLastSpawn >= secondsBetweenSpawns)
            {
                Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                secondsSinceLastSpawn = 0;
                enemiesToSpawn--;
            }
        }
    }
}
