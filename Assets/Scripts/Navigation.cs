using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour {

	[SerializeField]
	Transform target;

	[SerializeField]
	NavMeshAgent agent;

	[SerializeField]
	float updateDelay=.3f;

	void Reset () {
		agent = GetComponent<NavMeshAgent> ();
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating ("FollowTarget", 0f, updateDelay); 
	}
	
	// Update is called once per frame
	void FollowTarget () {
		agent.SetDestination (target.position);
	}
}
