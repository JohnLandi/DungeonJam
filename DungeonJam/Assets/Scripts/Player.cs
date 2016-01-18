using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameController gameController;

	public int bulletCount;

	public float moveSpeed;
	public float jumpHeight;
	private float moveVelocity;

	#region grounded variables
	//transform is a point in space
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded = true;


	private int doubleJumped;
	//private int doubleJumped2;
	#endregion

	public Transform playerShootPosition;

	public GameObject mcBullet;

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



		if(other.gameObject.tag == ("Coin") && gameObject.tag == ("Player"))
		{
			gameController.playerScoreUp();
		}



		if(other.gameObject.tag == ("Hazzard") && gameObject.tag == ("Player1"))
		{
			gameController.playerHP = 0;
		}


	}

	#endregion

	#region player move and shoot and teleport

	public void jump()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x , jumpHeight);
	}

	public void shoot()
	{
		Instantiate(mcBullet, playerShootPosition.position, playerShootPosition.rotation);
		bulletCount++;
	}
		

	#endregion

	#region player controls

	public void PlayerControls()
	{
		if(grounded && gameObject.tag == ("Player"))
		{
			doubleJumped = 1;
		}
			
		//the next two lines of code are the ones that control the players movement left and right
		moveVelocity = moveSpeed * Input.GetAxisRaw("Horizontal");
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);

		if(Input.GetButtonDown("Jump") && grounded && gameObject.tag == ("Player"))
		{
			//to make the character jump
			//Vector2 holds an x and a y value
			jump();
		}

		if(Input.GetButtonDown("Jump") && doubleJumped > 0 && !grounded && gameObject.tag == ("Player"))
		{
			jump();
			doubleJumped -= 1;
		}

		if(Input.GetButtonDown("Fire1") && bulletCount < 3)
		{
			shoot();
			gameController.player1AmmoDown();
		}


			
		anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
		//anim.SetFloat("Falling", GetComponent<Rigidbody2D>().velocity.y);

		if(GetComponent<Rigidbody2D>().velocity.x > 0)
			transform.localScale = new Vector3(-1f, 1f, 1f);
		else if(GetComponent<Rigidbody2D>().velocity.x < 0)
			transform.localScale = new Vector3(1f, 1f, 1f);
	}

	#endregion

}
