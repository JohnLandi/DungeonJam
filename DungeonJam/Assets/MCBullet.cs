using UnityEngine;
using System.Collections;

public class MCBullet : MonoBehaviour {

	public float moveSpeed;
	float timeLeft = .75f;
	public Player player;

	// Use this for initialization
	void Start ()
	{
		player = FindObjectOfType<Player>();

		if(player.transform.localScale.x > 0)
			moveSpeed = -moveSpeed;
	}
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

		timeLeft -= Time.deltaTime;

		if(timeLeft < 0)
		{
			player.bulletCount--;
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		//Debug.Log("balllllls");

		Destroy(this.gameObject);
	}
}