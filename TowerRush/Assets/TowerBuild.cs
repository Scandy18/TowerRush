using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerBuild : MonoBehaviour
{
    public List<TowerBehavior> towerPits;
    Vector2 pos,aim_pos;
    public GameObject build_ui;
    public Camera main_camera;
    //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Camera.main.WorldToScreenPoint(aim_pos, canvas.worldCamera, out pos);
    public void tower_1()
    {
        towerBuild(TowerBehavior.Tower_ty.Arch);
    }
    public void tower_2()
    {
        towerBuild(TowerBehavior.Tower_ty.Stone);
    }
    public void tower_3()
    {
        towerBuild(TowerBehavior.Tower_ty.Ice);
    }
    public void tower_4()
    {
        towerBuild(TowerBehavior.Tower_ty.Magic);
    }
    public void cancel()
    {
        towerBuild(TowerBehavior.Tower_ty.None);
    }
    void Update()
    {
        foreach(TowerBehavior temp in towerPits)
        {
            if (temp.is_selected == true)
            {
                build_ui.transform.localPosition = main_camera.WorldToScreenPoint(temp.transform.position) - new Vector3(Screen.width / 2, Screen.height / 2, 0) +new Vector3(-50, 120, 0);
                build_ui.SetActive(true);
                return;
            }
        }
        build_ui.SetActive(false);
    }
    void towerBuild(TowerBehavior.Tower_ty tower)
    {
        foreach (TowerBehavior temp in towerPits)
        {
            if (temp.is_selected == true)
            {
                build_ui.SetActive(false);
                temp.Set_Tower(tower);
                return;
            }
        }
    }
}
