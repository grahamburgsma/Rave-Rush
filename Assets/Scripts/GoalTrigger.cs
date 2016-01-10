using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoalTrigger : MonoBehaviour {
	public int goalsScored;
    public string Colour;
    [SerializeField]
    GoalHandler handler;

    [SerializeField]
    Light mainLight;

    private Quaternion initialRotation;
    private float range1, range2, range3, range4;

	void Start() {
		goalsScored = 0;
        if (handler.isDisco)
        {
            initialRotation = mainLight.GetComponent<Transform>().rotation;
            range1 = initialRotation.y;
            range2 = initialRotation.y + 90.0f;
            range3 = initialRotation.y + 180.0f;
            range4 = initialRotation.y + 270.0f;
            range1 = (range1 > 360.0f) ? range1 - 360f : range1;
            range2 = (range1 > 360.0f) ? range2 - 360f : range2;
            range3 = (range1 > 360.0f) ? range3 - 360f : range3;
            range4 = (range1 > 360.0f) ? range4 - 360f : range4;
        }
       
        

    }

    void Update()
    {
        
        if (handler.isDisco)
        {
            Quaternion newRotation = mainLight.GetComponent<Transform>().rotation;
            if (newRotation.y > range1 && newRotation.y < range2)
            {
                Colour = "green";
            }
            else if (newRotation.y > range2 && newRotation.y < range3)
            {
                Colour = "red";
            }
            else if (newRotation.y > range3 && newRotation.y < range4)
            {
                Colour = "blue";
            }
            else if (newRotation.y > range4 &&  newRotation.y < 360.0f)
            {
                Colour = "yellow";
            }
        }
    }

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Ball") {
			goalsScored++;  
		}
	}
}