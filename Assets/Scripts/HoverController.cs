using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HoverController : MonoBehaviour
{
    private static HoverController instance;
    // Settings
    public bool interpolate = true;
    public bool lookAt = true;
    [Range(0, 360)]
    public float panAngle = 0;
    [Range(0, 180)]
    public float tiltAngle = 180.0f;
    [Range(10, 500)]
    public float distance = 100.0f;
    [Range(1, 12)]
    public uint steps = 8;
    [Range(1, 12)]
    public float yFactor = 1;
    public GameObject target;
    public bool wrapPanAngle = false;
    public GameObject angleController; // Кастыль для LeanTween

    protected bool limitPanAngle = false;
    protected bool limitTiltAngle = false;

    private float currentPanAngle = 0;
    private float currentTiltAngle = 90;
    private Vector3 tempPosition;
    private Vector3 targetPosition;
    private Transform targetTransform;

    public GameObject AngleController { get => angleController; }
    public static HoverController Instance { get => instance; set => instance = value; }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        tempPosition = Vector3.zero;
        currentPanAngle = panAngle;
        currentTiltAngle = tiltAngle;
        targetTransform = target.transform;

        if (angleController == null)
            return;
        Transform tr = angleController.transform;
        tr.Rotate(tiltAngle, panAngle, 0, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        isTween();
        Hover();
    }

    private void isTween()
    {
        if (angleController == null)
            return;
        Transform tr = angleController.transform;
        if (!tiltAngle.Equals(tr.rotation.x))
        {
            tiltAngle = tr.rotation.eulerAngles.x;
        }
        if (!panAngle.Equals(tr.rotation.y))
        {
            panAngle = tr.rotation.eulerAngles.y;
        }
    }

    private void Hover()
    {
        if (!tiltAngle.Equals(currentTiltAngle) || !panAngle.Equals(currentPanAngle) || !targetPosition.Equals(targetTransform.position))
        {
            if (wrapPanAngle)
            {
                if (panAngle < 0)
                {
                    currentPanAngle += panAngle % 360 + 360 - panAngle;
                    panAngle = panAngle % 360 + 360;
                }
                else
                {
                    currentPanAngle += panAngle % 360 - panAngle;
                    panAngle = panAngle % 360;
                }

                while (panAngle - currentPanAngle < -180)
                    currentPanAngle -= 360;

                while (panAngle - currentPanAngle > 180)
                    currentPanAngle += 360;
            }

            if (interpolate)
            {
                currentTiltAngle += (tiltAngle - currentTiltAngle) / (steps + 1);
                currentPanAngle += (panAngle - currentPanAngle) / (steps + 1);
                targetPosition += (targetTransform.position - targetPosition) / (steps + 1);
            }
            else
            {
                currentPanAngle = panAngle;
                currentTiltAngle = tiltAngle;
                targetPosition = targetTransform.position;
            }

            //snap coords if angle differences are close
            if ((Mathf.Abs(tiltAngle - currentTiltAngle) < 0.01) && (Mathf.Abs(panAngle - currentPanAngle) < 0.01))
            {
                currentTiltAngle = tiltAngle;
                currentPanAngle = panAngle;
            }

            tempPosition.x = targetPosition.x + distance * Mathf.Sin(currentPanAngle * Mathf.Deg2Rad) * Mathf.Cos(currentTiltAngle * Mathf.Deg2Rad);
            tempPosition.z = targetPosition.z + distance * Mathf.Cos(currentPanAngle * Mathf.Deg2Rad) * Mathf.Cos(currentTiltAngle * Mathf.Deg2Rad);
            tempPosition.y = targetPosition.y + distance * Mathf.Sin(currentTiltAngle * Mathf.Deg2Rad) * yFactor;
            this.transform.position = tempPosition;
            if (lookAt) this.transform.LookAt(targetPosition);
        }
    }
}