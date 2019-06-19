using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI_controller : MonoBehaviour
{
    public GameObject settings;
    public void button_start()
    {
        Time.timeScale = 1;
    }

    public void button_pause()
    {
        Time.timeScale = 0;
    }
    public void button_accelerate()
    {
        Time.timeScale = 0.5F;
    }
    public void button_settings()
    {
        if (settings.activeSelf==false)
        {
            Time.timeScale = 0;
            settings.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            settings.SetActive(false);
        }

    }
    public void return_main_menu()
    {
        SceneManager.LoadScene("mainMenu");
    }
    public void restart()
    {
        SceneManager.LoadScene("Demo");
    }
}
