using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameController gameController;


	public float moveSpeed;
	public float jumpHeight;

	#region grounded variables
	//transform is a point in space
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;


	private int doubleJumped1;
	//private int doubleJumped2;
	#endregion

	public Transform playerShootPositionRight;
	public Transform playerShootPositionLeft;

	public GameObject bulletRight;
	public GameObject bulletLeft;


	//usually where you want to do physics stuff in unity
	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position , groundCheckRadius , whatIsGround);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Player1Controls();

		Player2Controls();
	}

	#region player collisions

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.transform.tag == "movingPlat")
		{
			transform.parent = other.transform;
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if(other.transform.tag == "movingPlat")
		{
			transform.parent = null;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if((other.gameObject.tag == ("LeftBullet") || other.gameObject.tag == ("RightBullet")) && gameObject.tag == ("Player1"))
		{
			gameController.player1HPDown();
		}

		if((other.gameObject.tag == ("LeftBullet") || other.gameObject.tag == ("RightBullet")) && gameObject.tag == ("Player2"))
		{
			gameController.player2HPDown();
		}

		if(other.gameObject.tag == ("Ammo") && gameObject.tag == ("Player1"))
		{
			gameController.player1AmmoUp();
		}

		if(other.gameObject.tag == ("Ammo") && gameObject.tag == ("Player2"))
		{
			gameController.player2AmmoUp();
		}

		if(other.gameObject.tag == ("Coin") && gameObject.tag == ("Player1"))
		{
			gameController.player1ScoreUp();
		}

		if(other.gameObject.tag == ("Coin") && gameObject.tag == ("Player2"))
		{
			gameController.player2ScoreUp();
		}
		
		if(other.gameObject.tag == ("Flag") && gameObject.tag == ("Player2"))
		{
			gameController.ShowFlagImage2();
			gameController.player2HasFlag = true;
			gameController.flagGone();
		}

		if(other.gameObject.tag == ("Flag") && gameObject.tag == ("Player1"))
		{
			gameController.ShowFlagImage1();
			gameController.player1HasFlag = true;
			gameController.flagGone();
		}

		if(other.gameObject.tag == ("Hazzard") && gameObject.tag == ("Player1"))
		{
			gameController.player1HP = 0;
		}

		if(other.gameObject.tag == ("Hazzard") && gameObject.tag == ("Player2"))
		{
			gameController.player2HP = 0;
		}

		if(other.gameObject.tag == ("Player2Cap") && gameObject.tag == ("Player1") && gameController.player1HasFlag == true)
		{
			gameController.player1Score += 25;
			gameController.player1HasFlag = false;
			gameController.HideFlagImage1();
			gameController.flagReturn();
		}

		if(other.gameObject.tag == ("Player1Cap") && gameObject.tag == ("Player2") && gameController.player2HasFlag == true)
		{
			gameController.player2Score += 25;
			gameController.player2HasFlag = false;
			gameController.HideFlagImage2();
			gameController.flagReturn();
		}
	}

	#endregion

	#region player move and shoot and teleport

	public void jump()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x , jumpHeight);
	}

	public void moveLeft()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed , GetComponent<Rigidbody2D>().velocity.y);
	}

	public void moveRight()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed , GetComponent<Rigidbody2D>().velocity.y);
	}

	public void shootRight()
	{
		Instantiate(bulletRight, playerShootPositionRight.position, playerShootPositionRight.rotation);
	}

	public void shootLeft()
	{
		Instantiate(bulletLeft, playerShootPositionLeft.position, playerShootPositionLeft.rotation);
	}

	#endregion

	#region player 1&2 controls

	public void Player1Controls()
	{
		if(grounded && gameObject.tag == ("Player1"))
		{
			doubleJumped1 = 4;
		}
		
		if(Input.GetKeyDown(KeyCode.K) && grounded && gameObject.tag == ("Player1"))
		{
			//to make the character jump
			//Vector2 holds an x and a y value
			jump();
		}
		
		if(Input.GetKeyDown(KeyCode.K) && doubleJumped1 > 0 && !grounded && gameObject.tag == ("Player1"))
		{
			jump();
			doubleJumped1 -= 1;
		}
		
		if(Input.GetKey(KeyCode.D )&& gameObject.tag == ("Player1"))
		{
			//to make the character move
			//Vector2 holds an x and a y value
			moveRight();
		}
		
		if(Input.GetKey(KeyCode.A) && gameObject.tag == ("Player1"))
		{
			//to make the character move
			//Vector2 holds an x and a y value
			moveLeft();
		}

		if(Input.GetKeyDown(KeyCode.J) && gameObject.tag == ("Player1") && gameController.player1Ammo > 0)
		{
			shootLeft();
			gameController.player1AmmoDown();
		}

		if(Input.GetKeyDown(KeyCode.L) && gameObject.tag == ("Player1") && gameController.player1Ammo > 0)
		{
			shootRight();
			gameController.player1AmmoDown();
		}

		if(Input.GetKeyDown(KeyCode.I) && gameObject.tag == ("Player1"))
		{
			gameController.player1Respawn();
		}
	}

	public void Player2Controls()
	{
		if(grounded && gameObject.tag == ("Player2"))
		{
			doubleJumped1 = 4;
		}
		
		if((Input.GetKeyDown(KeyCode.Keypad2)) && grounded && gameObject.tag == ("Player2"))
		{
			//to make the character jump
			//Vector2 holds an x and a y value
			jump();
		}
		
		if((Input.GetKeyDown(KeyCode.Keypad2)) && doubleJumped1 > 0 && !grounded && gameObject.tag == ("Player2"))
		{
			jump();
			doubleJumped1 -= 1;
		}
		
		if(Input.GetKey(KeyCode.RightArrow) && gameObject.tag == ("Player2"))
		{
			//to make the character move
			//Vector2 holds an x and a y value
			moveRight();
		}
		
		if(Input.GetKey(KeyCode.LeftArrow) && gameObject.tag == ("Player2"))
		{
			//to make the character move
			//Vector2 holds an x and a y value
			moveLeft();
		}

		if(Input.GetKeyDown(KeyCode.Keypad1) && gameObject.tag == ("Player2") && gameController.player2Ammo > 0)
		{
			shootLeft();
			gameController.player2AmmoDown();
		}

		if(Input.GetKeyDown(KeyCode.Keypad3) && gameObject.tag == ("Player2") && gameController.player2Ammo > 0)
		{
			shootRight();
			gameController.player2AmmoDown();
		}

		if(Input.GetKeyDown(KeyCode.Keypad5) && gameObject.tag == ("Player2"))
		{
			gameController.player2Respawn();
		}
	}

	#endregion

}
