using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
  private AudioSource _audioSource;

  [SerializeField] private AudioClip success;
  [SerializeField] private AudioClip error;

  private void Start()
  {
    _audioSource = GetComponent<AudioSource>();
  }

  // Start is called before the first frame update
  public void PlaySuccessSFX()
  {
    _audioSource.PlayOneShot(success);
  }public void PlayError()
  {
    _audioSource.PlayOneShot(error);
  }
}
