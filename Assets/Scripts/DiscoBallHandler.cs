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

        discoballTransform.Rotate(0, 60f * Time.deltaTime, 0, Space.Self);
        i++;
        if(i >= 360)
        {
            i = 0;
        }
    }
}
