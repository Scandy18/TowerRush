using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuUI : MonoBehaviour
{

    public GameObject
        Menu,
        LevelSelecter;

    public void SelectLevel()
    {
        LevelSelecter.SetActive(true);
        Menu.SetActive(false);
    }

    void Start()
    {
        LevelSelecter.SetActive(false);
    }

}
