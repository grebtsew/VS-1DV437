using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Follow_Enemy : MonoBehaviour
{

    private Enemy enemy;
    public float rotatespeed = 30;
    private float y_pos = 1.71f;


    // Use this for initialization
    void Start()
    {
        disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            transform.position = new Vector3(enemy.transform.position.x, y_pos, enemy.transform.position.z);

        }

        transform.Rotate(0, Time.deltaTime * rotatespeed, 0, Space.World);

    }

    public void setCurrentTarget(Enemy enemy)
    {
        this.enemy = enemy;
        enable();
    }

    public void setPosition(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, y_pos, pos.z);
        enemy = null;
        enable();
    }

    public void resetTarget()
    {
        enemy = null;
        disable();
    }

    public bool hasTarget()
    {
        return enemy != null;
    }

    public void disable()
    {
        gameObject.SetActive(false);
    }
    public void enable()
    {
        gameObject.SetActive(true);
    }



}
