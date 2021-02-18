using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDash : Ability
{
    [SerializeField] float DashDistance;
    [SerializeField] GameObject hitbox;

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
                if (cast)
                    CastAbility();
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

    //Activates a hitbox as a child of the player
    protected override void ChargeAbility()
    {
        base.ChargeAbility();
        ChannelAbility();
    }

    // Pushes the player forward and creates a hitbox for the slash
    protected override void ChannelAbility()
    {
        //While channeling duration is available, dash forward
        if (ChannelDurationRemaining > 0)
        {
            //Calculate this frame's movement
            float currentMoveDistance = DashDistance * Time.deltaTime / ChannelDuration;

            //Move
            gameObject.transform.vel
        }
        else //Put ability on cooldown
        {
            ChannelDurationRemaining = 0;
            Status = CastState.Cooldown;
        }
    }

    protected override void AbilityCooldown()
    {
        base.AbilityCooldown();
    }
}
