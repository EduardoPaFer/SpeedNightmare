using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;

    static float timePassed;
    //float lastTime = 0;
    public GameObject canvas;
    private void Awake()
    {
        Object.DontDestroyOnLoad(this.gameObject);
        Object.DontDestroyOnLoad(canvas);
    }
    
    private void Start()
    {
        //timePassed = lastTime;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        int miliseconds = Mathf.FloorToInt(timePassed * 60 % 60);
        int seconds = Mathf.FloorToInt(timePassed % 60);
        int minutes = Mathf.FloorToInt(timePassed / 60);
        timer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);
        //lastTime = timePassed;
    }
}
