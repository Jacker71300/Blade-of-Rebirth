using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] GameObject player;

    //Ability stuff
    float ability1Cooldown;
    float ability2Cooldown;
    public Text ability1Text;
    public string ability1Bind;
    public Text ability2Text;
    public string ability2Bind;

    //Objective stuff
    public string currentObjective;
    public Text objectiveText;

    //Waypoint stuff
    public GameObject waypointMarker;
    public RawImage markerImage;
    public Text distanceText;
    public GameObject targetObject;
    public Vector3 waypointOffset;
    public bool waypointActive;

    // Start is called before the first frame update
    void Start()
    {
        ability1Cooldown = player.GetComponent<SlashSpin>().GetCoolDownRemaining();
        ability2Cooldown = player.GetComponent<SlashDash>().GetCoolDownRemaining();
        if (currentObjective == string.Empty)
            currentObjective = "No objective";

        waypointActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        ability1Cooldown = player.GetComponent<SlashSpin>().GetCoolDownRemaining();
        ability2Cooldown = player.GetComponent<SlashDash>().GetCoolDownRemaining();

        if(ability1Cooldown == 0)
        {
            ability1Text.text = ability1Bind;
        }
        else
        {
            ability1Text.text = ((int)ability1Cooldown + 1).ToString();
        }

        if(ability2Cooldown == 0)
        {
            ability2Text.text = ability2Bind;
        }
        else
        {
            ability2Text.text = ((int)ability2Cooldown + 1).ToString();
        }

        objectiveText.text = "- " + currentObjective;

        float minX = markerImage.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = markerImage.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        if (targetObject != null)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(targetObject.transform.position) + waypointOffset;

            if (Vector3.Dot((targetObject.transform.position - player.transform.position).normalized, Camera.main.transform.forward) < 0)
            {
                //Target is behind player
                if (pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            waypointMarker.transform.position = pos;

            distanceText.text = ((int)Vector3.Distance(targetObject.transform.position, player.transform.position)).ToString() + "m";

        }
        else
        {
            waypointActive = false;
        }

        waypointMarker.gameObject.SetActive(waypointActive);
    }
}
