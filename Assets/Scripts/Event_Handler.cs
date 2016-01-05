using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Event_Handler : MonoBehaviour {

    [SerializeField]
    GameObject event_object;

    private Text event_text;

    public float size_count;

    private bool is_countdown, is_goal,is_endgame;
    private int counting_down = 3;
    private string go_text = "GO!!!!!!!!!";
    private string goal_text = "GGOOOOOAALLLLLLLL";
    private string game_over_text = "Game Over";

    Transform event_transform;

	// Use this for initialization
	void Start () {
        event_text = event_object.GetComponent<Text>();
        event_transform = event_text.GetComponent<Transform>();
       
    }

    //need this otherwise start is never called? confused
    void OnEnable()
    {
        Start();
    }



    // Update is called once per frame
    void Update () {
        if (is_countdown)
        {
            countdownUpdateCall();
        }

        if (is_goal)
        {
            goalUpdateCall();
        }
        

    }
    /*
    ****************************************************    GOAL RELATED **********************
    */
    public void startGoalDisplay()
    {
        initGoal();
        is_goal = true;
    }

    private void initGoal()
    {
        event_object.SetActive(true);
        event_transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        event_text.text = goal_text;
        size_count = 0;
      
        event_object.GetComponent<Text>().CrossFadeAlpha(0.0f, 2f, false);
    }

    private void goalUpdateCall()
    {
        if (size_count < 0.015f)
        {
            size_count += 0.0001f;
            event_transform.localScale += new Vector3(size_count, size_count, size_count);
        }
        else
        {
            event_object.SetActive(false);
            is_goal = false;


        }
    }

    /*
    ****************************************************    COUNTDOWN RELATED **********************
    */
    public void startCountdown()
    {
        event_object.SetActive(true);
        updateCountdown();
        is_countdown = true;
    }

    private void countdownUpdateCall() {

        if (size_count < 0.009f)
        {
            size_count += 0.0001f;
            event_transform.localScale -= new Vector3(size_count, size_count, size_count);
        }
        else
        {
            updateCountdown();


        }
    }

    private void updateCountdown()
    {
        
        if (counting_down > 0)
        {
            
            size_count = 0;
            event_transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            event_text.text = counting_down.ToString();
            counting_down--;
        }
        else if(!event_text.text.Equals(go_text))
        {
           
            event_text.text = go_text;
            size_count = 0;
            event_transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            event_object.SetActive(false);
            is_countdown = false;
        }
    }

    /*
   ****************************************************    GAME OVER RELATED **********************
   */

    public void startEndGame()
    {

        initEndGame();
        is_endgame = true;
    }

    private void initEndGame()
    {
        event_object.SetActive(true);
        event_transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        event_text.text = game_over_text;
        size_count = 0;
    }

    private void endGameUpdateCall()
    {
        if (size_count < 0.015f)
        {
            size_count += 0.0001f;
            event_transform.localScale += new Vector3(size_count, size_count, size_count);
        }
        else
        {
            event_object.SetActive(false);
            is_endgame = false;


        }
    }

    /*
  ****************************************************    Random Event **********************
  */
}


