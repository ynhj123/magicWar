using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public List<GameObject> games;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
 

    IEnumerator Timer()
    {
        Vector3 tmp = games[0].transform.localScale * 0.1f;
       
        while (true)
        {
            yield return new WaitForSeconds(15f);
            if (games[0].transform.localScale.x > 8)
            {
                games[0].transform.localScale -= tmp;
            }
            
        }
    }
}
