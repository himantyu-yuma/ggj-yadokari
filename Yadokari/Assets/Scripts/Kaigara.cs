using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshCollider))]
public class Kaigara : MonoBehaviour, IScoreble
{
    private Rigidbody shellRigid;
    private MeshCollider shellCollider;
    [SerializeField]
    private int score;
    public int Score { get { return score; } set { score = value; } }
    [SerializeField]
    private string itemName;
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    private void Start()
    {
        shellRigid = this.GetComponent<Rigidbody>();
        shellCollider = this.GetComponent<MeshCollider>();
    }
    /// <summary>
    /// 貝殻を背負ったときに実行
    /// </summary>
    public void Wear()
    {
        shellRigid.isKinematic = true;
        shellCollider.enabled = false;
    }

    /// <summary>
    /// 貝殻を手放したときに実行
    /// </summary>
    public void Remove()
    {
        shellRigid.isKinematic = false;
        shellCollider.enabled = true;

        this.transform.parent = null;
    }
}
