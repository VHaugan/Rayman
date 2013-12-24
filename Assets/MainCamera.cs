﻿using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

		public float dampTime = 0.10f;
		private Vector3 velocity = Vector3.zero;
		public Transform target;
	
		// Update is called once per frame
		void Update ()
		{
				if (target) {
						Vector3 point = camera.WorldToViewportPoint (target.position);
						Vector3 delta = target.position - camera.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, point.z));
						Vector3 destination = transform.position + delta;
						transform.position = Vector3.SmoothDamp (transform.position, new Vector3 (destination.x, destination.y, destination.z), ref velocity, dampTime);
				}
		}
}