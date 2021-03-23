using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNode : MonoBehaviour
{
    public enum SpawnerType { instantSpawn, doorSpawn }

    List<GameObject> enemies;
    [SerializeField] List<GameObject> EnemiesToSpawn;
    [SerializeField] SpawnerType spawnerType;
    [SerializeField] float spawnDelay = 1.0f;
    [SerializeField] GameObject target;
    int currentSpawnIndex;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        currentSpawnIndex = 0;
    }

    // FixedUpdate is called 60 times a second
    void FixedUpdate()
    {
        // check to see if an enemy is dead, if so remove from the list
        for(int i = 0; i < enemies.Count; i++)
        {
            // remove the dead enemies and reduce current index accordingly
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i);
                i--;
            }
        }

        //TODO: check the current status of enemies and track how long each has been in said status
        // then update the rest of the enemies if they should change behavior
    }

    // Spawns the enemies in the required space
    public void Activate()
    {
        switch(spawnerType)
        {
            case SpawnerType.instantSpawn:
                // Spawn all enemies right away
                for (int i = 0; i < EnemiesToSpawn.Count; i++)
                {
                    SpawnEnemy();
                }
                break;
            case SpawnerType.doorSpawn:
                // Spawn all enemies with a delay between each spawn
                for(int i = 0; i < EnemiesToSpawn.Count; i++)
                {
                    Invoke(nameof(SpawnEnemy), i * spawnDelay);
                }
            break;
        }

    }

    // Spawn an enemy then increase the current index
    private void SpawnEnemy()
    {
        if (currentSpawnIndex < EnemiesToSpawn.Count) // Don't throw an index out of bounds error
        {
            enemies.Add(GameObject.Instantiate(EnemiesToSpawn[currentSpawnIndex], gameObject.transform.position, gameObject.transform.rotation));
            if(target != null)
                enemies[currentSpawnIndex].GetComponent<EnemyBehavior>().
        }
        currentSpawnIndex++;
    }

    // Sets all managed enemies to alert
    public void SetAllToAlert()
    {
        //TODO: add code to change behavoir of enemies
    }

    // Returns the current amount of enemies alive
    public int GetTotalAlive()
    {
        return enemies.Count;
    }
}
