using UnityEngine;
using System.Collections;

public class SimpleAudioSource : MonoBehaviour
{
    public AudioSource sfx;
    public AudioClip Trumpets;
    public AudioClip Sheep;
    public AudioClip DragonSpitFire;
    public AudioClip DestroyedVillage;
    public AudioClip DeadDragon;

    public bool isSoundPlaying = false;


    public void PlaySound(string ID)
    {

        if (ID == "Trumpets")
        {
            sfx.PlayOneShot(Trumpets);
        }
        else if (ID == "Sheep")
        {
            sfx.PlayOneShot(Sheep);
        }
        else if (ID == "DragonSpitFire")
        {
            isSpittingFire();
        }
        else if (ID == "DestroyedVillage")
        {
            sfx.PlayOneShot(DestroyedVillage);
        }
        else if (ID == "DeadDragon")
        {
            sfx.PlayOneShot(DeadDragon);
        }

        else
        {
            Debug.LogError("Unknown audio ID" + ID);
        }


    }

    public void isSpittingFire()
    {
        if (isSoundPlaying)
        {
            return;  // Exit if a sound is already playing
        }
        else
        {
            StartCoroutine(ResetSoundPlaying(sfx.clip.length));
        }
    }
    private IEnumerator ResetSoundPlaying(float clipLength)
    {

        sfx.PlayOneShot(DragonSpitFire);
        yield return new WaitForSeconds(clipLength);
        isSoundPlaying = false;
    }
}

