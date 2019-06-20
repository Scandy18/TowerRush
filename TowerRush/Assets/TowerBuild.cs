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
    public GameObject not_enough_money_ui;
    public GameObject TowerAttributes;
    public Camera main_camera;
    //UI
    public GameObject UI;

    private float timer=0;

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

    public void destroy()
    {
        towerBuild(TowerBehavior.Tower_ty.None);
    }

    public void cancel()
    {
        foreach (TowerBehavior temp in towerPits)
        {
            if (temp.is_selected == true)
            {
                temp.is_selected = false;
                return;
            }
        }
    }
    void Update()
    {
        if(timer>0)
            timer -= Time.deltaTime;
        if(timer<=0)
            not_enough_money_ui.SetActive(false);
        foreach (TowerBehavior temp in towerPits)
        {
            if (temp.is_selected)
            {
                if (!temp.is_built)
                {
                    build_ui.transform.localPosition = main_camera.WorldToScreenPoint(temp.transform.position) - new Vector3(Screen.width / 2, Screen.height / 2, 0) + new Vector3(-50, 200, 0);
                    build_ui.SetActive(true);
                }
                else
                {
                    TowerAttributes.transform.localPosition = main_camera.WorldToScreenPoint(temp.transform.position) - new Vector3(Screen.width / 2, Screen.height / 2, 0);// + new Vector3(-50, 200, 0);
                    TowerAttributes.SetActive(true);
                }
                return;
            }
        }
        build_ui.SetActive(false);
        TowerAttributes.SetActive(false);
    }
    void towerBuild(TowerBehavior.Tower_ty tower)
    {
        foreach (TowerBehavior temp in towerPits)
        {
            if (temp.is_selected == true)
            {
                if(UI.GetComponent<Attribute_UI_controller>().Money>= TowerBehavior.costs[(int)tower])
                {
                    temp.Set_Tower(tower);
                }
                else
                {
                    not_enough_money();
                }
                return;
            }
        }
    }
    void not_enough_money()
    {
        cancel();
        //显示金钱不足未实装
        not_enough_money_ui.SetActive(true);
        timer = 1;
    }
}
