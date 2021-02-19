using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDash : Ability
{
    [SerializeField] float DashDistance;
    [SerializeField] GameObject hitbox;
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

    //Activates a hitbox as a child of the player and sets direction
    protected override void ChargeAbility()
    {
        base.ChargeAbility();
        direction = gameObject.transform.forward;
        ChannelAbility();
    }

    // Pushes the player forward and creates a hitbox for the slash
    protected override void ChannelAbility()
    {
        //While channeling duration is available, dash forward
        if (ChannelDurationRemaining > 0)
        {
            //Move
            gameObject.GetComponent<Rigidbody>().velocity = direction * dashSpeed;
            ChannelDurationRemaining -= Time.deltaTime;
        }
        else //Put ability on cooldown
        {
            gameObject.GetComponent<Rigidbody>().velocity = direction * 0;
            ChannelDurationRemaining = 0;
            Status = CastState.Cooldown;
        }
    }

    protected override void AbilityCooldown()
    {
        base.AbilityCooldown();
    }
}
