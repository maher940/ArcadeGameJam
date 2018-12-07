using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour {

    public Animator anim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", Input.GetAxis("Vertical"));
        anim.SetFloat("Strafe", Input.GetAxis("Horizontal"));
    }
}
