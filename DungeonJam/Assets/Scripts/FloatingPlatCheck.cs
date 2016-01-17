using UnityEngine;
using System.Collections;

public class FloatingPlatCheck : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == ("Ground"))
		{
			Debug.Log("Should work?");
			GameObject.FindGameObjectWithTag("FeetBox").GetComponent<CircleCollider2D>().enabled = false;
		}
	}

	/*void OnTriggerExit2D(Collision2D other)
	{
		if(other.gameObject.tag == ("Ground"))
		{
			Debug.Log("Shouldn't work?");
			GameObject.FindGameObjectWithTag("FeetBox").GetComponent<CircleCollider2D>().enabled = true;
		}
	}*/
}

