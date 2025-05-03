using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovableObjectMoveDetector : MonoBehaviour
{
    [Header("Movement Detection")]
    [SerializeField] private Transform target;  // The player/object that can move this

    [Header("FMOD Sound")]
    [SerializeField] private FMODUnity.EventReference movingSoundEvent;

    public bool Moving { get; private set; }
    private FMOD.Studio.EventInstance movingSoundInstance;

    private void Awake()
    {
        movingSoundInstance = FMODUnity.RuntimeManager.CreateInstance(movingSoundEvent);
        Moving = false;
    }

    private void Update()
    {
        if (Moving)
        {
            PlaySound();
        }
        else
        {
            StopSound();
        }
    }

    private void PlaySound()
    {
        FMOD.Studio.PLAYBACK_STATE playbackState;
        movingSoundInstance.getPlaybackState(out playbackState);

        if (playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            movingSoundInstance.start();
        }
    }

    private void StopSound()
    {
        FMOD.Studio.PLAYBACK_STATE playbackState;
        movingSoundInstance.getPlaybackState(out playbackState);

        if (playbackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            movingSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Moving && collision.transform == target)
        {
            Moving = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (Moving && collision.transform == target)
        {
            Moving = false;
        }
    }

    private void OnDestroy()
    {
        movingSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        movingSoundInstance.release();
    }
}