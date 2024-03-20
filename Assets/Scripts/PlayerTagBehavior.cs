using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTagBehavior : MonoBehaviour
{
    [SerializeField]
    private bool _isTagged = false;
    private bool _canBeTagged = true;

    public bool IsTagged { get => _isTagged; }

    public bool Tag()
    {
        //If cannot be tagged, return false
        if (!_canBeTagged) return false;

        //Set that we're tagged
        _isTagged = true;
        _canBeTagged = false;

        //Turn our trail renderer on
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail != null)
            trail.enabled = true;

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
        if (trail == null)
            trail.enabled = true;

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
        if(TryGetComponent(out TrailRenderer trail))
        {
            trail.enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Invoke("SetCanBeTagged", 0.5f);
    }
}
