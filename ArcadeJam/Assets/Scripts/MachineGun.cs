using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Gun {


    [SerializeField]
    GameObject effects;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Function for shooting
    /// </summary>
    protected override void Shot()
    {
        GameObject effect = Instantiate(effects, muzzle.transform.position, Quaternion.identity);
        Debug.Log("Firing");
        base.Shot();
        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, muzzle.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Damage(hit.collider, hit.transform.gameObject, damageAmount);
            }
            else
            {
                return;
            }
        }
        Destroy(effect);
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
