using UnityEngine;
using System.Collections;

public class Fist : MonoBehaviour
{

		private int punchTime = 0;
		private bool punching = false;
		private int punchLimit = 20;
		GameObject player;

		// Use this for initialization
		void Start ()
		{
				player = GameObject.FindGameObjectWithTag ("Player");
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (punching && punchTime != punchLimit) {
						punchTime++;
				}
				if (punchTime == punchLimit) {
						Vector3 delta = player.transform.position - transform.position;
						delta.Normalize ();
						transform.position = new Vector2 (transform.position.x + (0.3f * delta.x), transform.position.y + (0.3f * delta.y));
				}
		}

		void punch ()
		{
				punching = true;
				rigidbody2D.velocity = new Vector2 (-15, 0);
		}

		void reset ()
		{
				rigidbody2D.velocity = new Vector2 (0, 0);
				punchTime = 0;
				punching = false;
		}
}