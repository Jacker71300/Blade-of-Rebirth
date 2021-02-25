using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDash : Ability
{
    [SerializeField] float DashDistance;
    [SerializeField] GameObject hitbox;
    [SerializeField] float Damage;
    GameObject currentHitbox;
    Vector3 direction;
    float dashSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Status = CastState.Ready;
        dashSpeed = DashDistance / ChannelDuration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (Status)
        {
            //Waits for ability activation
            case CastState.Ready:
                if (casted)
                {
                    casted = false;

                    // Check to make sure that the player can't channel both abilities at the same time
                    if (gameObject.GetComponent<SlashSpin>().Status != CastState.Charging && gameObject.GetComponent<SlashSpin>().Status != CastState.Channeling)
                        CastAbility();
                }
                break;

            //Handles Charging ability
            case CastState.Charging:
                ChargeAbility();
                break;

            //Channels ability effects
            case CastState.Channeling:
                ChannelAbility();
                break;

            //Handles Ability Cooldown
            case CastState.Cooldown:
                AbilityCooldown();
                break;
        }
    }

    //Sets/Preps defaults for casting ability
    protected override void CastAbility()
    {
        base.CastAbility();
    }

    //Activates a hitbox as a child of the player and sets direction and creates a hitbox for the slash
    protected override void ChargeAbility()
    {
        base.ChargeAbility();
        direction = gameObject.transform.forward;
        currentHitbox = GameObject.Instantiate(hitbox, gameObject.transform);
        gameObject.layer = LayerMask.NameToLayer("PlayerNoCollision");
        ChannelAbility();
    }

    // Pushes the player forward
    protected override void ChannelAbility()
    {
        //While channeling duration is available, dash forward
        if (ChannelDurationRemaining > 0)
        {
            //Move
            gameObject.GetComponent<CharacterController>().Move(direction * dashSpeed * Time.deltaTime);
            ChannelDurationRemaining -= Time.deltaTime;
        }
        else //Put ability on cooldown
        {
            //Do damage to each enemy
            List<GameObject> enemies = currentHitbox.GetComponent<DashSlashHitbox>().getCollisions();

            for(int i = 0; i < enemies.Count; i++)
            {
                //Apply damage and reset cooldown on SlashSpin if the enemy dies
                enemies[i].GetComponent<EnemyBase>().ApplyDamage(Damage, gameObject.GetComponent<SlashSpin>().ResetCooldown);
            }

            Destroy(currentHitbox);
            gameObject.layer = LayerMask.NameToLayer("PlayerNoCollision");
            ChannelDurationRemaining = 0;
            Status = CastState.Cooldown;
        }
    }

    protected override void AbilityCooldown()
    {
        base.AbilityCooldown();
    }
}
