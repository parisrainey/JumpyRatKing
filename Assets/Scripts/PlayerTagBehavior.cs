using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTagBehavior : MonoBehaviour
{
    [SerializeField]
    private bool _isTagged = false;
    [SerializeField]
    private ParticleSystem _particleSystem;

    public UnityEvent OnTagged;
    private bool _canBeTagged = true;

    public bool IsTagged { get => _isTagged; }

    public bool Tag()
    {
        //If cannot be tagged, return false
        if (!_canBeTagged) return false;

        //Set that we're tagged
        _isTagged = true;
        _canBeTagged = false;

        //Play Particle System
        _particleSystem.Play();

        //Turn our trail renderer on
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail != null)
            trail.enabled = true;

        OnTagged.Invoke();
        return true;
    }

    private void SetCanBeTagged()
    {
        _canBeTagged = true;
    }
    private void Start()
    {
        //Get my trail renderer
        TrailRenderer trail = GetComponent<TrailRenderer>();

        //If I am tagged, turn tail on, otherwise off
        if (IsTagged)
            trail.enabled = true;
        else
            trail.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If not it, do nothing
        if (!IsTagged) return;

        //Attempt to get PlayerTagBehavior from what we hit
        PlayerTagBehavior tagBehavior = collision.gameObject.GetComponent<PlayerTagBehavior>();

        //If it didn't have one, return
        if (tagBehavior == null) return;

        //if Tag() is false return, otherwise Tag the other Player
        if (!tagBehavior.Tag()) return;
        
        //Set ourselves as not it
        _isTagged = false;
        _canBeTagged = false;

        //Turn off trail if tagged
        TrailRenderer trailRenderer = GetComponent<TrailRenderer>();
        if(trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Wait limited time before function can be called
        Invoke("SetCanBeTagged", 0.5f);
    }
}
