using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllCamera : MonoBehaviour
{
    public GameObject target;
    public GameObject center;

    private float xDeg;
    private float yDeg;
    private Quaternion desiredRotation;
    private Quaternion currentRotation;
    private Quaternion rotation;

    private Vector3 fingerFirst;
    private Vector3 fingerSecond;
    private float newDistance = 0f;
    private float oldDistance = 0f;
    private float desiredDistance;
    private float scaleSpeed = 16;
    private float currentDistance;
    private Vector3 position;
    private Vector3 targetOffset;
    [HideInInspector]
    public Vector3 initPosTarget;
    [HideInInspector]
    public Quaternion initRotTarget;
    [HideInInspector]
    public Vector3 initPosCam;
    [HideInInspector]
    public Quaternion initRotCam;
    private float distance;

    //void Awake()
    //{
    //    //Init();
    //}


    //public void Init()
    //{

    //    distance = Vector3.Distance(transform.position, target.transform.position);
    //    currentDistance = distance;
    //    desiredDistance = distance;

    //    position = transform.position;
    //    rotation = transform.rotation;
    //    currentRotation = transform.rotation;
    //    desiredRotation = transform.rotation;
    //    if (transform.localEulerAngles.y > 0 && transform.localEulerAngles.y < 180)
    //    {
    //        xDeg = Vector3.Angle(Vector3.right, transform.right);
    //    }
    //    else
    //    {
    //        xDeg = -Vector3.Angle(Vector3.right, transform.right);
    //    }
    //    yDeg = Vector3.Angle(Vector3.up, transform.up);
    //}

    public void ResetCamera(Vector3 pso,Vector3 pasCam,Vector3 rotCam)
    {
        target.transform.position = pso;
        target.transform.eulerAngles = rotCam;
        transform.position = pasCam;
        transform.eulerAngles = rotCam;

        distance = Vector3.Distance(transform.position, target.transform.position);
        currentDistance = distance;
        desiredDistance = distance;

        position = transform.position;
        rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;
        if (transform.localEulerAngles.y > 0 && transform.localEulerAngles.y < 180)
        {
            xDeg = Vector3.Angle(Vector3.right, transform.right);
        }
        else
        {
            xDeg = -Vector3.Angle(Vector3.right, transform.right);
        }
        yDeg = Vector3.Angle(Vector3.up, transform.up);
    }

    //public void Reset()
    //{
    //    target.transform.position = Vector3.zero;
    //    target.transform.rotation = Quaternion.identity;

    //    transform.position = Vector3.zero;
    //    transform.rotation = Quaternion.identity;

    //    Init();
    //}
   
    void LateUpdate()
    {
        //单指旋转
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            CamRotate();
        }
        else if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            fingerFirst = Input.GetTouch(0).position;
            fingerSecond = Input.GetTouch(1).position;
            newDistance = Vector2.Distance(fingerFirst, fingerSecond);
            //双指平移
            if (newDistance - oldDistance < 7 && newDistance - oldDistance > -7)
            {
                CamMove();
            }
            //双指缩放
            else
            {
                CamScale();
            }
            oldDistance = newDistance;
        }
        //desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);

        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * 5);
        if (currentDistance ==0 )
        {
            return;
        }
        position = target.transform.position - (rotation * Vector3.forward * currentDistance + targetOffset);
        transform.position = position;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    float variableSpeed()
    {
        float disCameraToCenter = Vector3.Distance(center.transform.position, this.transform.position);
        float k = Mathf.Sqrt(disCameraToCenter) / 3f;
        return k;
    }

    void CamRotate()
    {
        xDeg += Input.GetAxis("Mouse X") * 3;
        yDeg -= Input.GetAxis("Mouse Y") * 3;

        //Clamp the vertical axis for the orbit
        yDeg = ClampAngle(yDeg, -80, 80);
        // set camera rotation 
        desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
        currentRotation = transform.rotation;

        rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * 4.5f);
        transform.rotation = rotation;
        target.transform.rotation = rotation;
    }

    void CamMove()
    {
        target.transform.rotation = transform.rotation;
        target.transform.Translate(Vector3.right * -Input.GetAxis("Mouse X") * 0.2f * variableSpeed());
        target.transform.Translate(transform.up * -Input.GetAxis("Mouse Y") * 0.2f * variableSpeed(), Space.World);
    }

    void CamScale()
    {
        if (newDistance > oldDistance)
        {
            desiredDistance -= Time.deltaTime * scaleSpeed * variableSpeed() * 1.5f;
        }
        if (newDistance < oldDistance)
        {
            desiredDistance += Time.deltaTime * scaleSpeed * variableSpeed() * 1.5f;
        }
    }
}
