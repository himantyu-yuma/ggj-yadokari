using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private Yadokari player = default;

    /// <summary>
    /// タイマー
    /// </summary>
    private Timer timer;

    /// <summary>
    /// タイムアップ時に出現するテキスト
    /// </summary>
    [SerializeField]
    private GameObject timeUpText = default;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        player.enabled = false;

        timer = this.GetComponent<Timer>();
        timer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    public void GameOver()
    {
        DontDestroyOnLoad(player);
        timeUpText.SetActive(true);
        StartCoroutine(ChangeScene.SceneChange(2, 2));
        player.enabled = false;
    }

    public void GameStart()
    {
        player.enabled = true;
        // タイマースタート
        timer.enabled = true;
    }
}
