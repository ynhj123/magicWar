using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playTransform;
    Vector3 offset;
    void Start()
    {
         offset = transform.position - playTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //位置保持不变
        this.transform.position = playTransform.position + offset;
    }
}
