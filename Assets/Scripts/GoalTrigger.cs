using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoalTrigger : MonoBehaviour
{
    //public Text goalScoredText;
    //public string colour;
    public int goalsScored;
    

    void Start()
    {
        goalsScored = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            goalsScored++;  
        }
      
    }

   /* void setGoalText()
    {
        //goalScoredText.text = "Goals Scored :" + goalsScored.ToString();
    }

    void setOtherObjectEnteredText(Collider other)
    {
        goalScoredText.text = "The " + other.gameObject.tag + " is not the ball!";
    }*/
}