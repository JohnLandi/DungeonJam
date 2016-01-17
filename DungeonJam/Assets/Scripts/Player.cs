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


	private int doubleJumped;
	//private int doubleJumped2;
	#endregion

	public Transform playerShootPositionRight;
	public Transform playerShootPositionLeft;

	public GameObject bulletRight;
	public GameObject bulletLeft;

	private Animator anim;


	//usually where you want to do physics stuff in unity
	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position , groundCheckRadius , whatIsGround);
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () 
	{
		PlayerControls();

		anim.SetBool("Grounded", grounded);

		anim.SetInteger("Double", doubleJumped);

		//Player2Controls();
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
			gameController.playerHPDown();
		}

		if((other.gameObject.tag == ("LeftBullet") || other.gameObject.tag == ("RightBullet")) && gameObject.tag == ("Player2"))
		{
			gameController.playerHPDown();
		}

		/*if(other.gameObject.tag == ("Ammo") && gameObject.tag == ("Player"))
		{
			gameController.playerAmmoUp();
		}*/

		/*if(other.gameObject.tag == ("Ammo") && gameObject.tag == ("Player2"))
		{
			gameController.player2AmmoUp();
		}*/

		if(other.gameObject.tag == ("Coin") && gameObject.tag == ("Player"))
		{
			gameController.playerScoreUp();
		}

		/*if(other.gameObject.tag == ("Coin") && gameObject.tag == ("Player2"))
		{
			gameController.player2ScoreUp();
		}*/

		/*if(other.gameObject.tag == ("Flag") && gameObject.tag == ("Player2"))
		{
			gameController.ShowFlagImage2();
			gameController.player2HasFlag = true;
			gameController.flagGone();
		}*/

		/*if(other.gameObject.tag == ("Flag") && gameObject.tag == ("Player1"))
		{
			gameController.ShowFlagImage1();
			gameController.playerHasFlag = true;
			gameController.flagGone();
		}*/

		if(other.gameObject.tag == ("Hazzard") && gameObject.tag == ("Player1"))
		{
			gameController.playerHP = 0;
		}

		/*if(other.gameObject.tag == ("Hazzard") && gameObject.tag == ("Player2"))
		{
			gameController.player2HP = 0;
		}*/

		/*if(other.gameObject.tag == ("Player2Cap") && gameObject.tag == ("Player1") && gameController.player1HasFlag == true)
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
		}*/
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

	#region player controls

	public void PlayerControls()
	{
		if(grounded && gameObject.tag == ("Player"))
		{
			doubleJumped = 1;
		}

		if(Input.GetKeyDown(KeyCode.K) && grounded && gameObject.tag == ("Player"))
		{
			//to make the character jump
			//Vector2 holds an x and a y value
			jump();
		}

		if(Input.GetKeyDown(KeyCode.K) && doubleJumped > 0 && !grounded && gameObject.tag == ("Player"))
		{
			jump();
			doubleJumped -= 1;
		}

		if(Input.GetKey(KeyCode.D )&& gameObject.tag == ("Player"))
		{
			//to make the character move
			//Vector2 holds an x and a y value
			moveRight();
		}

		if(Input.GetKey(KeyCode.A) && gameObject.tag == ("Player"))
		{
			//to make the character move
			//Vector2 holds an x and a y value
			moveLeft();
		}

		if(Input.GetKeyDown(KeyCode.J) && gameObject.tag == ("Player") && gameController.playerAmmo > 0)
		{
			shootLeft();
			gameController.player1AmmoDown();
		}

		if(Input.GetKeyDown(KeyCode.L) && gameObject.tag == ("Player") && gameController.playerAmmo > 0)
		{
			shootRight();
			gameController.player1AmmoDown();
		}
			
		anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
		anim.SetFloat("Falling", GetComponent<Rigidbody2D>().velocity.y);

		if(GetComponent<Rigidbody2D>().velocity.x > 0)
			transform.localScale = new Vector3(-1f, 1f, 1f);
		else if(GetComponent<Rigidbody2D>().velocity.x < 0)
			transform.localScale = new Vector3(1f, 1f, 1f);
	}

	#endregion

}
