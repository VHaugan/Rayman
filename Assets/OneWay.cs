using UnityEngine;
using System.Collections;

public class OneWay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.gameObject.tag == "Fist" && Fist.punching) {
			if (rigidbody2D.IsSleeping ()) {
				rigidbody2D.WakeUp ();
				renderer.sortingOrder = 1;
			}
			else {
				rigidbody2D.velocity = new Vector2 (Mathf.Sign (collider.rigidbody2D.velocity.x) * 5, 0);
			}
		}
	}
}
