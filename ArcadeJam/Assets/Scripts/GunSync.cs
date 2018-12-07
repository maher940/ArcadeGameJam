using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSync : MonoBehaviour {

    [SerializeField]
    Animator anim;
    [SerializeField]
    Transform gunPivot;
    [SerializeField]
    Transform rightHandHold;
    [SerializeField]
    Transform leftHandHold;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        gunPivot.parent = Camera.main.transform;
	}


    
}
