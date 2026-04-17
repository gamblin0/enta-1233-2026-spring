using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    [SerializeField] private AudioMgr.MusicTypes _Song;

    private void Start()
    {
        AudioMgr.Instance.PlayMusic(_Song, 1);
    }
}
