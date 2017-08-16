using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public AudioClip levelup;
    public AudioClip pickup;
    public AudioClip dead;

    public float potion_base = 10;
    public float attackdelay = 1;
    public float wait_attack_time = 0.5f;

    public bool abilityaim;
    public bool usingabilty = false;
    public bool hold_animation = false;
    public bool isdead = false;
    public bool autoattacking = false;

    public Difficulty difficulty;
    public AudioSource audio_source;
    public Canvas smallcanvas;
    public Target_Follow_Enemy target_follower;
    public Rigidbody rb;
    public Animator animator;
    public Player player;
    public Buttons abilityinusage;
    public List<Enemy> InRangeEnemyList = new List<Enemy>();
    public Enemy target;
    public RaycastHit hit;

    public virtual void initiate(Player p)
    {
    }
    public virtual void use_ability(Buttons b)
    {

    }
    public virtual void Start()
    {
        player = GetComponent<Player>();

        audio_source = GetComponent<AudioSource>();
        levelup = Statics.player_levelup;
        pickup = Statics.player_pickup;
        dead = Statics.player_dead;

        attackdelay = Time.time;

        target_follower = Instantiate(Statics.target_follower);
        target_follower.transform.SetParent(player.parent);

        smallcanvas = GetComponentInChildren<Canvas>();

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    IEnumerator attackwait(float time)
    {
        animator.SetTrigger("Attack1Trigger");
        attackdelay = Time.time;
        attackdelay += player.attackspeed;
        yield return new WaitForSeconds(time);
        deal_damage(target);
    }

    public virtual void deal_damage(Enemy _target)
    {
        if (_target != null)
        {
            _target.TakeDamage(player.base_damage);
        }
    }

    public void rotate_y_towards_transform(Transform t)
    {
        if (t != null)
        {
            var lookPos = t.position - transform.position;
            lookPos.y = 0;
            if (lookPos != new Vector3(0, 0, 0))
            {
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * player.speed);
            }
        }
    }

    public virtual void Update()
    {
        if (target != null)
        {
            rotate_y_towards_transform(target.transform);
        }
        autoattack();
    }

    public void autoattack()
    {
        if (autoattacking && Time.time >= attackdelay && !usingabilty)
        {
            if (target == null || target.isdead)
            {
                InRangeEnemyList.Remove(target);

                if (InRangeEnemyList.Count > 0)
                {
                    target = InRangeEnemyList[0];
                    updateTarget(target);
                }
                else
                {
                    autoattacking = false;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= player.attackRange)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        if (!target.isdead)
                        {
                            StartCoroutine(attackwait(wait_attack_time));
                        }
                        else
                        {
                            target = null;
                        }
                    }
                }
                else
                {
                    autoattacking = false;
                }
            }
        }
    }

    public void Target_isDead(Enemy t)
    {
        if (t == null)
        {
            InRangeEnemyList.Remove(t);
            if (!usingabilty)
            {
                if (InRangeEnemyList.Count > 0)
                {
                    target = InRangeEnemyList[0];
                    updateTarget(target);
                }
                else
                {
                    autoattacking = false;
                }
            }
        }
    }

    public void disableTargetMarker()
    {
        target_follower.resetTarget();
    }

    public void setDifficulty(string difficulty_string)
    {
        switch (difficulty_string)
        {
            case "Easy":
                difficulty = Difficulty.Easy;
                break;
            case "Normal":
                difficulty = Difficulty.Normal;
                break;
            case "Hard":
                difficulty = Difficulty.Hard;
                break;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (target == other.GetComponent<GameObject>())
            {
                autoattacking = false;
            }
            InRangeEnemyList.Remove(other.GetComponent<Enemy>());
        }
    }
    public virtual void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            InRangeEnemyList.Add(other.GetComponent<Enemy>());
            if (!autoattacking && !usingabilty)
            {

                target = other.GetComponent<Enemy>();
                updateTarget(target);
            }
        }
    }

    public void updateTarget(Enemy target)
    {
        autoattacking = true;
        this.target = target;
        if (target_follower)
        {
            target_follower.setCurrentTarget(target);
        }
    }

    /* For future use of ability animations */
    public void ability_animation(Buttons ability, bool animation_start)
    {
        abilityinusage = ability;
        // remove target
        target = null;

        if (!animation_start)
        {
            hold_animation = true;
        }

        switch (ability)
        {
            case Buttons.ability1:
                ability_animation_1();
                break;
            case Buttons.ability2:
                ability_animation_2();
                break;
            case Buttons.ability3:
                ability_animation_3();
                break;
            case Buttons.ability4:
                ability_animation_4();
                break;
        }
    }
    public virtual void ability_animation_1()
    {

    }
    public virtual void ability_animation_2()
    {

    }
    public virtual void ability_animation_3()
    {

    }
    public virtual void ability_animation_4()
    {

    }

    public virtual void FirstAbility()
    {
    }
    public virtual void SecondAbility()
    {
    }
    public virtual void ThirdAbility()
    {
    }
    public virtual void FourthAbility()
    {
    }
    public virtual void potionClicked()
    {
    }
}
