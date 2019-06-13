using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public Transform transform;

    public bool is_dead;
    public int hp;

    void Start()
    {
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
    }

    public void beAttacked(int atk)
    {
        hp -= atk;
        Debug.Log("Enemy be attacked " + atk + "hp, now " + hp +"hp.");
    }
}
