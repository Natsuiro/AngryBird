using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redBirdCtl : MonoBehaviour
{
    public GameObject ancor;
    private SpringJoint2D sp;
    public float limitLength = 0.7f;
    private Rigidbody2D rg;
    private void OnMouseDrag()
    {
        DoDrag();
    }
    private void OnMouseDown()
    {
        rg.isKinematic = true;
    }
    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpringJoint2D>();
        sp.enabled = true;
    }

    private void DoDrag()
    {
        // DoDrag bug:
        // 错误的逻辑是通过if条件将 目标所在位置是否超出限制 分成了两种情况进行赋值，而结果就是
        // 这两个分支会在不同的两个帧中进行，实际上应该要在同一帧中完成，只需要一个判断，否则
        // 会因为两个帧的不同导致目标在不同位置快速闪烁
        // 正常的做法是先算出鼠标的位置并赋值，再判断是否合法，在同一帧中不会出现闪烁

        // 拖拽目标。使目标跟随鼠标拖拽移动，并消除Camera位置对目标position的影响
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition)
            - Camera.main.transform.position;
        transform.position = mousePos;
        if (Vector3.Distance(transform.position, ancor.transform.position) >= limitLength)
        {
            Vector3 dir = transform.position - ancor.transform.position;
            Vector3 limitPos = ancor.transform.position + dir.normalized * limitLength;
            transform.position = limitPos;
        }
    }


    private void OnMouseUp()
    {
        rg.isKinematic = false;
        Invoke("Fly",0.1f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void Fly()
    {
        sp.enabled = false;
    }
}
