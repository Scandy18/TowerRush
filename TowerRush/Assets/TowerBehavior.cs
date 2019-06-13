using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{

    private enum Tower_ty { None, Arch, Stone, Ice, Magic }
    private float timer;
    //区域属性
    private bool is_selected = true;
    private bool is_built = false;

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

    //敌人列表
    public List<EnemyBehavior> Enemies;

    void Start()
    {
        transform = gameObject.GetComponent<Transform>();
        timer = 0;

        Tower_Destroy();
        hp = 100;
        attack = 0;
        atk_range = 0;
        ty = Tower_ty.None;
        atk_time = Mathf.Infinity;
        
    }

    void Update()
    {
        //选中未建造区域可以建造塔
        if(is_selected && !is_built)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                ty = Tower_ty.Arch;
                go_arch.SetActive(true);
                attack = 10;
                atk_time = 0.5f;
                atk_range = 5;
                Debug.Log("Set to Arch Tower.");
                is_built = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                ty = Tower_ty.Stone;
                go_stone.SetActive(true);
                attack = 20;
                atk_time = 1f;
                atk_range = 5;
                Debug.Log("Set to Stone Tower.");
                is_built = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                ty = Tower_ty.Ice;
                go_ice.SetActive(true);
                attack = 5;
                atk_time = 1f;
                atk_range = 5;
                Debug.Log("Set to Ice Tower.");
                is_built = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                ty = Tower_ty.Magic;
                go_magic.SetActive(true);
                attack = 30;
                atk_time = 0.4f;
                atk_range = 5;
                Debug.Log("Set to Magic Tower.");
                is_built = true;
            }
        }
        //选中已建造区域可以摧毁塔
        if(is_selected && is_built)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
            {
                ty = Tower_ty.None;
                Tower_Destroy();
                attack = 0;
                atk_time = Mathf.Infinity;
                atk_range = 5;
                Debug.Log("Destroy the Tower.");
                is_built = false;
            }
        }
        //攻击逻辑
        timer += Time.deltaTime;
        foreach (var enemy in Enemies)
        {
            float dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(enemy.transform.position.x, enemy.transform.position.y));
            //条件：在攻击范围内，有攻击力，敌人没死，攻击CD好了
            if (dist < 100 && attack != 0 && enemy.hp > 0 && timer >= atk_time)
            {
                enemy.beAttacked(attack);
                timer = 0;
            }
        }
    }

    private void Tower_Destroy()
    {
        ty = Tower_ty.None;
        go_arch.SetActive(false);
        go_stone.SetActive(false);
        go_ice.SetActive(false);
        go_magic.SetActive(false);
    }
}
