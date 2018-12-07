using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHand : MonoBehaviour
{
    public bool PlayerHit
    {
        get;
        set;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHit = false;
        }
    }
}
