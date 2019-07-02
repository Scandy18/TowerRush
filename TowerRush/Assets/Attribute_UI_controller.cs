using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Attribute_UI_controller : MonoBehaviour
{
    private int money=0;
    private int curWave = 0;
    private int maxWave = 0;
    private int hp = 0;
    private int leftEnemy = 0;

    public Text money_text,wave_text,hp_text,leftEnemy_text;

    public int Money { get => money; set => money = value; }
    public int CurWave { get => curWave; set => curWave = value; }
    public int MaxWave { get => maxWave; set => maxWave = value; }
    public int Hp { get => hp; set => hp = value; }
    public int LeftEnemy { get => leftEnemy; set => leftEnemy = value; }

    public GameObject win,UI;

    void Start()
    {
        init_attributes(200, 1, 5, 10, 10);
    }

    public void init_attributes(  int money = 0,int curWave = 0,int maxWave = 0,int hp = 0,int leftEnemy = 0)
    {
        Money = money;
        CurWave = curWave;
        MaxWave = maxWave;
        Hp = hp;
        LeftEnemy = leftEnemy;
        all_changed();
    }

    void money_changed()
    {
        money_text.text = "money:" +Money;
    }
    void wave_changed()
    {
        wave_text.text = "wave:" + CurWave + "/" + MaxWave;
    }
    void enemy_changed()
    {
        leftEnemy_text.text = "enemy:" + LeftEnemy;
    }
    void hp_changed()
    {
        hp_text.text = "hp:" + Hp;
    }

    void all_changed()
    {
        money_changed();
        wave_changed();
        enemy_changed();
        hp_changed();
    }

    //正为加,负为减,不驳回负数操作
    public void change_money(int num)
    {
        Money += num;
        if(Money<0)
        {
            Debug.Log("money<0!");
        }
        money_changed();
    }

    //正为加,负为减,不驳回负数操作
    public void change_hp(int num)
    {
        Hp += num;
        hp_changed();
    }

    public void enemy_death(int reward)
    {
        change_money(reward);
        leftEnemy-=1;
        if (leftEnemy == 0 && CurWave < MaxWave)
            CurWave++;
        else
            levelwin();
        enemy_changed();
        wave_changed();
    }
    public void enemy_cross(int damage)
    {
        change_hp(-damage);
        if(Hp<=0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    void levelwin()
    {
        win.SetActive(true);
        UI.SetActive(false);
    }
}
