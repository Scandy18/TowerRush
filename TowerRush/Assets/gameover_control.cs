﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameover_control : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
