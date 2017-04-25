using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ProductMode : MonoBehaviour {

	private Rigidbody rbThis;
	[SerializeField]
	private Camera userView;
	private AsyncOperation handle;
	[SerializeField]
	private GameObject productModeThings;
	[SerializeField]
	private GameObject shopModeThings;
	[SerializeField]
	private GameObject functionGameThings;

	// Use this for initialization
	void Start () {
		rbThis = GetComponent<Rigidbody> ();
		userView.cullingMask = 513;
		productModeThings.SetActive(false);
		Debug.Log ("Disactivate the product mode.");
		shopModeThings.SetActive(true);
		Debug.Log ("Activate the shop mode.");
	}


	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("OnTriggerEnter is called!");
	
		if (other.gameObject.CompareTag ("ProductModeArea"))
		{
			Debug.Log("Enter the ProductModeArea");
			//Debug.Log("The original culling mask is:");Debug.Log(userView.cullingMask);
			//userView.cullingMask = 257;
			productModeThings.SetActive(true);
			functionGameThings.SetActive (false);
			shopModeThings.SetActive(false);
			//Debug.Log("Now the culling mask is:");Debug.Log(userView.cullingMask);
			Debug.Log("change the scene to product mode");

		}

	}
	void OnTriggerExit(Collider other)
	{
		Debug.Log ("OnTriggerExit is called!");
		//userView.cullingMask = 513;
		productModeThings.SetActive(false);
		shopModeThings.SetActive(true);
	}
		
	
	// Update is called once per frame
	void Update () {
	
	}
}
