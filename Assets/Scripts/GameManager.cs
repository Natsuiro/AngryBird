using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<redBirdCtl> birds;
    public List<TargetCtl> tars;
    private int tarCount;
    public GameObject woodP;
    private int cur;
    private static GameManager sInstance;
    public GameObject mWinBg;
    public GameObject mLoseBg;
    private void Awake()
    {
        sInstance = this;
        cur = -1;
        tarCount = tars.Count;
    }

    public static GameManager GetInstance()
    {
        return sInstance;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        NextBird();
    }
    private void Init()
    {
        for (int i = 0;i< birds.Count;i++)
        {
            birds[i].dragable = false;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextBird()
    {
        cur++;
        if (tarCount>0)
        {
            if (cur<birds.Count)
            {
                birds[cur].transform.position = woodP.transform.position;
                birds[cur].dragable = true;
                birds[cur].EnableSp(true);
            }
            else
            {
                Debug.Log("Lose");
                mLoseBg.SetActive(true);

            }
        }
        else
        {
            Debug.Log("Win");
            mWinBg.SetActive(true);
        }
    }

    public void TarDie()
    {
        tarCount--;
    }

    public void ShowStars()
    {

    }
}
