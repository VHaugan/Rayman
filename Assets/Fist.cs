using UnityEngine;
using System.Collections;

public class Fist : MonoBehaviour
{

		private int punchTime = 0;
		private bool punching = false;
		private int punchLimit = 20;
		private GameObject player;
		private float initScale;
		private int dir;

		// Use this for initialization
		void Start ()
		{
				player = GameObject.FindGameObjectWithTag ("Player");
				initScale = transform.localScale.x;
				dir = PlayerCroc.dir;
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
						if (dir != Mathf.Sign(delta.x)) {
								transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
								dir *= -1;
						}
						rigidbody2D.velocity = new Vector2 (25 * delta.x, 25 * delta.y);
				}
		}

		void punch ()
		{
				punching = true;
				rigidbody2D.velocity = new Vector2 (PlayerCroc.dir * 20, 0);
		dir = PlayerCroc.dir;
		}

		void reset ()
		{
				transform.localScale = new Vector2 (-PlayerCroc.dir * initScale, transform.localScale.y);
				rigidbody2D.velocity = new Vector2 (0, 0);
				punchTime = 0;
				punching = false;
		dir = PlayerCroc.dir;
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.gameObject.tag != "Player" && other.gameObject.tag != "Reverse") {
						punchTime = punchLimit;
			dir *= -1;
				}
		}

		void Flip ()
		{
				if (!punching) {
						transform.localScale = new Vector2 (PlayerCroc.dir * initScale, transform.localScale.y);
				}
		}
}