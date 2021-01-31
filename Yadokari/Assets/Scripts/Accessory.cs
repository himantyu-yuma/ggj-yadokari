using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Accessory : MonoBehaviour, IScoreble
{
    [SerializeField]
    private int score;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
