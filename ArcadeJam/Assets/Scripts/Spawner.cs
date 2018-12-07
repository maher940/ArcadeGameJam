using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private int spawnRate;
    [SerializeField]
    private float spawnWaitTime;
    [SerializeField]
    private GameObject[] zombies;
    [SerializeField]
    private float offsetAmount;
    [SerializeField]
    private float startDelay;

    private Vector3 spawnPos;
    private Vector3[] offsetDirections = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Spawn());

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            spawnPos = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Spawn()
    {
        bool shouldSpawn = true;
        yield return new WaitForSeconds(startDelay);
        while (shouldSpawn)
        {
            for (int i = 0; i < spawnRate; i++)
            {
                if (AIManager.instance.SpawnedCount >= AIManager.instance.MaxCount)
                {
                    break;
                }

                int spawnIndex = Random.Range(0, zombies.Length);
                int offsetIndex = Random.Range(0, offsetDirections.Length);

                AI ai = Instantiate(zombies[spawnIndex], spawnPos + offsetDirections[offsetIndex] * offsetAmount, Quaternion.identity).GetComponent<AI>();
                ai.Health = AIManager.instance.MaxHealth;

                AIManager.instance.SpawnedCount++;
            }

            yield return new WaitForSeconds(spawnWaitTime);
        }
    }
}
