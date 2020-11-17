using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnim : MonoBehaviour
{

    private Vector3 mTargetPosition;
    private Vector3 mTargetScale;
    // Start is called before the first frame update
    void Start()
    {
        mTargetPosition = transform.position + new Vector3(0,0.5f,0);
        mTargetScale = new Vector3(1.2f,1.2f,1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,mTargetPosition,0.1f);
        transform.localScale = Vector3.Lerp(transform.localScale, mTargetScale, 0.1f);
        bool posFlag = Vector3.Distance(transform.position, mTargetPosition) < 0.01;
        if (posFlag)
        {
            AnimEnd();
        }
    }

    private void AnimEnd()
    {
        Destroy(gameObject,0.5f);
    }
}
