﻿using UnityEngine;
using System.Collections;

public class PlayerCroc : MonoBehaviour
{
		public KeyCode moveUp;
		public KeyCode moveDown;
		public KeyCode moveLeft;
		public KeyCode moveRight;
		public KeyCode punchKey;
		private static int LEFT = -1;
		private static int RIGHT = 1;
		public static int dir = LEFT;
		private bool canJump = false;
		private bool onGround = false;
		private bool jumping = false;
		private bool punching = false;
		private int jumpTime = 0;
		private float speed = 5;
		private GameObject fist;
		private float fistCoolDownStart = 0;
		private bool fistCoolDown = false;

		void Start ()
		{
				Physics2D.IgnoreLayerCollision (10, 8);
				fist = GameObject.FindGameObjectWithTag ("Fist");
		}
	
		// Update is called once per frame

		void Update ()
		{
				if (Input.GetKey (moveUp)) {
						if (canJump) {
								rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 6);
								onGround = false;
								canJump = false;
								jumping = true;
						}
						if (jumping) {
								if (jumpTime == 20) {
										jumping = false;
										jumpTime = 0;
								}
								rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 10);
								jumpTime++;
						}
				} else {
						jumping = false;
						jumpTime = 0;
						
				}

				if (Input.GetKey (moveRight)) {

						rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
						if (dir == LEFT) {
								transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
								fist.SendMessage ("Flip");
								dir = RIGHT;
							
						}
				} else if (Input.GetKey (moveLeft)) {
						rigidbody2D.velocity = new Vector2 (speed * -1, rigidbody2D.velocity.y);
						if (dir == RIGHT) {
								fist.SendMessage ("Flip");
								transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
								dir = LEFT;
						}
				} else {
						rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
				}

				if (onGround && !Input.GetKey (moveUp)) {
						canJump = true;
				}
				if (rigidbody2D.velocity.y < -10) {
						rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, -10);
				}

				if (rigidbody2D.velocity.y < 0) {
						canJump = false;
						onGround = false;
				}

				if (Input.GetKey (punchKey) && !punching && !fistCoolDown) {
						fistCoolDownStart = Time.time;
						fist.SendMessage ("punch");
						punching = true;
				}
				if (!punching) {
						fist.transform.position = transform.position;
						if (fistCoolDown) {
								if (Time.time - fistCoolDownStart > 0.7) {
										fistCoolDown = false;
								}
						}
				}
		}
		
		void OnCollisionEnter2D (Collision2D collision)
		{
				if (collision.gameObject.tag == "Ground") {
						onGround = true;
				}
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.gameObject.tag == "Fist" && punching) {
						fistCoolDown = true;
						punching = false;
						fist.SendMessage ("reset");
				}
		}
}