using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] inputSources;
    [SerializeField] AudioSource newCourseSource;
    [SerializeField] AudioSource dmgSource;
    [SerializeField] AudioSource hungerSource;

    public void PlayInputSFX(AudioClip sfx)
    {
        foreach(AudioSource audioSource in inputSources)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sfx);
                break;
            }
        }
    }

    public void PlayCourseSFX(AudioClip sfx)
    {
        newCourseSource.PlayOneShot(sfx);
    }

    public void PlayDMGSFX(AudioClip sfx)
    {
        dmgSource.PlayOneShot(sfx);
    }

    public void PlayHungerSFX(AudioClip sfx)
    {
        hungerSource.PlayOneShot(sfx);
    }
}
