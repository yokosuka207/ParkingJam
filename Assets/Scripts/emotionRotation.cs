using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class emotionRotation : MonoBehaviour
{
    [SerializeField] private Transform targetObject; // 3D�I�u�W�F�N�g��Transform
    public RawImage imageToMove; // 2D�摜

    goalRotate goal;

    private void Start()
    {
        goal = GameObject.Find("Goal").GetComponent<goalRotate>();
    }

    void Update()
    {
        Debug.Log(imageToMove);

        if (targetObject != null && imageToMove != null)
        {
            // 3D�I�u�W�F�N�g�̈ʒu���擾���A�����2D�摜�̈ʒu�ɔ��f
            if (goal.GetRotating())
            {
                imageToMove.enabled = true;
                Vector3 worldPosition = targetObject.position;
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
                screenPosition.y += 10f;
                imageToMove.transform.position = screenPosition;
            }
            else
            {
                imageToMove.enabled = false;
            }
        }
    }
}
