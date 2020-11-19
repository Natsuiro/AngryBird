using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redBirdCtl : MonoBehaviour
{
    public GameObject mRightAnchor;
    public GameObject mLeftAnchor;
    private SpringJoint2D sp;
    public float limitLength = 1f;
    private Rigidbody2D rg;
    public bool dragable;
    public LineRenderer mRightLr;
    public LineRenderer mLeftLr;
    public GameObject mBoomEffect;
    public float smooth = 3;


    private Vector3 mOriginPos;
    private bool flyable = false;
    public void EnableSp(bool enable)
    {
        sp.enabled = enabled;
        
    }

    public void Flyable()
    {
        flyable = true;
    }

    // 处理鼠标拖拽物体身上的碰撞器时的逻辑
    private void OnMouseDrag()
    {
        if (dragable)
        {
            DoDrag();
            Line();
        }
        
    }
    private void OnMouseDown()
    {
        if (dragable)
        {
            mRightLr.enabled = true;
            mLeftLr.enabled = true;
            rg.isKinematic = true;
        }
        
    }
    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpringJoint2D>();
        sp.enabled = false;
        flyable = false;
        dragable = false;
        mOriginPos = transform.position;
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
        if (Vector3.Distance(transform.position, mRightAnchor.transform.position) >= limitLength)
        {
            Vector3 dir = transform.position - mRightAnchor.transform.position;
            Vector3 limitPos = mRightAnchor.transform.position + dir.normalized * limitLength;
            transform.position = limitPos;
        }
    }


    private void OnMouseUp()
    {
        if (dragable)
        {
            rg.isKinematic = false;

            mRightLr.SetPosition(0,transform.position);
            mLeftLr.SetPosition(0,transform.position);
            mRightLr.enabled = false;
            mLeftLr.enabled = false;
            Invoke("Fly",0.1f);
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        if (flyable)
        {
            float pos = transform.position.x;
            float cY =  Camera.main.transform.position.y;
            float cZ =  Camera.main.transform.position.z;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
            new Vector3(Mathf.Clamp(pos,0,15),cY,cZ),smooth * Time.deltaTime);
        }
        
    }
    // 小鸟飞行的逻辑
    private void Fly()
    {
        sp.enabled = false;
        dragable = false;
        // 3s后自动销毁并通知GM切换下一只鸟
        Invoke("NextBird",3f);


    }

    private void NextBird()
    {
        flyable = false;
        Instantiate(mBoomEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);
        GameManager.GetInstance().NextBird();
    }
    // 绘制弹弓和小鸟之间的线
    private void Line()
    {
        mRightLr.SetPosition(0, mRightAnchor.transform.position);
        mRightLr.SetPosition(1, transform.position);
        mLeftLr.SetPosition(0, mLeftAnchor.transform.position);
        mLeftLr.SetPosition(1, transform.position);
    }
}
