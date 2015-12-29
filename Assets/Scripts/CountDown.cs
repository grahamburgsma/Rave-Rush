using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

    public Text timerText;
    public int currentTime = 10;
    private AudioSource source;
    public AudioClip endRound_Sound;

    // Use this for initialization
    void Start()
    {
        setTime();
        InvokeRepeating("decreaseTimeLeft", 1.0f, 1.0f);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

  void decreaseTimeLeft()
    {
        currentTime--;

        if (currentTime <= 0)
        {
            currentTime = 0;
            setTime();
            source.PlayOneShot(endRound_Sound);
            CancelInvoke("decreaseTimeLeft");
        }

        setTime();

    }

    void setTime()
    {
        timerText.text = "Time Left :" + currentTime.ToString();
    }

}

