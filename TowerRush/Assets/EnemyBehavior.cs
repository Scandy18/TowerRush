using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public Transform transform;

    private int route_count;
    private List<Vector3> route;
    private float move_speed = 0.5f;
    public bool is_dead;
    public int hp;

    void Start()
    {
        route_count = 0;
        route.Add(new Vector3(-1, 0.5f, 1));
        route.Add(new Vector3(4, 0.5f, 2));
        route.Add(new Vector3(9, 0.5f, 1));

        is_dead = false;
        transform = gameObject.GetComponent<Transform>();
        hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0 && !is_dead)
        {
            Debug.Log("Enemy dies.");
            is_dead = true;
        }
        else //活着
        {
            if(route_count < route.Count)
            {
                (route[route_count + 1] - route[route_count]).normalized * move_speed

            }
        }
    }

    public void beAttacked(int atk)
    {
        hp -= atk;
        Debug.Log("Enemy be attacked " + atk + "hp, now " + hp +"hp.");
    }
}
