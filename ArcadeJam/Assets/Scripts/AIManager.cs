using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager instance;

    [SerializeField]
    private int baseMaxCount;
    [SerializeField]
    private int countIncrement;
    [SerializeField]
    private int baseMaxHealth;
    [SerializeField]
    private int healthIncrement;
    [SerializeField]
    private float waitDuration;

    private int currentCount;
    private int killedCount;
    private bool countSet;

    public int KilledCount
    {
        get
        {
            return killedCount;
        }

        set
        {
            killedCount = value;

            if (killedCount >= MaxCount)
            {
                killedCount = 0;
                StartCoroutine(WaitToEndRound());
                
            }
            else if (killedCount <= 0)
            {
                killedCount = 0;
            }
        }
    }

    public int SpawnedCount
    {
        get
        {
            return currentCount;
        }
        set
        {
            currentCount = value;
            currentCount = Mathf.Clamp(currentCount, 0, MaxCount);
        }
    }

    public int MaxCount
    {
        get;
        private set;
    }

    public int MaxHealth
    {
        get;
        private set;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            MaxCount = baseMaxCount;
            MaxHealth = baseMaxHealth;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void IncrementStats()
    {
        MaxCount += countIncrement;
        MaxHealth += healthIncrement;
        SpawnedCount = 0;
    }

    IEnumerator WaitToEndRound()
    {
        yield return new WaitForSeconds(waitDuration);
        RoundManager.instance.NextRound();
        IncrementStats();
    }
}
