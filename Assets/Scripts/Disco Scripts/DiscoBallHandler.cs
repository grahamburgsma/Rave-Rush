using UnityEngine;
using System.Collections;

public class DiscoBallHandler : MonoBehaviour {

    [SerializeField]
    GameObject discoball;


    private Transform discoballTransform;
	// Use this for initialization
	void Start () {
        discoballTransform = discoball.GetComponent<Transform>();
    }

    // Update is called once per frame
    int i = 0;
	void Update () {

        foreach (Transform child in discoballTransform)
        {
            //if (child.rotation.y > 360.0f)
            //{
                //child.Rotate(0, 0, 0, Space.Self);
            //}
            child.Rotate(0, 10f * Time.deltaTime, 0, Space.Self);
        }
        
        
      
    }
}
