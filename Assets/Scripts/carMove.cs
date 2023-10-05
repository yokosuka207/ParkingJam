using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMove : MonoBehaviour
{
    public float moveSpeed = 5.0f; // �ړ����x
    private float moveDirection;
    private Vector2 touchStartPos; // �^�b�`�̊J�n�ʒu
    private Vector2 touchEndPos;   // �^�b�`�̏I���ʒu
    private bool isMoving = false; // �I�u�W�F�N�g���ړ������ǂ���

    void Update()
    {
        // �v���b�g�t�H�[���ɂ���ă^�b�`�܂��̓}�E�X�̓��͂��擾
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Car")) // "Car" �^�O���L�����N�^�[�ɐݒ肵�Ă���Ɖ���
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

        // �I�u�W�F�N�g���ړ����ł���΁A�O��Ɉړ�
        if (isMoving)
        {
            transform.Translate(Vector3.forward * moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    void HandleSwipe()
    {
        // �X���C�v�̕������v�Z
        Vector2 swipeDirection = touchEndPos - touchStartPos;

        // �X���C�v�̋��������ȏォ�I�u�W�F�N�g���ړ����łȂ��ꍇ�A�I�u�W�F�N�g���ړ�������
        if (swipeDirection.magnitude > 50f && !isMoving)
        {
            // �c�����̃X���C�v�̏ꍇ�A�O��Ɉړ�������
            moveDirection = Mathf.Sign(swipeDirection.y);
            isMoving = true;
        }
    }
}
