using UnityEngine;
using System.Collections;

public class Fist : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void punch(){
		rigidbody2D.velocity = new Vector2 (-2,0);
	}
}
