using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public enum CastState
    {
        Ready = 0,
        Charging = 1,
        Channeling = 2,
        Cooldown = 3
    }

    public CastState Status;
    [SerializeField] protected float ChargeTime;
    [SerializeField] protected float ChannelDuration;
    [SerializeField] protected float CoolDownDuration;

    protected float ChargeTimeRemaining;
    protected float ChannelDurationRemaining;
    protected float CoolDownDurationRemaining;
    protected bool casted;

    public float CoolDownDurationRemainingPub;


    // Start is called before the first frame update
    void Start()
    {
        Status = CastState.Ready;
        casted = false;
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
    protected virtual void CastAbility()
    {
        print("Casting");
        // Set values to prep for casting
        ChargeTimeRemaining = ChargeTime;
        ChannelDurationRemaining = ChannelDuration;
        CoolDownDurationRemaining = CoolDownDuration;

        //Change status
        Status = CastState.Charging;
    }

    //Handles Charging Ability
    protected virtual void ChargeAbility()
    {
        //Wait for activation animations
        if (ChargeTimeRemaining > 0)
        {
            ChargeTimeRemaining -= Time.deltaTime;
            print("Charging");
        }
        else
        {
            ChargeTimeRemaining = 0;
            Status = CastState.Channeling;
        }
    }

    //Handles Channeling the Ability
    protected virtual void ChannelAbility()
    {
        //While channeling durations is available
        if(ChannelDurationRemaining > 0)
        {
            //Do Stuff
            ChannelDurationRemaining -= Time.deltaTime;
        }
        else //Put ability on cooldown
        {
            ChannelDurationRemaining = 0;
            Status = CastState.Cooldown;
        }
    }

    //Handles the Ability Cooldown
    protected virtual void AbilityCooldown()
    {
        //Wait for Cooldown to finish
        if(CoolDownDurationRemaining > 0)
        {
            CoolDownDurationRemaining -= Time.deltaTime;
            CoolDownDurationRemainingPub = CoolDownDurationRemaining;
        }
        else
        {
            CoolDownDurationRemaining = 0;
            CoolDownDurationRemainingPub = CoolDownDurationRemaining;
            Status = CastState.Ready;
        }
    }

    public void ResetCooldown()
    {
        if(Status == CastState.Cooldown)
            Status = CastState.Ready;
    }

    public void Cast()
    {
        if (Status == CastState.Ready)
            casted = true;
    }
}
