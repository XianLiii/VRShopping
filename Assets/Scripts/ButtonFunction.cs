using UnityEngine;
using System.Collections;

public class ButtonFunction : MonoBehaviour {


	//for button
	private Rigidbody rbThis;
	[SerializeField]
	private GameObject functionGameThings;

	//public AudioClip nature;
	public AudioSource source; 
	public AudioClip audioClip;

	//for game
	private Rigidbody player;
	private Transform playerOriginalTransform;
	private Vector3 playerOriginalPosition;

	private GameObject[] chips;
	private Vector3[] chipsOriginalPositions;

	private int chipClosestIndex=-1;

	public bool isGaming = false;
	private int chipClosestIndexLast=-1;

	//for animation
	private Animator anim;
	private Animation animMove;

	//for navigation
	[SerializeField]Transform target;
	[SerializeField]NavMeshAgent agent;
	[SerializeField]float updateDelay=.1f;

	// Use this for initialization
	void Start () {
		//find the player
		GameObject p = new GameObject ();
		p=GameObject.FindWithTag("Player");
		player = p.GetComponent<Rigidbody>();
		anim = player.GetComponent<Animator> ();
		animMove = player.GetComponent<Animation> ();


		rbThis = GetComponent<Rigidbody> ();
		player.gameObject.GetComponent<NavMeshAgent> ().enabled = false;
		Debug.Log ("set the navmeshagent to false:"+player.gameObject.GetComponent<NavMeshAgent> ().enabled);
	}


	//TRIGGER
	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("3D Button Function is activated");

		source.clip = audioClip;
		source.Play ();

		if (other.gameObject.CompareTag ("Hand"))
		{
			anim.enabled = !anim.enabled;
			//anim.SetBool ("IsRobotMove", !anim.GetBool("IsRobotMove"));

			if (isGaming) //functionGameThings.activeInHierarchy==true
			{
				
				gameOver ();
				functionGameThings.SetActive (false);

		player.constraints = 
			RigidbodyConstraints.FreezePosition|
			RigidbodyConstraints.FreezeRotationX|
			RigidbodyConstraints.FreezeRotationZ;
			
			}
			else if(!isGaming)//functionGameThings.activeInHierarchy==false
			{
				player.gameObject.GetComponent<NavMeshAgent> ().enabled = true;
				functionGameThings.SetActive(true);
				initial ();
				Debug.Log ("start the game");
			}
		}
	}


	//GAME－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－
	void initial()
	{
		isGaming = true;

		agent = player.GetComponent<NavMeshAgent> ();//set player as navigationAgent

		//keep the original position to reset the game
		playerOriginalTransform=player.transform;
		playerOriginalPosition = new Vector3 (0, 10f, 1f);
		Debug.Log ("player Original Transform ="+playerOriginalTransform.position);
		Debug.Log ("player Original  position="+playerOriginalPosition);

		//free the position constraints
		player.constraints=RigidbodyConstraints.None;
		Debug.Log ("RigidbodyConstraints.None");
		player.useGravity = true;

		//initialize all chips in an array
		if (chips == null)
			chips = GameObject.FindGameObjectsWithTag("Chip");
		Debug.Log ("chips length="+chips.Length);

		//store all the initial position to reset?
		/*
		for (int i = 0; i < chips.Length; i++) {
			chipsOriginalPositions [i] = chips [i].transform.position;
		}
		Debug.Log ("Store all chips positions");
		*/

		//start the moving animation


		//start game
		InvokeRepeating ("Game",0f,updateDelay);
	}

	// Update is called once per frame
	void Game () {

		//closest chip＝findClosestChip()｛if no, return null｝notice: return an int
		chipClosestIndex = findClosestChip ();

		if (!isGaming) {
			return;
		}
		
		//avoid refresh
		if (chipClosestIndex != chipClosestIndexLast&&isGaming==true) {
			//if there is a nearest chip
			if (chipClosestIndex >= 0) {
				Debug.Log ("find the closest" + chipClosestIndex);
				//－》set chip to target
				target = chips [chipClosestIndex].transform;
				agent.SetDestination (target.position);
				chipClosestIndexLast = chipClosestIndex;
				//－》clean it when collide(I don't know why the Ontriggerenter doesn't work. So I write a clean fucntion)
				InvokeRepeating ("CleanIt", 0f, updateDelay);
			} else {
				Debug.Log ("Go back");
				target = playerOriginalTransform;
				agent.SetDestination (target.position);
				chipClosestIndexLast = chipClosestIndex;
				//if there isn't one, do nothing
			}
		}

		//if user exit｜click the button again
		if (Input.GetKey (KeyCode.DownArrow))
			gameOver ();

	}

	void CleanIt()
	{
		if (chipClosestIndex>=0&&Vector3.Distance (agent.transform.position, target.transform.position)<1) {
			Debug.Log ("Clean="+chipClosestIndex);
			chips [chipClosestIndex].SetActive (false);
			Debug.Log ("Clean a chip!");
		}

		return;
	}

	int findClosestChip()
	{
		

		int index=-1;
		float distance=Vector3.Distance (chips [0].transform.position, player.transform.position);;

		for (int i = 1; i < chips.Length; i++) {
			//haven't been cleaned＋on the ground，calculate distance
			if (chips[i].activeInHierarchy && chips[i].transform.position.y < 1) {
				float distanceTemp = Vector3.Distance (chips [i].transform.position, player.transform.position);
				if (distanceTemp < distance) {
					index = i;
					distance = distanceTemp;
				}
			}
		}
		//Debug.Log ("find the index"+index);
		//return the index of the array
		return index;
	}
			


	void gameOver()
	{	  
		Debug.Log("End of the game");

		//end game
		isGaming=false;

		//reset all the chips
		/*
		Debug.Log("Reeset all chips");
		for (int i = 0; i < chips.Length; i++) {
			chips [i].SetActive (true);
			chips [i].transform.position = chipsOriginalPositions [i];
		}*/

		player.gameObject.GetComponent<NavMeshAgent> ().enabled = false;

		//contraint
		player.useGravity=false;

		//robot need to go back to the original postion (BUT NONE OF THEM WORKS! I take advantage of another bug-if enable the animator the robot can only be the riginial position and cannot be moved)
		//set target as originalposition
		Debug.Log ("player current Transform position="+player.transform.position);
		Debug.Log ("player Original Transform position="+playerOriginalTransform.position);
		Debug.Log ("player Original  position="+playerOriginalPosition);
		//player.transform.position = Vector3.MoveTowards (player.transform.position, playerOriginalPosition, 0.3f);
		player.MovePosition(playerOriginalPosition);
	

		return;

	}






}
