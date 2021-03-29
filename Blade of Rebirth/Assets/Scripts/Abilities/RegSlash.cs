using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegSlash : Ability
{
    [SerializeField] GameObject hitbox;
    [SerializeField] float Damage;
    GameObject currentHitbox;

    // Start is called before the first frame update
    void Start()
    {
        Status = CastState.Ready;
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
                    if (gameObject.GetComponent<SlashSpin>().Status != CastState.Charging && gameObject.GetComponent<SlashSpin>().Status != CastState.Channeling && gameObject.GetComponent<SlashDash>().Status != CastState.Charging && gameObject.GetComponent<SlashDash>().Status != CastState.Channeling)
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
        currentHitbox = GameObject.Instantiate(hitbox, gameObject.transform);
        //gameObject.layer = LayerMask.NameToLayer("PlayerNoCollision");
        ChannelAbility();
    }

    // Pushes the player forward
    protected override void ChannelAbility()
    {
        //While channeling duration is available, dash forward
        if (ChannelDurationRemaining > 0)
        {
            ChannelDurationRemaining -= Time.deltaTime;
        }
        else //Put ability on cooldown
        {
            //Do damage to each enemy
            List<GameObject> enemies = currentHitbox.GetComponent<RegSlashHitbox>().getCollisions();

            if (enemies != null)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].GetComponent<EnemyBase>().ApplyDamage(Damage);
                }

            }

            Destroy(currentHitbox);
            //gameObject.layer = LayerMask.NameToLayer("PlayerNoCollision");
            ChannelDurationRemaining = 0;
            Status = CastState.Cooldown;
        }
    }

    protected override void AbilityCooldown()
    {
        base.AbilityCooldown();
    }
}
