using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{

    [SerializeField] private AudioSource _footstepSource;
    [SerializeField] private AudioSource _landingSource;
    [SerializeField] private AudioSource _jumpingSource;

    public void PlayFootstep()
    {
        _footstepSource?.Play();
    }

    public void PlayLanding()
    {
        _landingSource?.Play();
    }

    public void PlayJumping()
    {
        _jumpingSource?.Play();
    }

}
