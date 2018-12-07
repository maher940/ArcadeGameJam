using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Grenade : MonoBehaviour {

    private Rigidbody rBody;
    public float timer;
    [SerializeField]
    private float explodeRadius;
    [SerializeField]
    private Collider[] colliders;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float power;
    [SerializeField]
    private float upMod;
    private Transform muzzle;
    public Transform Muzzle
    {
        get
        {
            return muzzle;
        }
        set
        {
            muzzle = value;
        }
    }
	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody>();
        rBody.AddForce(muzzle.transform.forward * 10, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if(timer > 3f)
        {
            Explode();
        }
	}
    /// <summary>
    /// Caues grenade to explode
    /// </summary>
    void Explode()
    {
        colliders = Physics.OverlapSphere(transform.position, explodeRadius, LayerMask.GetMask("Enemy"));
        List<Transform> alreadyHurt = new List<Transform>();

        for(int i = 0; i < colliders.Length; i++)
        {
           AI ai = colliders[i].transform.root.GetComponent<AI>();
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>();

           if (ai != null && !alreadyHurt.Contains(ai.transform))
            {
                //colliders[i].GetComponent<Health>().ChangeHealth(damage);
                ai.Health -= damage;
                if (ai.Health <= 0)
                {
                    ai.Kill(null, Vector3.zero, 0.0f);
                }
                alreadyHurt.Add(ai.transform);
            }

            if (ai.Health > 0)
            {
                continue;
            }

            if (rb != null)
            {
                rb.AddExplosionForce(power, transform.position, explodeRadius, upMod);
            }
        }
        StartCoroutine(WaitToDie());
    }

    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
