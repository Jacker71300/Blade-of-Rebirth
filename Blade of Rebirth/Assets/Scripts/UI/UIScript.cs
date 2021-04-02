using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    float ability1Cooldown;
    float ability2Cooldown;
    public Text ability1Text;
    public Text ability2Text;

    // Start is called before the first frame update
    void Start()
    {
        ability1Cooldown = player.GetComponent<SlashSpin>().GetComponentInParent<Ability>().CoolDownDurationRemainingPub;
        ability2Cooldown = player.GetComponent<SlashDash>().GetComponentInParent<Ability>().CoolDownDurationRemainingPub;
    }

    // Update is called once per frame
    void Update()
    {
        ability1Cooldown = player.GetComponent<SlashSpin>().GetComponentInParent<Ability>().CoolDownDurationRemainingPub;
        ability2Cooldown = player.GetComponent<SlashDash>().GetComponentInParent<Ability>().CoolDownDurationRemainingPub;

        if(ability1Cooldown == 0)
        {
            ability1Text.text = "Ready!";
        }
        else
        {
            ability1Text.text = ((int)ability1Cooldown + 1).ToString();
        }

        if(ability2Cooldown == 0)
        {
            ability2Text.text = "Ready!";
        }
        else
        {
            ability2Text.text = ((int)ability2Cooldown + 1).ToString();
        }
    }
}
