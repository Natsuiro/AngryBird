using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 通过碰撞检测 控制目标物体的受伤以及死亡
 */
public class TargetCtl : MonoBehaviour
{

    private float maxVelocity = 8;
    private float minVelocity = 4;
    private SpriteRenderer mSr;
    public Sprite mHurtSprite;

    public GameObject mBoomEffect;
    public GameObject mScoreEffect;
    private void Awake()
    {

        mSr = GetComponent<SpriteRenderer>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        float relativeVelocity = collision.relativeVelocity.magnitude;
        Debug.Log(relativeVelocity);
        // 碰撞过程的相对速度,取标量
        // 达到最大速度 使本物体死亡
        if (relativeVelocity >= maxVelocity)
        {
            Dead();
        }
        else if(relativeVelocity > minVelocity)// 使本物体受伤
        {
            mSr.sprite = mHurtSprite;
        }
    }


    private void Dead()
    {

        Instantiate(mBoomEffect,transform.position,Quaternion.identity);
        Instantiate(mScoreEffect,transform.position,Quaternion.identity);
        GameManager.GetInstance().TarDie();
        Destroy(gameObject);
    }
}
