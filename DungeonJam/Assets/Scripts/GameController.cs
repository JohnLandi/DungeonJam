using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private static PlayerData playerData;

	public GameObject whiteFlag;

	//public AudioClip hitSound;
	public int player1HP;
	public int player2HP;

	public int player1Score;
	public int player2Score;

	public int player1Ammo;
	public int player2Ammo;

	private Text scoreText1;
	private Text scoreText2;
	private Slider healthBar1;
	private Slider healthBar2;
	private Text ammoText1;
	private Text ammoText2;
	private Image flag1;
	private Image flag2;

	public bool player1HasFlag = false;
	public bool player2HasFlag = false;

	public AudioClip Splat;
	public AudioClip Music;

	AudioSource sound;



	// Use this for initialization
	void Start () 
	{
		player1HP = 10;
		player2HP = 10;

		player1Ammo = 25;
		player2Ammo = 25;

		player1Respawn();
		player2Respawn();

		sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		#region player dies
		if(player1HP <= 0)
		{
			player1Respawn();
			player2Score += 10;
			UpdateHealthBar1();
			UpdateScoreText2();
			if(player1HasFlag == true)
			{
				HideFlagImage1();
				flagReturn();
				player1HasFlag = false;
			}
			player1HP = 10;
		}

		if(player2HP <= 0)
		{
			player2Respawn();
			player1Score += 10;
			UpdateScoreText1();
			UpdateHealthBar2();
			if(player2HasFlag == true)
			{
				HideFlagImage2();
				flagReturn();
				player2HasFlag = false;
			}
			player2HP = 10;
		}

		fell();

		#endregion

		#region player win conditions
		if(player1Score >= 50)
		{
			//Debug.Log("I should be changing to win screen!");
			Application.LoadLevel (2);
		}

		if(player2Score >= 50)
		{
			Application.LoadLevel (3);
		}
		#endregion




	
	}

	void FixedUpdate()
	{
		FindUIComponents();
	}

	#region methods
	public void player1HPDown()
	{
		player1HP -= 1;
		UpdateHealthBar1();
		sound.Play();
		Debug.Log("Audio");
	}

	public void player2HPDown()
	{
		player2HP -= 1;
		UpdateHealthBar2();
		sound.Play();
		Debug.Log("Audio");
	}

	public void player1Respawn()
	{
		GameObject.FindGameObjectWithTag("Player1").transform.position = GameObject.FindGameObjectWithTag("Player1Spawn").transform.position;
		if(player2HasFlag == true)
		{
			player2HasFlag = false;
			HideFlagImage2();
		}
		flagReturn();
	}

	public void player2Respawn()
	{
		GameObject.FindGameObjectWithTag("Player2").transform.position = GameObject.FindGameObjectWithTag("Player2Spawn").transform.position;
		if(player2HasFlag == true)
		{
			player2HasFlag = false;
			HideFlagImage2();
		}
		flagReturn();
	}

	public void player1ScoreUp()
	{
		player1Score += 2;
		UpdateScoreText1();
	}

	public void player2ScoreUp()
	{
		player2Score += 2;
		UpdateScoreText2();
	}

	public void fell()
	{
		if(GameObject.FindGameObjectWithTag("Player1").transform.position.y <= -60)
		{
			Debug.Log("I should respawn");
			player1Respawn();
		}

		if(GameObject.FindGameObjectWithTag("Player2").transform.position.y <= -60)
		{
			Debug.Log("I should respawn");
			player2Respawn();
		}
	}

	public void player1AmmoUp()
	{
		player1Ammo = 25;
		UpdateAmmoText1();
	}
	
	public void player2AmmoUp()
	{
		player2Ammo = 25;
		UpdateAmmoText2();
	}

	public void player1AmmoDown()
	{
		player1Ammo -= 1;
		UpdateAmmoText1();
	}
	
	public void player2AmmoDown()
	{
		player2Ammo -= 1;
		UpdateAmmoText2();
	}
	#endregion

	#region UI methods
	public void FindUIComponents() {

		if (flag1 == null) {
			flag1 = GameObject.Find("FlagIcon1").GetComponent<Image>();
		}
		if (flag2 == null) {
			flag2 = GameObject.Find("FlagIcon2").GetComponent<Image>();
		}
		if (scoreText1 == null) {
			scoreText1 = GameObject.Find("Player1Score").GetComponent<Text>();
		}
		if (healthBar1 == null) {
			healthBar1 = GameObject.Find("Player1HP").GetComponent<Slider>();
		}  
		if (scoreText2 == null) {
			scoreText2 = GameObject.Find("Player2Score").GetComponent<Text>();
		}
		if (healthBar2 == null) {
			healthBar2 = GameObject.Find("Player2HP").GetComponent<Slider>();
		}  
		if (ammoText1 == null) {
			ammoText1 = GameObject.Find("Player1Ammo").GetComponent<Text>();
		}
		if (ammoText2 == null) {
			ammoText2 = GameObject.Find("Player2Ammo").GetComponent<Text>();
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

	public void HideFlagImage2() {
		if (flag2) {
			flag2.enabled = false;
		}
	}
	
	public void ShowFlagImage2() {
		if (flag2) {
			flag2.enabled = true;
		}
	}

	public void UpdateScoreText1() {
		if (scoreText1) {
			string hud = "Score: " + player1Score;
					
			scoreText1.text = hud;
		}
	}
	
	public void UpdateHealthBar1() {
		if (healthBar1) {
			healthBar1.value = player1HP;
		}
	}

	public void UpdateScoreText2() {
		if (scoreText2) {
			string hud = "Score: " + player2Score;
			
			scoreText2.text = hud;
		}
	}
	
	public void UpdateHealthBar2() {
		if (healthBar2) {
			healthBar2.value = player2HP;
		}
	}

	public void UpdateAmmoText1() {
		if (ammoText1) {
			string hud = "Ammo: " + player1Ammo;
			
			ammoText1.text = hud;
		}
	}

	public void UpdateAmmoText2() {
		if (ammoText2) {
			string hud = "Ammo: " + player2Ammo;
			
			ammoText2.text = hud;
		}
	}
	#endregion

	#region Flag Methods
	public void flagGone()
	{
		if(player1HasFlag || player2HasFlag)
		{
			whiteFlag.GetComponent<SpriteRenderer>().enabled = false;
			whiteFlag.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	public void flagReturn()
	{
		whiteFlag.GetComponent<SpriteRenderer>().enabled = true;
		whiteFlag.GetComponent<BoxCollider2D>().enabled = true;
		Debug.Log("Flag returned");
	}



	#endregion
}
