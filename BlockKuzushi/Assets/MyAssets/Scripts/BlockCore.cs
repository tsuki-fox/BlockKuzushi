using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCore : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		MessageVisualizer.Write("HIT", transform.position);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
