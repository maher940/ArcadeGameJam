using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun {

    [SerializeField]
    GameObject effects;
	// Use this for initialization
	void Start () {
        fireRate = 1;
        damageAmount = 10;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// Funciton for shooting
    /// </summary>
    protected override void Shot()
    {
        GameObject effect = Instantiate(effects, muzzle.transform.position, Quaternion.identity);
        base.Shot();
        RaycastHit hit;

        if(Physics.Raycast(muzzle.position, muzzle.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Damage(hit.collider, hit.transform.gameObject, damageAmount);
                Destroy(effect);
            }
            else
            {
                Debug.Log("Hur");
                Destroy(effect);
                return;
            }
        }
        else
        {
            Destroy(effect);
        }

        
        

    }

    public override void Fire()
    {
        base.Fire();

    }

    protected override void Damage(Collider collider, GameObject targetHit, int amount)
    {
        base.Damage(collider, targetHit, amount);

        //targetHit.GetComponent<Health>().ChangeHealth(amount);
    }

    public override void Reload()
    {
        base.Reload();
    }
}
