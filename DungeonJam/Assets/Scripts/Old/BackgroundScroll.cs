﻿using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {
	public float speed = .5f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2 (0, Time.time * speed);
		GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
