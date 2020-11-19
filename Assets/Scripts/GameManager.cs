using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public List<redBirdCtl> birds;
    public List<TargetCtl> tars;
    public List<GameObject> stars;
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
                birds[cur].Flyable();
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
        StartCoroutine("DoShow");
    }

    IEnumerator DoShow()
    {
        int starCount = birds.Count - cur + 1;
        for (int i = 0; i < starCount; i++)
        {
            yield return new WaitForSeconds(0.3f);
            stars[i].SetActive(true);
        }
    }
    // 重试
    public void Retry()
    {
        SceneManager.LoadScene(2);
    }
    // 关卡选择
    public void LevelSelect()
    {
        SceneManager.LoadScene(1);
    }
    public void NextLevel()
    {

    }
}
