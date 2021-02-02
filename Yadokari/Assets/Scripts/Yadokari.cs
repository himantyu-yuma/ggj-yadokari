using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Yadokari : MonoBehaviour
{
    /// <summary>
    /// 移動スピード
    /// </summary>
    [SerializeField]
    private float moveSpeed = 1.0f;
    private Rigidbody yadokariRigid;
    /// <summary>
    /// プレイヤーにかかる重力
    /// </summary>
    [SerializeField]
    private float gravity = 2.0f;

    /// <summary>
    /// ヤドカリのアニメーター
    /// </summary>
    private Animator animator;

    private float inputVertical;
    private float inputHorizontal;

    /// <summary>
    /// カメラの向きを正面とする処理に必要なやつ
    /// </summary>
    private GameObject cameraObj;

    private Transform kaigaraTransform;
    private Kaigara currentShell;

    /// <summary>
    /// レイを飛ばす距離
    /// </summary>
    private float rayDistance = 7.0f;
    /// <summary>
    /// レイとして飛ばすハコの大きさ
    /// </summary>
    private Vector3 castBox = new Vector3(2, 4, 2);

    /// <summary>
    /// 効果音鳴らすやつ
    /// </summary>
    private AudioSource yadokaraAudio;
    /// <summary>
    /// アイテム取得時のSE
    /// </summary>
    [SerializeField]
    private AudioClip itemSound = default;

    // Start is called before the first frame update
    void Start()
    {
        yadokariRigid = this.GetComponent<Rigidbody>();
        cameraObj = Camera.main.gameObject;

        animator = this.GetComponent<Animator>();

        kaigaraTransform = this.transform.Find("Kaigara_pos");
        currentShell = kaigaraTransform.GetComponentInChildren<Kaigara>();
        currentShell.Wear();

        yadokaraAudio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AdjustDirection();

        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");

        Move(inputVertical, inputHorizontal);

        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            //if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDistance))
            if (Physics.BoxCast(this.transform.position, castBox,transform.TransformDirection(Vector3.forward), out hit, this.transform.rotation, rayDistance))
            {
                if (hit.transform.TryGetComponent(out Kaigara kaigara))
                {
                    ChangeShell(kaigara);
                }
                if(hit.transform.TryGetComponent(out Accessory accesory)){
                    GetItem(accesory);
                }
            }
        }
    }

    /// <summary>
    /// プレイヤーの向きを変える
    /// </summary>
    private void AdjustDirection()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(cameraObj.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputVertical + cameraObj.transform.right * inputHorizontal;

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            this.transform.rotation = Quaternion.LookRotation(cameraForward);
        }
    }

    private void Move(float vertical, float horizontal)
    {
        yadokariRigid.velocity = (this.transform.forward * vertical + this.transform.right * horizontal) * moveSpeed + -Vector3.up * gravity;
        animator.SetFloat("InputAxis", vertical + horizontal);
    }

    private void GetItem(Accessory targetAccesory)
    {
        Vector3[] vartices = currentShell.GetComponent<MeshFilter>().mesh.vertices;
        Vector3[] normals = currentShell.GetComponent<MeshFilter>().mesh.normals;

        int randNum = Random.Range(0, vartices.Length);
        Vector3 targetVartix = vartices[randNum];
        Vector3 targetNormal = normals[randNum];
        //Debug.Log(targetVartix);
        //Debug.Log(targetNormal);

        targetAccesory.transform.parent = currentShell.transform;
        targetAccesory.transform.localPosition = targetVartix;
        // ちょっと押し出したい
        // TODO: 押し出し具合どうしようか
        targetAccesory.transform.Translate(targetNormal * 1.5f);
        //targetAccesory.transform.LookAt(targetNormal) ;
        targetAccesory.transform.rotation = Quaternion.LookRotation(this.transform.position - targetNormal);

        //int i = 0;
        //Debug.Log(currentShell.GetComponent<MeshFilter>().mesh.bounds.Contains(targetAccesory.GetComponent<Collider>().bounds.min));
        //while (currentShell.GetComponent<MeshFilter>().mesh.bounds.Intersects(targetAccesory.GetComponent<Collider>().bounds))
        //{
        //    i++;
        //    if (i > 100) break;

        //    targetAccesory.transform.Translate(targetNormal * 1.1f);
        //}

        targetAccesory.GetComponent<Rigidbody>().isKinematic = true;
        targetAccesory.GetComponent<Collider>().enabled = false;

        // 音鳴らす
        yadokaraAudio.PlayOneShot(itemSound);
    }

    private void ChangeShell(Kaigara targetShell)
    {
        currentShell.transform.parent = null;
        currentShell.transform.position = targetShell.transform.position;
        currentShell.Remove();

        targetShell.transform.parent = kaigaraTransform;
        targetShell.transform.localPosition = Vector3.zero;
        targetShell.transform.rotation = new Quaternion(0, 0, 0, 0);
        targetShell.Wear();

        currentShell = targetShell;

    }
}
