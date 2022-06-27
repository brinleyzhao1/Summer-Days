using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
  [SerializeField] private GameObject[] day4Options;
  [SerializeField] private GameObject[] day3Options;
  [SerializeField] private GameObject[] day2Options;

  // [SerializeField] private GameObject[] day1Options;

  // Start is called before the first frame update
  void Start()
  {
    foreach (GameObject item in day4Options)
    {
      item.SetActive(false);
    }
    foreach (GameObject item in day3Options)
    {
      item.SetActive(false);
    }
    foreach (GameObject item in day2Options)
    {
      item.SetActive(false);
    }
  }

  // Update is called once per frame
  void Update()
  {
  }

  public void NewOptionsOnDay(int day)
  {
    if (day == 4)
    {
      foreach (GameObject item in day4Options)
      {
        item.SetActive(true);
      }
    }

    if (day == 3)
    {
      foreach (GameObject item in day3Options)
      {
        item.SetActive(true);
      }
    }

    if (day == 2)
    {
      foreach (GameObject item in day2Options)
      {
        item.SetActive(true);
      }
    }
  }
}
