using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{

    public float x = 0;
    public float y = 1;
    public float z = 0;

    private float _x;
    private float _y;
    private float _z;
    public float rotspeed = 5;

    public bool randomrot = false;

    void Update()
    {
        if (!randomrot)
        {
            transform.Rotate(rotspeed * x + Time.deltaTime, rotspeed * y + Time.deltaTime, rotspeed * z + Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 1) < 0.5)
            {
                _x = rotspeed * x;
            }
            else
            {
                _x = 0;
            }
            if (Random.Range(0, 1) < 0.5)
            {
                _y = rotspeed * y;
            }
            else
            {
                _y = 0;
            }
            if (Random.Range(0, 1) < 0.5)
            {
                _z = rotspeed * z;
            }
            else
            {
                _z = 0;
            }
            transform.Rotate(_x, _y, _z);
        }

    }
}
