using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShellLocations : MonoBehaviour
{
    /// <summary>
    /// 配置する貝
    /// </summary>
    [SerializeField]
    private GameObject[] shell = default;
    [SerializeField]
    private GameObject[] accessory = default;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rand_gen = new System.Random();
        List<Vector3> shellPos = Setup(rand_gen, 20, 0, 0, 36, -200, 200, -200, 200, 40);
        for (int i = 0; i < shellPos.Count/2; i++)
        {
            Instantiate(shell[UnityEngine.Random.Range(0, shell.Length)], shellPos[i] + Vector3.up*2, Quaternion.identity);
        }
        for (int i = shellPos.Count/2; i < shellPos.Count; i++)
        {
            Instantiate(accessory[UnityEngine.Random.Range(0, accessory.Length)], shellPos[i] + Vector3.up*2, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    const int MAX_FAILURE = 1000;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rand_generator"></param>
    /// <param name="number"></param>
    /// <param name="start_x"></param>
    /// <param name="start_y"></param>
    /// <param name="min_distance_start"></param>
    /// <param name="min_x"></param>
    /// <param name="max_x"></param>
    /// <param name="min_y"></param>
    /// <param name="max_y"></param>
    /// <param name="min_distance_shell"></param>
    /// <returns></returns>
    public static List<Vector3> Setup(System.Random rand_generator, int number, float start_x, float start_y, float min_distance_start, float min_x, float max_x, float min_y, float max_y, float min_distance_shell)
    {
        List<Vector3> shellLocations = new List<Vector3>();

        for (int round = 0; round < MAX_FAILURE; ++round)
        {
            Debug.LogError($"Round {round}\n");
            shellLocations.Clear();
            shellLocations.Capacity = number;

            int failure = 0;
            for (int i = 0; i < number; ++i)
            {
                Vector3 p = new Vector3();
                for (; ; )
                {
                    p.x = (float)(rand_generator.NextDouble() * (max_x - min_x) + min_x);
                    p.z = (float)(rand_generator.NextDouble() * (max_y - min_y) + min_y);

                    // すでに生成した点に近すぎる場合はやり直す
                    bool failed = false;
                    if (Vector3.Distance(p, new Vector3(start_x, start_y, 0)) < min_distance_start)
                    {
                        failed = true;
                    }
                    else
                    {
                        for (int j = 0; j < i; ++j)
                        {
                            if (Vector3.Distance(p, shellLocations[j]) < min_distance_shell)
                            {
                                failed = true;
                                break;
                            }
                        }
                    }
                    if (failed)
                    {
                        ++failure;
                        if (failure >= MAX_FAILURE) break;
                    }
                    else
                    {
                        // 近すぎない点が生成できたらループを抜ける
                        break;
                    }
                }
                if (failure >= MAX_FAILURE) break;
                shellLocations.Add(p);
            }
            if (failure < MAX_FAILURE) break;
        }

        if (shellLocations.Count() < number)
        {
            Debug.Log("適切な配置を生成できませんでした。");
            shellLocations.Clear();
        }
        return shellLocations;
    }
}
