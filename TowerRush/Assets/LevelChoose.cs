using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChoose : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level1");
    }
    public GameObject
        Menu,
        LevelSelecter;

    public void Return()
    {
        MenuUI.Status = 0;
        LevelSelecter.SetActive(false);
        Menu.SetActive(true);
    }
}
