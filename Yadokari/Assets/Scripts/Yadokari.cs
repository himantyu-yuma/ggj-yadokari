using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Yadokari : MonoBehaviour
{
    /// <summary>
    /// 移動スピード
    /// </summary>
    [SerializeField]
    private float moveSpeed = 1.0f;
    private Rigidbody yadokariRigid;

    private float inputVertical;
    private float inputHorizontal;

    /// <summary>
    /// カメラの向きを正面とする処理に必要なやつ
    /// </summary>
    private GameObject cameraObj;

    // Start is called before the first frame update
    void Start()
    {
        yadokariRigid = this.GetComponent<Rigidbody>();
        cameraObj = Camera.main.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
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

        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");

        Move(inputVertical, inputHorizontal);
    }

    private void Move(float vertical, float horizontal)
    {
        yadokariRigid.velocity = (this.transform.forward * vertical + this.transform.right * horizontal) * moveSpeed;
    }
}
