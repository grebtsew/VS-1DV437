using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel_Controller : MonoBehaviour {

    private Canvas canvas;

    private abilitybutton a1;
    private abilitybutton a2;
    private abilitybutton a3;
    private abilitybutton a4;
    private abilitybutton potion;
    private abilitybutton passive;
    private Player player;

    public void initiate(Player p)
    {
        player = p;
        a1.initiate(player);
        a2.initiate(player);
        a3.initiate(player);
        a4.initiate(player);
        potion.initiate(player);
        passive.initiate(player);

    }

    // Use this for initialization
    void Start () {
        
     
        canvas = GetComponent<Canvas>();
        disable();
        
        foreach(abilitybutton ab in FindObjectsOfType<abilitybutton>())
        {
            switch (ab.b)
            {
                case Buttons.ability1:
                    a1 = ab;
                    break;
                case Buttons.ability2:
                    a2 = ab;
                    break;
                case Buttons.ability3:
                    a3 = ab;
                    break;
                case Buttons.ability4:
                    a4 = ab;
                    break;
                case Buttons.passive:
                    passive = ab;
                    break;
                case Buttons.potion:
                    potion = ab;
                    break;
            }
        }
    }

    public void disable()
    {
        canvas.enabled = false;
    }

    public bool isEnabled()
    {
        return canvas.enabled;
    }

    private void setAbilityActive(abilitybutton a)
    {
        if (a.getAbilityLevel() <= 0)
        {
            a.MakeInActive();
        }
        else
        {
            a.MakeActive();
        }
    }

    public void toggleLevelUp()
    {
        if(player != null) {
        if (canvas.enabled)
        {
            canvas.enabled = false;

            setAbilityActive(a1);
            setAbilityActive(a2);
            setAbilityActive(a3);
            setAbilityActive(a4);
            setAbilityActive(potion);
            setAbilityActive(passive);
          
        } else
        {
            canvas.enabled = true;

            if (player.got_ability_point()) {
                ability1_tree();
                ability2_tree();
                ability3_tree();
                ability4_tree();
                potion.MakeActive();
                passive.MakeActive();
            }
        }
        }
    }


    private void ability1_tree()
    {
        if (a1.getAbilityLevel() <= 2)
        {
            a1.MakeActive();
        } else
        if (a1.getAbilityLevel() <= 4 && player.level >= 6)
        {
            a1.MakeActive();
        } else
        if (a1.getAbilityLevel() <= 5 && player.level >= 10)
        {
            a1.MakeActive();
        } else
        {
            a1.MakeInActive();
        }
    }

    private void ability2_tree()
    {
        if (a2.getAbilityLevel() <= 2)
        {
            a2.MakeActive();
        } else
        if (a2.getAbilityLevel() <= 4 && player.level >= 6)
        {
            a2.MakeActive();
        }
        else
        if (a2.getAbilityLevel() <= 5 && player.level >= 10)
        {
            a2.MakeActive();
        }
        else
        {
            a2.MakeInActive();
        }
    }

    private void ability3_tree()
    {
        if (a3.getAbilityLevel() <= 2)
        {
            a3.MakeActive();
        }
        else
        if (a3.getAbilityLevel() <= 4 && player.level >= 6)
        {
            a3.MakeActive();
        }
        else
        if (a3.getAbilityLevel() <= 5 && player.level >= 10)
        {
            a3.MakeActive();
        }
        else
        {
            a3.MakeInActive();
        }
    }

    private void ability4_tree()
    {
       
        if (a4.getAbilityLevel() <= 2 && player.level >= 6)
        {
            a4.MakeActive();
        }else

        if (a4.getAbilityLevel() <= 4 && player.level >= 10)
        {
            a4.MakeActive();
        }
        else

        if (a4.getAbilityLevel() <= 5 && player.level >= 12)
        {
            a4.MakeActive();
        }
        else
        {
            a4.MakeInActive();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
