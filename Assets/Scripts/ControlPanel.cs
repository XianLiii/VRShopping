using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlPanel : MonoBehaviour {

	public string targetSceneName;

	private Rigidbody rbThis;

	// Use this for initialization
	void Start () {
		rbThis = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("OnTriggerEnter is called!");

		if (other.gameObject.CompareTag ("ControlPanel"))
		{
			SceneManager.LoadScene(targetSceneName);
			Debug.Log("change the scene to: ");
			Debug.Log(targetSceneName);
		}
	}

}
