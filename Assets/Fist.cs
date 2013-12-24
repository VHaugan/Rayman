using UnityEngine;
using System.Collections;

public class Fist : MonoBehaviour
{

		private int punchTime = 0;
		private bool punching = false;
		private int punchLimit = 20;
		private GameObject player;

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
						rigidbody2D.velocity = new Vector2 (20 * delta.x, 20 * delta.y);
				}
		}

		void punch ()
		{
				punching = true;
				rigidbody2D.velocity = new Vector2 (PlayerCroc.dir * 15, 0);
		}

		void reset ()
		{
				rigidbody2D.velocity = new Vector2 (0, 0);
				punchTime = 0;
				punching = false;
		}
}