using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : Enemy
{

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    // Use this for initialization
    public override void Start()
    {
        // Super Start
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

}
