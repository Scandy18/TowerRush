using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChoose : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("Demo");
    }

}
