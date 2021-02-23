using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSpin : Ability
{
    [SerializeField] GameObject Hitbox;
    GameObject currentHitbox;
    float spinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Status = CastState.Ready;
        spinSpeed = 360.0f / ChannelDuration;
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
                    if(gameObject.GetComponent<SlashDash>().Status != CastState.Charging && gameObject.GetComponent<SlashDash>().Status != CastState.Channeling)
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
        currentHitbox = GameObject.Instantiate(Hitbox, gameObject.transform);
        ChannelAbility();
    }

    // Rotates the hitbox around the player
    protected override void ChannelAbility()
    {
        //While channeling duration is available, rotate the hitbox
        if (ChannelDurationRemaining > 0)
        {
            // Rotate
            currentHitbox.transform.Rotate(0, -spinSpeed * Time.deltaTime, 0, Space.Self);
            ChannelDurationRemaining -= Time.deltaTime;
        }
        else //Put ability on cooldown
        {
            // if hits > 3, reset the cooldown on SlashDash
            if (currentHitbox.GetComponentInChildren<SpinSlashHitbox>().getNumCollisions() >= 3)
                gameObject.GetComponent<SlashDash>().ResetCooldown();

            Destroy(currentHitbox);
            ChannelDurationRemaining = 0;
            Status = CastState.Cooldown;
        }
    }

    protected override void AbilityCooldown()
    {
        base.AbilityCooldown();
    }
}