using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WalkingSoundController : MonoBehaviour
{
    [Header("FMOD Settings")]
    [SerializeField] private FMODUnity.EventReference walkingSoundEvent;
    [SerializeField] private float footstepDelay = 0.3f;

    private Animator animator;
    private FMOD.Studio.EventInstance walkingSoundInstance;
    private Coroutine footstepCoroutine;
    private bool wasRunning = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        walkingSoundInstance = FMODUnity.RuntimeManager.CreateInstance(walkingSoundEvent);
    }

    private void Update()
    {
        bool isRunning = animator.GetBool("Running");

        // If running state changed
        if (isRunning != wasRunning)
        {
            if (isRunning)
            {
                // Start playing footsteps
                footstepCoroutine = StartCoroutine(PlayFootsteps());
            }
            else
            {
                // Stop playing footsteps
                if (footstepCoroutine != null)
                {
                    StopCoroutine(footstepCoroutine);
                    footstepCoroutine = null;
                }
                walkingSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }

            wasRunning = isRunning;
        }
    }

    private IEnumerator PlayFootsteps()
    {
        while (true)
        {
            // Play the footstep sound
            walkingSoundInstance.start();

            // Wait for the next footstep
            yield return new WaitForSeconds(footstepDelay);
        }
    }

    private void OnDestroy()
    {
        // Clean up FMOD instance when object is destroyed
        walkingSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        walkingSoundInstance.release();
    }
}