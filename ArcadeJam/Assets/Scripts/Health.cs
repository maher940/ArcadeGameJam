using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    private float health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeHealth(float amount)
    {
        health += amount;
        CheckHealth();
    }

    public void CheckHealth()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
