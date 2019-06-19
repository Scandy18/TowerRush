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
    private float move_speed = 0.0005f;

    public bool is_dead;
    public int hp;

    void Start()
    {
        route_count = 0;
        //设定路线
        route = new List<Vector3>();
        route.Add(new Vector3(-3, 0.5f, 2));
        route.Add(new Vector3(-3, 0.5f, 1));
        route.Add(new Vector3(-1, 0.5f, 2));

        is_dead = false;
        hp = 500;

        transform = gameObject.GetComponent<Transform>();
        transform.position = route[0];
    }

    // Update is called once per frame
    void Update()
    {
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
            transform.position += (route[route_count + 1] - route[route_count]).normalized * move_speed / Time.deltaTime;
            Debug.Log("move");
            if (Vector3.Distance(transform.position, route[route_count + 1]) < 0.02f)
                route_count++;
        }
    }

    //被攻击接口
    public void beAttacked(int atk)
    {
        hp -= atk;
        Debug.Log("Enemy be attacked " + atk + "hp, now " + hp +"hp.");
    }
}
