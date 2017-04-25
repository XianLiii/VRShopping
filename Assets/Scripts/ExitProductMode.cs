using UnityEngine;
using System.Collections;

public class ExitProductMode : MonoBehaviour {

	[SerializeField]
	private GameObject productModeThings;
	[SerializeField]
	private GameObject shopModeThings;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.DownArrow)){  
			//Debug.Log("Exit the ProductModeArea");
			productModeThings.SetActive(false);
			shopModeThings.SetActive(true);
		} 
	
	}
}
