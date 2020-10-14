using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Transform myTransonfrom;
    List<Vector3> endPoints;
    float speed = 5;
    float angluarSpeed = 100;
    public double hp = 100;
   
    public bool isDebuff = false;
    // Start is called before the first frame update
    void Start()
    {
        myTransonfrom = GetComponent<Transform>();
        endPoints = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UpdateControl();
        }
        if (endPoints.Count > 0)
        {
            Vector3 v = endPoints[0] - myTransonfrom.position;
            var dot = Vector3.Dot(v, myTransonfrom.right);
            Vector3 next = v.normalized * speed * Time.deltaTime;
            float angle = Vector3.Angle(v, myTransonfrom.forward);
            if (Vector3.SqrMagnitude(v) > 1f)
            {
                float minAngle = Mathf.Min(angle, angluarSpeed * Time.deltaTime);
                //点乘
                if (angle > 1f)
                {
                    //transform.Rotate(Vector3.Cross(tank.forward, v.normalized), minAngle);
                    if (dot > 0)
                    {
                        myTransonfrom.Rotate(new Vector3(0, minAngle, 0));
                    }
                    else
                    {
                        myTransonfrom.Rotate(new Vector3(0, -minAngle, 0));
                    }
                }
                else
                {
                    myTransonfrom.LookAt(endPoints[0]);
                    myTransonfrom.position += next;
                }
            }
            else
            {
                endPoints.RemoveAt(0);
            }

        }
        JudgeMentDebuff();
    }
    void JudgeMentDebuff()
    {
        //float v = Vector3.Distance(transform.position, Vector3.zero);
        if (isDebuff)
        {
            hp -= Time.deltaTime;
        }
         

    }
    void UpdateControl()
    {

        

        //获取屏幕坐标
        Vector3 mousepostion = Input.mousePosition;
        //定义从屏幕
        Ray ray = Camera.main.ScreenPointToRay(mousepostion);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
           
            if (Input.GetKey(KeyCode.LeftShift))
            {
                AddEndPoint(hitInfo.point);
            }
            else
            {
                ReSetEndPoint(hitInfo.point);
            }
            //transform.LookAt(endPoint);
            //transform.Translate(movePoint * 0.1f);
        }

    }
    void AddEndPoint(Vector3 endPoint)
    {
        endPoint.y = myTransonfrom.position.y;
        endPoints.Add(endPoint);
    }
    void ReSetEndPoint(Vector3 endPoint)
    {
        endPoint.y = myTransonfrom.position.y;
        endPoints.Clear();
        endPoints.Add(endPoint);
    }

}
