﻿using UnityEngine;
using System.Collections;

public class PlayerCroc : MonoBehaviour
{
		public AudioClip hitground;
		public AudioClip punch;
		public AudioClip death;
		public KeyCode moveUp;
		public KeyCode moveDown;
		public KeyCode moveLeft;
		public KeyCode moveRight;
		public KeyCode jumpKey;
		public KeyCode punchKey;
		private static int LEFT = -1;
		private static int RIGHT = 1;
		public static int dir = RIGHT;
		private bool holdingJump = false;
		private bool punching = false;
		private int jumpTime = 0;
		private float deathTime;
		private float speed = 5;
		private GameObject fist;
		private GameObject mainMusic;
		private Collider2D currentVine;
		private float fistCoolDownStart = 0;
		private float CLIMB_SPEED = 0.04f;
		private bool fistCoolDown = false;
		private int playerState = 1;

		private static int DEAD = 0;
		private static int ON_GROUND = 1;
		private static int JUMPING = 2;
		private static int FALLING = 3;
		private static int CLIMBING = 4;
		private static int HANGING = 6;

		private static float playerHeight = 2.0183f;
		private static float vineHeight = 5.24f;

		private void playerDie() {
			playerState = 0;
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
			mainMusic.audio.Stop ();
			AudioSource.PlayClipAtPoint (death, Camera.main.transform.position, 0.5f);
			deathTime = Time.time;
			
		}

		void Start ()
		{
				Physics2D.IgnoreLayerCollision (10, 8);
				fist = GameObject.FindGameObjectWithTag ("Fist");
				mainMusic = GameObject.FindGameObjectWithTag ("Muzak");
		}
	
		// Update is called once per frame

		void Update ()
		{
				if (Input.GetKey (jumpKey)) {
					if ((playerState == ON_GROUND || (playerState == CLIMBING && (Input.GetKey (moveLeft) || Input.GetKey (moveRight)))) && !holdingJump) {
								rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 6);
								playerState = 2;
				GameObject.FindGameObjectWithTag ("Berry").layer = 12;
						}
						if (playerState == JUMPING) {
								jumpTime++;
								rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 10);
								if (jumpTime == 20) {
										playerState = 3;
										jumpTime = 0;
								}	
						}
						holdingJump = true;
				} else if (playerState == JUMPING) {
						playerState = 3;
						jumpTime = 0;
				}

				if (Input.GetKey (moveUp)) {
					if (playerState == CLIMBING && (transform.position.y + playerHeight/2) < (currentVine.transform.position.y + vineHeight/2 + 0.1f)) {
						transform.position = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + CLIMB_SPEED);
						rigidbody2D.Sleep ();
					}
				}

				if (Input.GetKey (moveDown)) {
					if (playerState == CLIMBING && (transform.position.y - playerHeight/2) > (currentVine.transform.position.y - vineHeight/2 - 0.1f)) {
						transform.position = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y - CLIMB_SPEED);
						rigidbody2D.Sleep ();
					}
				}

				if (playerState == ON_GROUND || playerState == JUMPING || playerState == FALLING || playerState == CLIMBING) {
					if (Input.GetKey (moveRight)) {
							if (playerState != CLIMBING) rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
							if (dir == LEFT) {
									transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
									if (playerState == CLIMBING) rigidbody2D.Sleep ();
									fist.SendMessage ("Flip");
									dir = RIGHT;
								
							}
					} else if (Input.GetKey (moveLeft)) {
							if (playerState != CLIMBING) rigidbody2D.velocity = new Vector2 (speed * -1, rigidbody2D.velocity.y);
							if (dir == RIGHT) {
									transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
									if (playerState == CLIMBING) rigidbody2D.Sleep ();
									fist.SendMessage ("Flip");
									dir = LEFT;
							}
					} else {
						rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
					}
				}

				if (!Input.GetKey (jumpKey)) {
						holdingJump = false;
				}

				if (rigidbody2D.velocity.y < -10) {
						rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, -10);
				}

				if (playerState == ON_GROUND && rigidbody2D.velocity.y < 0) {
						playerState = 3;
				}

				if (Input.GetKey (punchKey) && !punching && !fistCoolDown) {
						fist.SendMessage ("punch");
						punching = true;
						AudioSource.PlayClipAtPoint (punch, Camera.main.transform.position, 0.5f);
				}
				if (!punching) {
						fist.transform.position = transform.position;
						if (fistCoolDown) {
								if (Time.time - fistCoolDownStart > 0.3) {
										fistCoolDown = false;
								}
						}
				}
				/*
				RaycastHit2D[] rayCast = Physics2D.RaycastAll (transform.position, new Vector2 (0, -1));
		if (GameObject.FindGameObjectWithTag ("Berry").layer == 8) {
						GameObject.FindGameObjectWithTag ("Berry").layer = 12;
				}
				foreach (RaycastHit2D hit in rayCast) {

			//{}
			if (hit.collider.gameObject.tag == "Ground"||hit.collider.gameObject.tag == "Berry") {
				if (rigidbody2D.velocity.y < 0) {
					if (hit.collider.gameObject.tag == "Berry")GameObject.FindGameObjectWithTag ("Berry").layer = 8;}
								if (hit.fraction < 1.5) {
										playerState = 1;
								} else {
										playerState = 3;
								}
								break;
						}
				}
				*/
				if (playerState == 0) {
					if ((Time.time - deathTime) > 6) {
						transform.position = new Vector2 (-21.05936f, -0.2536466f);
						mainMusic.audio.Play ();
						playerState = 2;
					}
				}
		}
		
		void OnCollisionEnter2D (Collision2D collision)
		{
				if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Berry") {
						playerState = 1;
						AudioSource.PlayClipAtPoint (hitground, Camera.main.transform.position, 0.5f);
				}
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.gameObject.tag == "Fist" && punching) {
						fistCoolDownStart = Time.time;
						fistCoolDown = true;
						punching = false;
						fist.SendMessage ("reset");
				}
				if (other.gameObject.tag == "OneWayPlatform") {
						if (rigidbody2D.velocity.y < 0) {
								GameObject.FindGameObjectWithTag ("Berry").layer = 8;
						}
				}
				if (other.gameObject.tag == "Vine") {
					if (playerState == JUMPING || playerState == FALLING || playerState == CLIMBING) {
						playerState = CLIMBING;
						currentVine = other;
						transform.position = new Vector2 (other.transform.position.x, gameObject.transform.position.y);
						rigidbody2D.Sleep();
					}
				}
				if (other.gameObject.tag == "Death") {
					if (playerState > 0) playerDie();
				}
	}
}