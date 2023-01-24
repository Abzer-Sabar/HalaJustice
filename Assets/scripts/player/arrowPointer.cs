using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class arrowPointer : MonoBehaviour
{
    public GameObject Target;
    public RectTransform pointerRectTransform;
    private Vector3 targetPos;
    [SerializeField]
    private Camera uiCamera;

    private void Start()
    {
        targetPos = new Vector3(Target.transform.position.y, Target.transform.position.y);
    }
    private void Update()
    {
        Vector3 toPosition = targetPos;
        Vector3 fromPosition = Camera.main.transform.position;

        fromPosition.z = 0f;
        //Vector3 dir = (toPosition - fromPosition).normalized;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 objScreenPos = Camera.main.WorldToScreenPoint(Target.transform.position);
        Vector3 dir = (objScreenPos - pointerRectTransform.position).normalized;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
        pointerRectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float border = 100f;
        Vector3 targetPosScreenPoint = Camera.main.WorldToScreenPoint(Target.transform.position);
        bool isOffScreen = targetPosScreenPoint.x <= border || targetPosScreenPoint.x >= Screen.width - border || targetPosScreenPoint.y <= border || targetPosScreenPoint.y >= Screen.height - border;
       
       /* if (isOffScreen)
        {
            Vector3 cappedTarget = targetPosScreenPoint;
            if (cappedTarget.x <= border) cappedTarget.x = border;
            if (cappedTarget.x >= Screen.width - border) cappedTarget.x = Screen.width - border;
            if (cappedTarget.y <= border) cappedTarget.y = border;
            if (cappedTarget.y >= Screen.height - border) cappedTarget.y = Screen.height - border;

            Vector3 pointerWorldPos = uiCamera.ScreenToViewportPoint(cappedTarget);
            pointerRectTransform.position = pointerWorldPos;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            Vector3 pointerWorldPos = uiCamera.ScreenToViewportPoint(targetPosScreenPoint);
            pointerRectTransform.position = pointerWorldPos;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }*/

    }

}
