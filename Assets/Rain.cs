﻿using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnParticleCollision(GameObject other) {
		Destroy (this);
	}
}