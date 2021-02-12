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

    public bool cast = false;

    public CastState Status;
    [SerializeField] float ChargeTime;
    [SerializeField] float ChannelDuration;
    [SerializeField] float CoolDownDuration;

    private float ChargeTimeRemaining;
    private float ChannelDurationRemaining;
    private float CoolDownDurationRemaining;


    // Start is called before the first frame update
    void Start()
    {
        Status = CastState.Ready;
    }

    // Update is called once per frame
    void Update()
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
    protected void CastAbility()
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
    protected void ChargeAbility()
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
    protected void ChannelAbility()
    {
        //While channeling durations is available
        if(ChannelDurationRemaining > 0)
        {
            //Do Stuff
            print("Channeling");
            ChannelDurationRemaining -= Time.deltaTime;
        }
        else //Put ability on cooldown
        {
            ChannelDurationRemaining = 0;
            Status = CastState.Cooldown;
        }
    }

    //Handles the Ability Cooldown
    protected void AbilityCooldown()
    {
        //Wait for Cooldown to finish
        if(CoolDownDurationRemaining > 0)
        {
            CoolDownDurationRemaining -= Time.deltaTime;
            print("Cooling Down");
        }
        else
        {
            CoolDownDurationRemaining = 0;
            Status = CastState.Ready;
        }
    }

    public void ResetCooldown()
    {
        if(Status == CastState.Cooldown)
            Status = CastState.Ready;
    }
}
