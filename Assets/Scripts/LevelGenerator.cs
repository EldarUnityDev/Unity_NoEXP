using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public List<GameObject> possibleChunkPrefabs;
    public List<GameObject> weaponPrefabs;
    public GameObject antiquePrefab;
    public GameObject guardPrefab;

    public string nextLevelName;
    public int alarmLevels;

    public float fractionOfPlinthsToHaveAntiques;
    public int numberOfGuardsToCreate;
    public int numberOfSpawnersToCreate;

    public int widthInChunks;
    public int lengthInChunks;

    private void Awake()
    {
        References.levelGenerator = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < lengthInChunks; j++)
        {
            for (int i = 0; i < widthInChunks; i++)
            {
                //get a random chunk type
                int randomChunkIndex = Random.Range(0, possibleChunkPrefabs.Count);
                GameObject randomChunkType = possibleChunkPrefabs[randomChunkIndex];
                Vector3 spawnPosition = transform.position + new Vector3(i * 15, 0, j * 25);
                Instantiate(randomChunkType, spawnPosition, Quaternion.identity);
            }
        }
        int numberOfThingsToPlace = References.plinths.Count;
        int NumberOfAntiquesToPlace = Mathf.RoundToInt(numberOfThingsToPlace * fractionOfPlinthsToHaveAntiques);


        foreach (PlinthBehaviour plinth in References.plinths)
        {

            GameObject thingToCreate;
            float chanceOfAntique = NumberOfAntiquesToPlace / numberOfThingsToPlace;

            if (Random.value < chanceOfAntique)
            {
                //place an antique
                thingToCreate = antiquePrefab;
                NumberOfAntiquesToPlace--;
            }
            else
            {
                //place a weapon
                //выбери случайную вещь, которую ещё не ставили
                int randomThingIndex = Random.Range(0, weaponPrefabs.Count);
                thingToCreate = weaponPrefabs[randomThingIndex];
            }
            numberOfThingsToPlace--;
            //создай одну из них
            GameObject newThing = Instantiate(thingToCreate);
            //закрепи за этой доской
            plinth.AssignItem(newThing);

        }

        List<NavPoint> possibleSpots = new List<NavPoint>();
        float minDistanceFromPlayer = 12;
        foreach (NavPoint nav in References.navPoints)
        {
            //is it far enough from the player?
            if(Vector3.Distance(nav.transform.position, References.thePlayer.transform.position) >= minDistanceFromPlayer)
            {
                possibleSpots.Add(nav);
            }
        }
        for(int  i = 0; i < numberOfGuardsToCreate; i++)
        {
            if(possibleSpots.Count == 0) { break; } //если больше нет мест, стоп
            int randomIndex = Random.Range(0, possibleSpots.Count);
            NavPoint spotToSpawnAt = possibleSpots[randomIndex];
            Instantiate(guardPrefab, spotToSpawnAt.transform.position, Quaternion.identity);
            possibleSpots.Remove(spotToSpawnAt);
        }

        while (References.spawners.Count > numberOfSpawnersToCreate)
        {
            int randomIndex = Random.Range(0, References.spawners.Count);
            Destroy(References.spawners[randomIndex].gameObject);
        }

        References.alarmManager.SetUpLevel(alarmLevels);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
