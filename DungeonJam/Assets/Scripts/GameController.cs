using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private static PlayerData playerData;

	public GameObject whiteFlag;

	//public AudioClip hitSound;
	public int playerHP;

	public int playerScore;

	public int playerAmmo;


	private Text scoreText;

	private Slider healthBar;

	private Text ammoText;

	/*
	public AudioClip Splat;
	public AudioClip Music;

	AudioSource sound;
*/


	// Use this for initialization
	void Start () 
	{
		playerHP = 10;
		//player2HP = 10;

		playerAmmo = 25;


		playerRespawn();


		//	sound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () 
	{
		#region player dies
		if(playerHP <= 0)
		{
			playerRespawn();
			//UpdateHealthBar1();

			playerHP = 10;
		}



		fell();

		#endregion


		//Application.LoadLevel (3);


	}

	void FixedUpdate()
	{
		//FindUIComponents();
	}

	#region methods
	public void playerHPDown()
	{
		playerHP -= 1;
		//UpdateHealthBar1();
		//sound.Play();
		//Debug.Log("Audio");
	}



	public void playerRespawn()
	{
		GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;

	}



	public void playerScoreUp()
	{
		playerScore += 2;
		//UpdateScoreText1();
	}



	public void fell()
	{
		if(GameObject.FindGameObjectWithTag("Player").transform.position.y <= -60)
		{
			Debug.Log("I should respawn");
			playerRespawn();
		}


	}

	public void player1AmmoUp()
	{
		playerAmmo = 25;
		//UpdateAmmoText1();
	}



	public void player1AmmoDown()
	{
		playerAmmo -= 1;
		//UpdateAmmoText1();
	}


	#endregion


	/*
	#region UI methods
	public void FindUIComponents() {

		if (flag1 == null) {
			flag1 = GameObject.Find("FlagIcon1").GetComponent<Image>();
		}

		if (scoreText == null) {
			scoreText = GameObject.Find("Player1Score").GetComponent<Text>();
		}
		if (healthBar == null) {
			healthBar = GameObject.Find("Player1HP").GetComponent<Slider>();
		}   
		if (ammoText == null) {
			ammoText = GameObject.Find("Player1Ammo").GetComponent<Text>();
		}

	}

	public void HideFlagImage1() {
		if (flag1) {
			flag1.enabled = false;
		}
	}

	public void ShowFlagImage1() {
	if (flag1) {
		flag1.enabled = true;
	}
}



public void UpdateScoreText1() {
	if (scoreText) {
		string hud = "Score: " + playerScore;

		scoreText.text = hud;
	}
}

public void UpdateHealthBar1() {
	if (healthBar) {
		healthBar.value = playerHP;
	}
}


public void UpdateAmmoText1() {
	if (ammoText) {
		string hud = "Ammo: " + playerAmmo;

		ammoText.text = hud;
	}
}

#endregion

	*/
}
