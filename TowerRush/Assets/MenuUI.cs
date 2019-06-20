using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public static int Status;

    public GameObject
        Menu,
        LevelSelecter;

    public void SelectLevel()
    {
        Status = 1;
        LevelSelecter.SetActive(true);
        Menu.SetActive(false);
    }

    void Start()
    {
        if(Status==1)
        {
            SelectLevel();
        }
        else
        {
            LevelSelecter.SetActive(false);
            Menu.SetActive(true);
        }
    }

}
