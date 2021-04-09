using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXManager : MonoBehaviour
{
    public enum PlayerAudioKeys { RegSlash, SlashDash, SpinSlash }


    [SerializeField] AudioSource audioSource;

    // These 2 fields are just placeholders
    [SerializeField] List<PlayerAudioKeys> keys;
    [SerializeField] List<AudioClip> clips;

    // Dictionaries can't be serialized so we're gonna use 2 lists that get loaded into it
    Dictionary<PlayerAudioKeys, AudioClip> audioClips;

    // Start is called before the first frame update
    void Start()
    {
        audioClips = new Dictionary<PlayerAudioKeys, AudioClip>();

        // Get the audio source associated with the game object
        audioSource = GetComponent<AudioSource>();

        // Load all the audioClips into the dictionary
        for(int i = 0; i < keys.Count; i++)
        {
            audioClips.Add(keys[i], clips[i]);
        }
    }

    // Plays the associated sound file
    public void Play(PlayerAudioKeys key)
    {
        audioSource.PlayOneShot(audioClips[key]);
    }
}
