using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMove : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 移動速度
    private float moveDirection;
    private Vector2 touchStartPos; // タッチの開始位置
    private Vector2 touchEndPos;   // タッチの終了位置
    private bool isMoving = false; // オブジェクトが移動中かどうか

    void Update()
    {
        // プラットフォームによってタッチまたはマウスの入力を取得
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Car")) // "Car" タグをキャラクターに設定していると仮定
                {
                    HandleSwipe();
                }
            }

            touchStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;
        }

        // オブジェクトが移動中であれば、前後に移動
        if (isMoving)
        {
            transform.Translate(Vector3.forward * moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    void HandleSwipe()
    {
        // スワイプの方向を計算
        Vector2 swipeDirection = touchEndPos - touchStartPos;

        // スワイプの距離が一定以上かつオブジェクトが移動中でない場合、オブジェクトを移動させる
        if (swipeDirection.magnitude > 50f && !isMoving)
        {
            // 縦方向のスワイプの場合、前後に移動させる
            moveDirection = Mathf.Sign(swipeDirection.y);
            isMoving = true;
        }
    }
}
