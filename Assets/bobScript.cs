using UnityEngine;
using System.Collections;

public class bobScript : MonoBehaviour
{
		private float x = 0;
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
		transform.position = new Vector2 (transform.position.x, initHeight + Mathf.Sin (x/20)/2);
		}
}