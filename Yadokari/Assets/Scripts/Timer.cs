using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


public class Timer : MonoBehaviour
{
    /// <summary>
    /// 制限時間
    /// </summary>
    [SerializeField]
    private int timeLimit = 30;
    /// <summary>
    /// 現在の残り時間
    /// </summary>
    private float timeLeft = 0;

    [SerializeField]
    private TMP_Text timerText =default;

    [SerializeField]
    private UnityEvent timeUpEvent = new UnityEvent();

    // Start is called before the first frame update
    void Awake()
    {
        timeLeft = timeLimit;
        timerText.text = timeLeft.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = ((int)timeLeft).ToString();
        if (timeLeft < 0)
        {
            timerText.text = "0";
            this.enabled = false;
            timeUpEvent.Invoke();
        }
    }
}
