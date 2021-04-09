using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSpin : Ability
{
    [SerializeField] GameObject Hitbox;
    [SerializeField] float Damage;
    Vector3 position;
    GameObject currentHitbox;
    float spinSpeed;
    Transform playerTrans;
    Vector3 cameraForward;
    Queue<GameObject> destroyHitbox;

    // Start is called before the first frame update
    void Start()
    {
        destroyHitbox = new Queue<GameObject>();
        Status = CastState.Ready;
        spinSpeed = 360.0f / ChannelDuration;
        playerTrans = gameObject.GetComponent<Transform>();
        position = playerTrans.transform.position;
        //cameraForward = gameObject.GetComponent<MovementBasic>().GetCameraTransLateral();
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
                    if(gameObject.GetComponent<SlashDash>().Status != CastState.Charging && gameObject.GetComponent<SlashDash>().Status != CastState.Channeling && gameObject.GetComponent<RegSlash>().Status != CastState.Charging && gameObject.GetComponent<RegSlash>().Status != CastState.Channeling)
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
        cameraForward = gameObject.GetComponent<MovementBasic>().GetCameraTransLateral();
        playerTrans.forward = cameraForward;
        base.CastAbility();
    }

    //Activates a hitbox as a child of the player and sets direction and creates a hitbox for the slash
    protected override void ChargeAbility()
    {
        base.ChargeAbility();
        position = playerTrans.position;
        currentHitbox = GameObject.Instantiate(Hitbox, position, Quaternion.identity);
        ChannelAbility();
    }

    // Gets Hit detection and applies damage
    protected override void ChannelAbility()
    {
        //While channeling duration is available
        if (ChannelDurationRemaining > 0)
        {
            //Rotate the particles
            ChannelDurationRemaining -= Time.deltaTime;
        }
        else //Put ability on cooldown
        {
            // if hits > 3, reset the cooldown on SlashDash
            if (currentHitbox.GetComponentInChildren<SpinSlashHitbox>().getNumCollisions() >= 3)
                gameObject.GetComponent<SlashDash>().ResetCooldown();

            List<GameObject> enemies = currentHitbox.GetComponent<SpinSlashHitbox>().getCollisions();

            for (int i = 0; i < enemies.Count; i++)
            {
                //Apply damage
                enemies[i].GetComponent<EnemyBase>().ApplyDamage(Damage);
            }

            destroyHitbox.Enqueue(currentHitbox);

            Invoke(nameof(DestroyBox), 0.5f + ChannelDuration);
            GetComponent<PlayerSFXManager>().Play(PlayerSFXManager.PlayerAudioKeys.SpinSlash);
            ChannelDurationRemaining = 0;
            Status = CastState.Cooldown;
        }
    }

    protected override void AbilityCooldown()
    {
        base.AbilityCooldown();
    }

    private void DestroyBox()
    {
        Destroy(destroyHitbox.Dequeue());
    }
}
