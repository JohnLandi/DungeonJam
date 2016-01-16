using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float moveSpeed;

	// Use this for initialization
	void Start ()
	{
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed , 0);
	}
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, .75f);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		//Debug.Log("balllllls");
		Destroy(this.gameObject);
	}
}
