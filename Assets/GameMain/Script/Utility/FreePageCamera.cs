using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class FreePageCamera : MonoBehaviour
{
    public GameObject model;
    public GameObject target;
    public GameObject mCamera;
    public GameObject mCameraRawImage;

    public Vector3 fingerFirst;
    public Vector3 fingerSecond;

    public float newDistance;
    public float oldDistance;

    public Vector3 mRotate;
    public Vector3 camPos;

    public float xDeg;
    public float yDeg;

    public Quaternion goalRotation;
    public Quaternion currentRotation;
    public Quaternion rotation;

    public bool moved;
    public void resetCamera()
    {
        target.transform.localRotation = Quaternion.Euler(mRotate);
        target.transform.localPosition = Vector3.zero;
        mCamera.transform.localRotation = Quaternion.Euler(Vector3.zero);
        mCamera.transform.localPosition = camPos;
        mCameraRawImage.transform.localRotation = Quaternion.Euler(Vector3.zero);
        mCameraRawImage.transform.localPosition = mCamera.transform.localPosition;
    }
    public void resetProductCamera() {
        target.transform.localRotation = Quaternion.Euler(mRotate);
        target.transform.localPosition = new Vector3 (0,1,0);
        mCamera.transform.localRotation = Quaternion.Euler(Vector3.zero);
        mCamera.transform.localPosition = camPos;
        mCameraRawImage.transform.localRotation = Quaternion.Euler(Vector3.zero);
        mCameraRawImage.transform.localPosition = mCamera.transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE_WIN||UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            if (mCamera.activeInHierarchy)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                else
                {
                    camRot();
                }
            }
            else {
                camRot();
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else {
                camMove();
            }
        }
        camScale();
#elif UNITY_ANDROID || UNITY_IPHONE
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            if (mCamera.activeInHierarchy)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    return;
                }  
          else {
                camRot();
            }
            }
            else {
                camRot();
            }
        }
        else if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            fingerFirst = Input.GetTouch(0).position;
            fingerSecond = Input.GetTouch(1).position;
            newDistance = Vector2.Distance(fingerFirst, fingerSecond);
            if (newDistance - oldDistance < 7 && newDistance - oldDistance > -7)
            {
                camMove();
            }
            else
            {
                cameScalePhone();
            }
            oldDistance = newDistance;
        }

#endif
    }

    /// <summary>
    /// 相机平移
    /// </summary>
    void camMove()
    {
        moved = true;
        target.transform.Translate(Vector3.left * Input.GetAxis("Mouse X") * 0.1f * variableNum());
        target.transform.Translate(transform.up * -Input.GetAxis("Mouse Y") * 0.1f * variableNum(), Space.World);
    }

    /// <summary>
    /// 旋转
    /// </summary>
    void camRot()
    {
        moved = true;

        xDeg -= Input.GetAxis("Mouse Y") * 3;
        yDeg -= Input.GetAxis("Mouse X") * 3;

        yDeg = angleLimit(yDeg, -80, 80);

        goalRotation = Quaternion.Euler(yDeg, xDeg, 90);
        currentRotation = transform.localRotation;
        rotation = Quaternion.Lerp(currentRotation, goalRotation, Time.deltaTime * 2f*variableNum());

        target.transform.rotation = rotation;

    }

    /// <summary>
    /// 缩放
    /// </summary>
    void camScale()
    {
        //moved = true;

        float distance = Vector3.Distance(mCamera.transform.position, model.transform.position);
        //distance += Input.GetAxis("Mouse ScrollWheel") * variableNum();
        mCamera.transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * variableNum());
        mCameraRawImage.transform.localPosition = mCamera.transform.localPosition;
    }
    void cameScalePhone()
    {
        moved = true;

        float disCamToModel = Vector3.Distance(model.transform.position, mCamera.transform.position);

        if (newDistance > oldDistance)
        {
            mCamera.transform.Translate(Vector3.forward * Time.deltaTime *12* variableNum());
            mCameraRawImage.transform.localPosition = mCamera.transform.localPosition;
        }
        if (newDistance < oldDistance)
        {
            mCamera.transform.Translate(-Vector3.forward * Time.deltaTime*12 * variableNum());
            mCameraRawImage.transform.localPosition = mCamera.transform.localPosition;
        }
    }

    /// <summary>
    /// 角度限制
    /// </summary>
    float angleLimit(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }

    /// <summary>
    /// 根据相机距离模型的距离返回一个系数 
    /// </summary>
    float variableNum()
    {
        float disCamToModel = Vector3.Distance(model.transform.position, mCamera.transform.position);
        float k = Mathf.Sqrt(disCamToModel) /1.2f;
        return k;
    }

}
