using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class Result : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのオブジェクト
    /// </summary>
    private GameObject player;

    [SerializeField]
    private GameObject resultItemText = default;
    [SerializeField]
    private Transform resultField = default;

    /// <summary>
    /// 合計点表示させるやつ
    /// </summary>
    [SerializeField]
    private TMP_Text sumScore = default;

    private int allScore;

    private IScoreble[] items;

    /// <summary>
    /// リザルトで操作できるカメラ
    /// </summary>
    [SerializeField]
    private CinemachineFreeLook resultCamera = default;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        player = GameObject.FindGameObjectWithTag("Player");

        resultCamera.LookAt = player.transform;
        resultCamera.Follow = player.transform;


        items = player.GetComponentsInChildren<IScoreble>();
        for (int i = 0; i < items.Length; i++)
        {
            GameObject resultItem = Instantiate(resultItemText, resultField) as GameObject;
            resultItem.transform.parent = resultField;
            resultItem.GetComponent<TMP_Text>().text = $"{items[i].ItemName}\t{items[i].Score}てん";
            allScore += items[i].Score;
        }

        Debug.Log(allScore);
        sumScore.text = $"合計\t{allScore}";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
