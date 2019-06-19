using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public Transform transform;
    public float SpawnTime;
    private float Timer;

    private int route_count;
    private List<Vector3> route;
    private float move_speed;

    public GameObject hp_bar;
    public Transform hp_bar_tran;
    public bool is_dead;
    public float def;
    public int hp;
    //用于减速
    private float slow_timer = 0;
    private bool is_slowed = false;

    void Start()
    {
        route_count = 0;
        //设定路线
        route = new List<Vector3>();
        route.Add(new Vector3(-3, 0.5f, 2));
        route.Add(new Vector3(-3, 0.5f, 1));
        route.Add(new Vector3(-1, 0.5f, 1));
        route.Add(new Vector3(-1, 0.5f, 0));
        route.Add(new Vector3(2, 0.5f, 0));
        route.Add(new Vector3(2, 0.5f, -2));
        route.Add(new Vector3(1, 0.5f, -2));
        route.Add(new Vector3(1, 0.5f, -3));
        route.Add(new Vector3(-3, 0.5f, -3));

        is_dead = false;
        def = 0.5f;
        hp = 500;
        move_speed = 0.5f;

        hp_bar_tran = hp_bar.GetComponent<Transform>();
        transform = gameObject.GetComponent<Transform>();
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
        }
        //匀速移动
        if (!is_dead && route_count < route.Count - 1)
        {
            transform.position += (route[route_count + 1] - route[route_count]).normalized * move_speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, route[route_count + 1]) < 0.02f)
                route_count++;
        }
    }

    //被攻击接口
    public void BeAttacked(int atk)
    {
        hp -= atk;
        Debug.Log("Enemy be attacked " + atk + "hp, now " + hp +"hp.");
    }

    public void BeSlowed()
    {
        Debug.Log("slow.");
        is_slowed = true;
        slow_timer = 0;
    }
    
}
