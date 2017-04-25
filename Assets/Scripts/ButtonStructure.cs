using UnityEngine;
using System.Collections;

public class ButtonStructure : MonoBehaviour {


	private Animator anim;
	private Rigidbody player;
	// Use this for initialization

	void Start () {
		//find the player
		GameObject p = new GameObject ();
		p=GameObject.FindWithTag("Player");
		player = p.GetComponent<Rigidbody>();
		anim = player.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("3D Button Structure is activated");
		if (other.gameObject.CompareTag ("Hand")) 
		{
			if (anim.GetBool ("ButtonTriggered"))
				anim.SetBool ("ButtonTriggerOff", true);

			anim.SetBool ("ButtonTriggered", !anim.GetBool ("ButtonTriggered"));
		}
	}

}
