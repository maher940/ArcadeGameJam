using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Gun {

    [SerializeField]
    private Grenade grenade;
    // Use this for initialization
    void Start()
    {
        fireRate = 1;
        damageAmount = -10;
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Instatiates a grenade to the scene
    /// </summary>
    protected override void Shot()
    {
        base.Shot();

        Grenade bullet = Instantiate(grenade, muzzle.transform.position, Quaternion.identity);
        bullet.Muzzle = muzzle;
       
    }

    public override void Fire()
    {
        base.Fire();

    }

    protected override void Damage(Collider collider, GameObject targetHit, int amount)
    {
        base.Damage(collider, targetHit, amount);

       // targetHit.GetComponent<Health>().ChangeHealth(amount);
    }

    public override void Reload()
    {
        base.Reload();
    }
}
