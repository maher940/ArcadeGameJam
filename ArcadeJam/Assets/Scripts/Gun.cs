 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Gun : MonoBehaviour {

    /// <summary>
    /// Ammo for the gun
    /// </summary>
    [SerializeField]
    protected int ammo;
    public int Ammo
    {
        get
        {
            return ammo;
        }
    }
    /// <summary>
    /// Muzzle location for the shot
    /// </summary>
    [SerializeField]
    protected Transform muzzle;
    /// <summary>
    /// How fast the gun can fire
    /// </summary>
    [SerializeField]
    protected float fireRate;
    /// <summary>
    /// Cooldown of the gun to be checked with the firerate
    /// </summary>
    [SerializeField]
    protected float cooldown;
    public float CoolDown
    {
        get
        {
            return cooldown;
        }
        set
        {
            cooldown = value;
        }
    }
    /// <summary>
    /// Max ammo
    /// </summary>
    [SerializeField]
    protected int maxAmmo;
    /// <summary>
    /// Damage of gun
    /// </summary>
    [SerializeField]
    protected int damageAmount;
    /// <summary>
    /// If the gun is equipped
    /// not using
    /// </summary>
    [SerializeField]
    protected bool equipped;
    public bool Equipped
    {
        get
        {
            return equipped;
        }
        set
        {
            equipped = value;
        }
    }
    /// <summary>
    /// If the gun is reloading
    /// </summary>
    [SerializeField]
    protected bool reloading;

    /// <summary>
    /// Where the right hand shoiuld be only pistol***
    /// </summary>
    public Transform rightHand;
    /// <summary>
    /// Where the left hand shoiuld be only pistol***
    /// </summary>
    public Transform leftHand;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// function for shooting
    /// </summary>
    protected virtual void Shot()
    {
        ammo--; 
    }

    /// <summary>
    /// funtiong for reloading
    /// </summary>
    public virtual void Reload()
    {
        
        ammo = maxAmmo;
        
    }
    /// <summary>
    /// function for firing
    /// </summary>
    public virtual void Fire()
    {
        if(reloading == true || ammo <= 0)
        {
            Debug.Log("Ammo");
            cooldown = 0;
            return;
        }
        if (cooldown <= 0)
        {
            Shot();
        }
        cooldown += Time.deltaTime;

        if (cooldown > fireRate)
        {
            cooldown = 0;
        }
    }
    /// <summary>
    /// function for damage
    /// </summary>
    /// <param name="targetHit"></param>
    /// <param name="amount"></param>
    protected virtual void Damage(Collider collider, GameObject targetHit, int amount)
    {
        AI ai = targetHit.transform.root.GetComponent<AI>();
        ai.Health -= amount;
        Debug.Log(ai.Health);
        if (ai.Health <= 0)
        {
            ai.Kill(collider.transform, muzzle.forward, 50.0f);
        }
    }
}
