using UnityEngine;
using System.Collections;

public class bobScript : MonoBehaviour
{
		private float x = 0;
		public float maxHeight;
		public float speed;
		private float initHeight;
		// Use this for initialization
		void Start ()
		{
				initHeight = transform.position.y;
		}
	
		// Update is called once per frame
		void Update ()
		{
				x++;
				rigidbody2D.velocity = new Vector2 (0, Mathf.Sin (x / speed) / maxHeight);
		}
}