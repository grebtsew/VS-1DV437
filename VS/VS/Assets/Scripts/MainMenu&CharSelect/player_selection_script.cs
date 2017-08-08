using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_selection_script : MonoBehaviour
{

    public Player_Character character;
    private SkinnedMeshRenderer smr;
    private Material temp_material;
    private Material lock_material;
    public bool unlocked = false;

    void Start()
    {
        smr = GetComponentInChildren<SkinnedMeshRenderer>();
        temp_material = smr.material;

        lock_material = Resources.Load("Materials/Black_transparent_Material", typeof(Material)) as Material;

    }

    public void lock_character()
    {
        smr.material = lock_material;
    }

    public void unlock_character()
    {
        unlocked = true;

    }
}
