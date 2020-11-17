using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBgCtl : MonoBehaviour
{
    public void Show()
    {
        GameManager.GetInstance().ShowStars();
    }
    
}
