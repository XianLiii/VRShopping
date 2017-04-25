using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	public string targetSceneName;

	public AsyncOperation handle;

	public void onSwitchScene(string targetSceneName)
	{
		handle=SceneManager.LoadSceneAsync (targetSceneName, LoadSceneMode.Single);
		handle.allowSceneActivation = true;
		//handle.allowSceneActivation = false;
		//SceneManager.LoadScene(targetSceneName);
		Debug.Log("change the scene to: ");
		Debug.Log(targetSceneName);
	}
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
