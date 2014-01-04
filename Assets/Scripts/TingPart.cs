using UnityEngine;
using System.Collections;

public class TingPart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		particleSystem.renderer.sortingLayerName = "Particles";
		particleSystem.renderer.sortingOrder = 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
