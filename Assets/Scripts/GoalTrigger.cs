using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoalTrigger : MonoBehaviour
{
    public Text goalScoredText;
    public int goalsScored;
    public AudioClip goalScored_Sound;
    private AudioSource source;

    void Start()
    {
        goalsScored = 0;
        source = GetComponent<AudioSource>();
        setGoalText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            goalsScored++;
            setGoalText();
            source.PlayOneShot(goalScored_Sound);
        }
       /* else
        {
            setOtherObjectEnteredText(other);
        }*/
        
        
    }

    void setGoalText()
    {
        goalScoredText.text = "Goals Scored :" + goalsScored.ToString();
    }

    void setOtherObjectEnteredText(Collider other)
    {
        goalScoredText.text = "The " + other.gameObject.tag + " is not the ball!";
    }
}