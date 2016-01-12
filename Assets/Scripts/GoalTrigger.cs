using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/**
    Handles the goal triggers. For rave mode it determines what light is currently shining on it
*/
public class GoalTrigger : MonoBehaviour {
	public int goalsScored;
	public string Colour;
	[SerializeField] GoalHandler handler;

	[SerializeField]
	Light mainLight;

	private float initialRotation;
	private float range1, range2, range3, range4;
    public string first, second, third, fourth;

    //called when created
	void Start() {
		goalsScored = 0;
		if (handler.isRave) {
			initialRotation = mainLight.GetComponent<Transform>().rotation.eulerAngles.y;
            range1 = initialRotation;
			range2 = initialRotation + 90.0f;
			range3 = initialRotation + 180.0f;
			range4 = initialRotation + 270.0f;
           
            range1 = (range1 > 360.0f) ? range1 : range1 - 360.0f;
            range2 = (range1 > 360.0f) ? range2 : range2 - 360.0f;
            range3 = (range1 > 360.0f) ? range3 : range3 - 360.0f;
            range4 = (range1 > 360.0f) ? range4 : range4 - 360.0f;

            range1 = (range1 < 0f) ? range1 + 360.0f : range1;
            range2 = (range2 < 0f) ? range2 + 360.0f : range2;
            range3 = (range3 < 0f) ? range3 + 360.0f : range3;
            range4 = (range4 < 0f) ? range4 + 360.0f : range4;
            print("1 " + range1);
            print("2 " + range2);
            print("3 " + range3);
            print("4 " + range4);

        }
	}

    /**
        Called every frame to determine what light is currently shining on it
    */
	void Update() {
        
		if (handler.isRave) {
			float newRotation = mainLight.GetComponent<Transform>().rotation.eulerAngles.y;
                if (newRotation > range1 && newRotation < range2)
                {
                changeColour(1);
            }
                else if (newRotation > range2 && newRotation < range3)
                {
                changeColour(2);
            }
                else if (newRotation > range3 && newRotation < range4)
                {
                    changeColour(3);
                }
                else if (newRotation > range4 && newRotation < range1)
                {
                     changeColour(4);
                 }
            else
            {
                if (range1 > range2)
                {
                    if (newRotation > range1 || newRotation < range2)
                    {
                        changeColour(1);
                    }
                }
                else if (range2 > range3)
                {
                    if (newRotation > range2 || newRotation < range3)
                    {
                        changeColour(2);
                    }
                }
                else if (range3 > range4)
                {
                    if (newRotation > range3 || newRotation < range4)
                    {
                        changeColour(3);
                    }
                }
                else if (range4 > range1)
                {
                    if (newRotation > range4 || newRotation < range1)
                    {
                        changeColour(4);
                    }
                }
            }


            
		}
	}

    /**
        Handles when the ball makes it through the net
    */
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Ball") {
			goalsScored++;  
		}
	}
    /**
        Changes the colour of the side, this occurs when a new colour covers more than 50% of the net
    */
    private void changeColour(int numColour)
    {
        switch (numColour)
        {
            case 1:
                Colour = first;
                break;
            case 2:
                Colour = second;
                break;
            case 3:
                Colour = third;
                break;
            case 4:
                Colour = fourth;
                break;
        }
    }
}