using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //UI
    GameObject UI;
    //组件
    public Transform transform;
    public MeshRenderer meshRenderer;
    //出生
    public float SpawnTime;
    private float Timer;
    private bool can_be_atk;
    //行进
    private int route_count;
    public List<Vector3> route;
    public float move_speed;
    //属性
    public GameObject hp_bar;
    public Transform hp_bar_tran;
    private bool is_dead;
    public float def;
    public int hp;
    public int reward = 30;//奖励money
    public int damage = 10;//对博士的伤害
    //用于减速
    private float slow_timer = 0;
    private bool is_slowed;

    

    void Start()
    {
        route_count = 0;
        is_slowed = false;
        is_dead = false;
        //def = 0.5f;
        //hp = 500;
        //move_speed = 0.5f;
        hp_bar_tran = hp_bar.GetComponent<Transform>();
        transform = gameObject.GetComponent<Transform>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        can_be_atk = false;
        transform.position = route[0];
    }

    // Update is called once per frame
    void Update()
    {
        //血条
        hp_bar_tran.localScale = new Vector3(0.15f, 0.7f * hp / 500, 0.15f);
        //减速
        if(is_slowed)
        {
            slow_timer += Time.deltaTime;
            move_speed = 0.2f;
        }
        else
        {
            move_speed = 0.5f;
        }
        if(slow_timer >= 0.3f)
        {
            is_slowed = false;
        }
        //计时器
        Timer += Time.deltaTime;
        //死亡判断
        if (hp <= 0 && !is_dead)
        {
            Debug.Log("Enemy dies.");
            is_dead = true;
            gameObject.SetActive(false);
            UI = GameObject.Find("BasicAttribute");
            UI.GetComponent<Attribute_UI_controller>().enemy_death(reward);
        }
        //到达终点
        if(route_count >= route.Count - 1)
        {
            Debug.Log("Enemy arrives.");
            gameObject.SetActive(false);
        }
        //匀速移动
        if (!is_dead && route_count < route.Count - 1 && meshRenderer.enabled)
        {
            transform.position += (route[route_count + 1] - route[route_count]).normalized * move_speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, route[route_count + 1]) < 0.02f)
                route_count++;
        }
        //敌人出生了
        if (Timer >= SpawnTime)
        {
            meshRenderer.enabled = true;
            can_be_atk = true;
        }
    }

    //被攻击接口
    public void BeAttacked(int atk)
    {
        if(can_be_atk)
        {
            hp -= atk;
            Debug.Log("Enemy be attacked " + atk + "hp, now " + hp + "hp.");
        }
    }

    public void BeSlowed()
    {
        Debug.Log("slow.");
        is_slowed = true;
        slow_timer = 0;
    }
    
    //怪物到达终点
    public void cross()
    {
        UI = GameObject.Find("BasicAttribute");
        UI.GetComponent<Attribute_UI_controller>().enemy_cross(damage);
    }
}
