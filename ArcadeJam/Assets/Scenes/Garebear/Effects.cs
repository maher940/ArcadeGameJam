using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {

    public delegate void PlayerHit();
    public RainCameraController bloodRain;

    public Player player;
	// Use this for initialization
	void Start () {
        player.OnPlayerHitEffect = OnPlayerHit;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnPlayerHit()
    {
        bloodRain.Play();
    }

}
