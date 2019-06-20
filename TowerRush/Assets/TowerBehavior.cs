using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBehavior : MonoBehaviour
{

    public enum Tower_ty { None, Arch, Stone, Ice, Magic }
    public static int[] costs= {0,60,80,100,120};
    public static int[] destroy_return = { 0, 30, 40, 50, 60 };
    private float timer;
    //区域属性
    public bool is_selected = false;
    public bool is_built = false;

    //塔属性
    private Transform transform;
    private Tower_ty ty;
    private int hp;
    private int attack;
    private float atk_time;
    private float atk_range;

    //塔模型
    public GameObject go_arch;
    public GameObject go_stone;
    public GameObject go_ice;
    public GameObject go_magic;

    //UI
    GameObject UI;

    //敌人列表
    public List<EnemyBehavior> Enemies;

    //渲染器
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false; //隐形

        transform = gameObject.GetComponent<Transform>();
        timer = 0;

        Set_Tower(Tower_ty.None);
        hp = 100;
        attack = 0;
        atk_range = 0;
        ty = Tower_ty.None;
        atk_time = Mathf.Infinity;
        
    }
    private void OnMouseUp()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("touch area is UI");
        }
        else
        {
            is_selected = !is_selected;
        }
        
    }

    void Update()
    {
        #region 选中
        if (is_selected)
            meshRenderer.enabled = true;
        else
            meshRenderer.enabled = false;
        #endregion

        //选中未建造区域可以建造塔
        //if (is_selected && !is_built)
        //{
        //    if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        //    {
        //        Set_Tower(Tower_ty.Arch);
        //        Debug.Log("Set to Arch Tower.");
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        //    {
        //        Set_Tower(Tower_ty.Stone);
        //        Debug.Log("Set to Stone Tower.");
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        //    {
        //        Set_Tower(Tower_ty.Ice);
        //        Debug.Log("Set to Ice Tower.");
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        //    {
        //        Set_Tower(Tower_ty.Magic);
        //        Debug.Log("Set to Magic Tower.");
        //    }
        //}
        ////选中已建造区域可以摧毁塔
        //if(is_selected && is_built)
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        //    {
        //        Set_Tower(Tower_ty.None);
        //        Debug.Log("Destroy the Tower.");
        //    }
        //}

        #region 攻击逻辑
        
        timer += Time.deltaTime;
        float nearestDist = Mathf.Infinity;
        EnemyBehavior nearestEnemy = null;
        foreach (var enemy in Enemies)
        {
            float dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(enemy.transform.position.x, enemy.transform.position.y));
            //在攻击范围内，计算最近的敌人
            if (dist < atk_range && dist < nearestDist)
            {
                nearestDist = dist;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy)
        {
            //敌人没死，攻击CD好了
            if (nearestEnemy.hp > 0 && timer >= atk_time)
            {
                if(ty == Tower_ty.Magic) //魔法塔无视防御
                    nearestEnemy.BeAttacked(attack);
                else
                    nearestEnemy.BeAttacked((int)(attack * (1 - nearestEnemy.def)));

                if (ty == Tower_ty.Ice) //冰塔减速
                    nearestEnemy.BeSlowed();

                timer = 0; // 攻击CD重新计时
            }
        }
        #endregion
    }


    public void Set_Tower(Tower_ty tempty)
    {
        int return_cost=0;//拆毁时记录返回钱数量
        switch(tempty)
        {
            case Tower_ty.None:
                return_cost = destroy_return[(int)ty];
                ty = Tower_ty.None;
                attack = 0;
                atk_time = Mathf.Infinity;
                atk_range = 5;
                go_arch.SetActive(false);
                go_stone.SetActive(false);
                go_ice.SetActive(false);
                go_magic.SetActive(false);
                is_built = false;
                break;
            case Tower_ty.Arch:
                ty = Tower_ty.Arch;
                go_arch.SetActive(true);
                attack = 10;
                atk_time = 0.5f;
                atk_range = 3;
                is_built = true;
                break;
            case Tower_ty.Stone:
                ty = Tower_ty.Stone;
                go_stone.SetActive(true);
                attack = 20;
                atk_time = 1f;
                atk_range = 1.5f;
                is_built = true;
                break;
            case Tower_ty.Ice:
                ty = Tower_ty.Ice;
                go_ice.SetActive(true);
                attack = 5;
                atk_time = 1f;
                atk_range = 2;
                is_built = true;
                break;
            case Tower_ty.Magic:
                ty = Tower_ty.Magic;
                go_magic.SetActive(true);
                attack = 5;
                atk_time = 0.4f;
                atk_range = 3;
                is_built = true;
                break;
        }
        is_selected = false;
        if(return_cost==0)
        {
            UI = GameObject.Find("BasicAttribute");
            UI.GetComponent<Attribute_UI_controller>().change_money(-costs[(int)tempty]);
        }
        else
        {
            UI = GameObject.Find("BasicAttribute");
            UI.GetComponent<Attribute_UI_controller>().change_money(return_cost);
        }
        
    }
}
