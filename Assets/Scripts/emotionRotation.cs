using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class emotionRotation : MonoBehaviour
{
    [SerializeField] private Transform targetObject; // 3DオブジェクトのTransform
    public RawImage imageToMove; // 2D画像

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
            // 3Dオブジェクトの位置を取得し、それを2D画像の位置に反映
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
