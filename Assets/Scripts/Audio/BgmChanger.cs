using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
  public class BgmChanger : MonoBehaviour
  {
    [SerializeField] private AudioClip[] tracks = new AudioClip[6];


    private AudioSource audioSource;

    public void Start()
    {
      audioSource = GetComponent<AudioSource>();
    }

    public void PlayTrack(int trackNum)
    {
      audioSource.clip = tracks[trackNum];
      audioSource.Play();

    }
  }
}
