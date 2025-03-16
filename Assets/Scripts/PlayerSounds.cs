using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    List<AudioClip> footstepSound;

    [SerializeField]
    float minDelayOnWalk, maxDelayOnWalk, minDelayOnRun, maxDelayOnRun;

    float currentMinDelay, currentMaxDelay;

    float waitTime,elapsedTime;

    [SerializeField]
    float transitionSpeed;

    void Start()
    {
        currentMinDelay = minDelayOnWalk;
        currentMaxDelay = maxDelayOnWalk;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootstepSound()
    {
        waitTime = Random.Range(currentMinDelay,currentMaxDelay);

        elapsedTime += Time.deltaTime;

        if(elapsedTime >= waitTime)
        {
            AudioClip audioClip = footstepSound[Random.Range(0, footstepSound.Count)];
            audioSource.PlayOneShot(audioClip);

            elapsedTime = 0f;
        }

    }

    public void OnRunFootstepDelay()
    {
        currentMinDelay = Mathf.Lerp(currentMinDelay, minDelayOnRun, transitionSpeed);
        currentMaxDelay = Mathf.Lerp(currentMaxDelay, maxDelayOnRun, transitionSpeed);
    }

    public void OnWalkFootstepDelay()
    {
        currentMinDelay = Mathf.Lerp(currentMinDelay, minDelayOnWalk, transitionSpeed);
        currentMaxDelay = Mathf.Lerp(currentMaxDelay, maxDelayOnWalk, transitionSpeed);
    }


}
