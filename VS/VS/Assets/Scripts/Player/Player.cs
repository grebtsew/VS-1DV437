using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health = 100;
    public float energy = 100;
    public float experience = 0;
    public float level = 1;
    private float level_experience = 20;
    public float resist = 0;
    private float LEVEL_MIN_XP = 20f;
    private float SLIDER_MAX = 100;
    public float energyreg_speed = 0.5f;
    public float healthreg_speed = 0.5f;
    private float energytime;
    private float healthtime;
    public float current_level_xp = 0;
    public float base_damage = 10;
    public float attackRange = 5;
    public float speed = 1;
    public float attackspeed = 1;
    private float resist_multi = 4;
    private bool gameOver = false;

    public Slider small_healthslider;
    public Slider small_energyslider;

    public bool invulnerable = false;
    public bool ability_mode = false;
    private bool isdead = false;
    public bool initiated = false;

    public global_game_controller global_game_controller;

    public int level_ability_points = 0;
    public int a1_level = 0;
    public int a2_level = 0;
    public int a3_level = 0;
    public int a4_level = 0;
    public int passive = 0;
    public int potion_level = 0;

    public static int player_amount = 0;

    public Transform parent;
    public Player_Controll controll;
    public Player_Character character;
    public PlayerHUDController playerhud;
    public Controller controller;
    private toggle_canvas toggle_canvas;
    private LevelUpPanel_Controller leveluppanel;
    public Map_script map_reference;

    public string Name = "";

    public Player()
    {
        player_amount++;
    }
    public void setController(Player_Controll c)
    {
        controll = c;
        if(controll == Player_Controll.Player)
        {
            controller = GetComponent<Player_Controller>();
            GetComponent<Ai_Controller>().enabled = false;

        } else if(controll == Player_Controll.Ai)
        {
            controller = GetComponent<Ai_Controller>();
            GetComponent<Player_Controller>().enabled = false;
        }
       
    }
    public void InitiatePlayer(global_game_controller game, Transform par)
    {
        global_game_controller = game;
        this.parent = par;

        // initiate HUD & Map
        if (controll == Player_Controll.Player)
        {
            toggle_canvas = FindObjectOfType<toggle_canvas>();
            toggle_canvas.initiate(this);

            leveluppanel = FindObjectOfType<LevelUpPanel_Controller>();
            leveluppanel.initiate(this);

            playerhud = FindObjectOfType<PlayerHUDController>();
            playerhud.initiate(this);
            playerhud.updateAllLabels();
        }

        map_reference = GetComponentInParent<Map_script>();
        map_reference.initiate(this);

        initiated = true;
        controller.initiate(this);

        setDifficultDependent(controller.difficulty);
    }
    private void setDifficultDependent(Difficulty d)
    {
        switch (d)
        {
            case Difficulty.Easy:
                
                break;
            case Difficulty.Normal:
                base_damage = 30;
                break;
            case Difficulty.Hard:
                base_damage = 20;
                break;
        }
    }

    public void PowerUpTaken(PowerUp bonus, float value)
    {
        switch (bonus)
        {
            case PowerUp.Energy:
                energy += value;
                if (controll == Player_Controll.Player)
                {
                    playerhud.energyslider.value = energy;
                }

                small_energyslider.value = energy;

                break;
            case PowerUp.Health:
                health += value;

                if (controll == Player_Controll.Player)
                {
                    playerhud.healthslider.value = health;
                }

                small_healthslider.value = health;

                break;
            case PowerUp.Damage:
                base_damage += value;
                if (controll == Player_Controll.Player)
                {
                    playerhud.updateDamage();
                }
                break;
        }
    }
    public virtual void TakeDamage(float damage)
    {
        if (!invulnerable)
        {
            health -= damage - (damage / 100) * resist * resist_multi;

            if (controll == Player_Controll.Player)
            {
                playerhud.healthslider.value = health;
            }

            small_healthslider.value = health;

            if (!isdead)
            {
                FloatingTextController.CreateFloatingText(damage.ToString(), transform, Color.red);
            }
        }
    }
    public virtual void Start()
    {
        gameOver = false;
    }
    public virtual void passiveStatic()
    {

    }
    public virtual void PassiveUpdate()
    {

    }

    public void use_ability_point(Buttons b)
    {
        switch (b)
        {
            case Buttons.ability1:
                a1_level++;
                break;
            case Buttons.ability2:
                a2_level++;
                break;
            case Buttons.ability3:
                a3_level++;
                break;
            case Buttons.ability4:
                a4_level++;
                break;
            case Buttons.potion:
                potion_level++;
                break;
            case Buttons.passive:
                passive++;
                passiveStatic();
                break;
        }

        level_ability_points--;
        if (controll == Player_Controll.Player)
        {
            playerhud.updateLevelUpPoints();
        }
    }
    public bool got_ability_point()
    {
        return level_ability_points > 0;
    }

    public void addXP(float xp)
    {
        experience += xp;
        current_level_xp = (experience / level_experience) * SLIDER_MAX;

            if (controll == Player_Controll.Player)
            {
                playerhud.experienceslider.value = current_level_xp;
            }

        if (current_level_xp >= SLIDER_MAX)
        {
            
            levelUp();
        }
    }

    private void levelUp()
    {

        level++;
        level_ability_points++;
        level_experience = level * LEVEL_MIN_XP;
        experience = 0;
        resist++;
        base_damage++;

        if (controll == Player_Controll.Player)
        {
            playerhud.experienceslider.value = 0;
            playerhud.updateAllLabels();
        }

        FloatingTextController.CreateFloatingText("Level Up", transform, Color.magenta);

        controller.audio_source.clip = controller.levelup;
        controller.audio_source.Play();

    }

    private void checkOutOfBounds()
    {
        if (transform.position.y <= -10)
        {
            TakeDamage(100);
        }
    }

    private void checkIsDead()
    {
        if (health <= 0)
        {
            isdead = true;
            // game over
            gameOver = true;

            global_game_controller.GameOver(this);

            //sound
            controller.audio_source.clip = controller.dead;
            controller.audio_source.Play();
        }


    }

    private void checkToMuchResources()
    {
        if (health > 100)
        {
            health = 100;
        }

        if (energy > 100)
        {
            energy = 100;
        }
    }

    private void energyRegeneration()
    {
        if (energy < 100)
        {
            if (Time.time >= energytime)
            {
                energytime = Time.time + energyreg_speed;


                energy++;

                if (controll == Player_Controll.Player)
                {
                    playerhud.energyslider.value = energy;
                }

                small_energyslider.value = energy;

            }
        }
    }

    private void healthRegeneration()
    {
        if (health < 100)
        {
            if (Time.time >= healthtime)
            {
                healthtime = Time.time + healthreg_speed;

                health++;
                if (controll == Player_Controll.Player)
                {
                    playerhud.healthslider.value = health;
                }
                small_healthslider.value = health;
            }
        }
    }

    public virtual void Update()
    {
        if (initiated && !isdead)
        {
            PassiveUpdate();
            checkOutOfBounds();
            checkIsDead();
            checkToMuchResources();
            healthRegeneration();
            energyRegeneration();
        }
    }

    public bool gotEnoughtEnergy(float cost)
    {
        return energy >= cost;
    }

    public void useEnergy(float useamount)
    {
        if (energy >= useamount)
        {
            energy -= useamount;

            if (controll == Player_Controll.Player)
            {
                playerhud.energyslider.value = energy;
            }
            small_energyslider.value = energy;
        }
    }

}
