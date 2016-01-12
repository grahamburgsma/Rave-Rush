using UnityEngine;
using System.Collections;

public class DiscoBallHandler : MonoBehaviour {

	[SerializeField] GameObject discoball;
	private Transform discoballTransform;


	// Use this for initialization
	void Start() {
		discoballTransform = discoball.GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update() {

		foreach (Transform child in discoballTransform) {   //rotate every child not just part object
			child.Rotate(0, 10f * Time.deltaTime, 0, Space.Self);
		}
        
        
      
	}
}
