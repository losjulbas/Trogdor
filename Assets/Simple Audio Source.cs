using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SimpleAudioSource : MonoBehaviour
{
    public AudioSource sfx;
    public AudioSource thirdAudiosource;
    public AudioClip Trumpets;
    public AudioClip Sheep;
    public AudioClip DragonSpitFire;
    public AudioClip DestroyedVillage;
    public AudioClip DeadDragon;
    public AudioClip CastleBell;
    public AudioClip ArrowTower;
    public AudioClip WizardCasting;
    public AudioClip ButtonClick;

    public bool isSoundPlaying = false;


    public void PlaySound(string ID)
    {


        if (ID == "Sheep")
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
        else if (ID == "CastleBell")
        {
            sfx.PlayOneShot(CastleBell);
        }
        else if (ID == "ArrowTower")
        {
            sfx.PlayOneShot(ArrowTower);
        }
        else if (ID == "WizardCasting")
        {
            sfx.PlayOneShot(WizardCasting);
        }
        else
        {
            Debug.LogError("Unknown audio ID" + ID);
        }
    }

    public void PlayEndGameSounds(string ID)
    {

        if (ID == "Trumpets")
        {
            thirdAudiosource.PlayOneShot(Trumpets);
        }
        else if (ID == "DeadDragon")
        {
            thirdAudiosource.PlayOneShot(DeadDragon);
        }
        else if (ID == "ButtonClick")
        {
            Debug.Log("Playing ButtonClick sound");
            thirdAudiosource.PlayOneShot(ButtonClick);
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

