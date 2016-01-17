using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	public Transform maxMoveCheckUp;
	public Transform maxMoveCheckLeft;
	public Transform maxMoveCheckRight;

	public float moveSpeed;
	public float CheckRadius;

	public LayerMask sendUp;
	public LayerMask sendDown;
	public LayerMask sendLeft;
	public LayerMask sendRight;

	public bool moveUp;
	public bool moveRight;
	public bool moveLeft;
	public bool moveDown;

	// Use this for initialization
	void Start () 
	{
		if(moveUp)
			GetComponent<Rigidbody2D>().velocity = new Vector2(0 , moveSpeed);
		if(moveDown)
			GetComponent<Rigidbody2D>().velocity = new Vector2(0 , -moveSpeed);
		if(moveLeft)
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed , 0);
		if(moveRight)
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed , 0);
	}

	void FixedUpdate()
	{
		moveDown = Physics2D.OverlapCircle(maxMoveCheckUp.position , CheckRadius , sendDown);
		if(Physics2D.OverlapCircle(maxMoveCheckUp.position , CheckRadius , sendDown))
		{
			moveUp = false;
		}

		moveUp = Physics2D.OverlapCircle(maxMoveCheckUp.position , CheckRadius , sendUp);
		if(Physics2D.OverlapCircle(maxMoveCheckUp.position , CheckRadius , sendUp))
		{
			moveDown = false;
		}

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


	// Update is called once per frame
	void Update () 
	{
		if(moveUp)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0 , moveSpeed);
			//moveDown = false;
		}
		if(moveDown)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0 , -moveSpeed);
			//moveUp = false;
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
