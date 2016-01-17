using UnityEngine;
using System.Collections;

public class PatrollingEnemy : MonoBehaviour {
	public GameController gameController;

	public Transform maxMoveCheckUp;

	public float moveSpeed;
	public float CheckRadius;

	public LayerMask sendLeft;
	public LayerMask sendRight;

	public bool moveRight;
	public bool moveLeft;

	public int enemyHP;

	public GameObject enemyComponent;

	// Use this for initialization
	void Start () {
		if(moveLeft)
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed , 0);
		if(moveRight)
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed , 0);

		enemyHP = 15;
	}

	void FixedUpdate()
	{
		moveLeft = Physics2D.OverlapCircle(maxMoveCheckUp.position , CheckRadius , sendLeft);
		if(Physics2D.OverlapCircle(maxMoveCheckUp.position , CheckRadius , sendLeft))
		{
			moveRight = false;
		}
		
		moveRight = Physics2D.OverlapCircle(maxMoveCheckUp.position , CheckRadius , sendRight);
		if(Physics2D.OverlapCircle(maxMoveCheckUp.position , CheckRadius , sendRight))
		{
			moveLeft = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == ("LeftBullet") || other.gameObject.tag == ("RightBullet"))
		{
			enemyHP -=1;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == ("Player1"))
		{
			gameController.playerHP = 0;
		}
			
	}

	// Update is called once per frame
	void Update () {
	
		if(enemyHP <= 0)
		{
			Destroy(enemyComponent);
		}

		if(moveLeft)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed , 0);
			//moveRight = false;
		}
		if(moveRight)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed , 0);
			//moveLeft = false;
		}

	}
}
