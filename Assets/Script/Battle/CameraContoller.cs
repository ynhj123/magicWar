using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playTransform;
    Vector3 offset;
    public float high;

    void Start()
    {
        if (playTransform == null)
        {
            return;
        }
        offset = transform.position - playTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(playTransform == null)
        {
            return ;
        }
        this.transform.position = playTransform.position + offset;

        //位置保持不变
    }
}
