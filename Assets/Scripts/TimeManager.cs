using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
  [SerializeField] private int totalDays;
  private int _hour;
  private int _minute;
  private float _second;
  [SerializeField] private int idleTimeMultiplier = 30;
  [SerializeField] private int fastTimeMultiplier = 3;


  [SerializeField] private TextMeshProUGUI timeText;
  // Start is called before the first frame update

  [SerializeField] private Progress progress;

  OptionsManager optionsManager;


  private void Start()
  {
    if (progress == null)
    {
      Debug.LogError("progress not assigned");
    }

    progress.UpdateDayLeftText(totalDays );

    optionsManager = FindObjectOfType<OptionsManager>();
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.K))
    {
      TimeOver();
    }

    CalculateTime();
  }

  private void UpdateTimeText()
  {
    timeText.text = _hour + ":" + _minute;
  }

  public void AddHour(int num)
  {
    _hour += num;
    UpdateTimeText();
  }


  private void CalculateTime()
  {
    _second += Time.deltaTime * idleTimeMultiplier;

    if (_second >= 60)
    {
      _minute++;
      _second = 0;
      UpdateTimeText();
    }
    else if (_minute >= 60)
    {
      _hour++;
      _minute = 0;
      UpdateTimeText();
    }
    else if (_hour >= 24)
    {
      totalDays -= 1;
      _hour -= 24;
      UpdateTimeText();
      progress.UpdateDayLeftText(totalDays );

      optionsManager.NewOptionsOnDay(totalDays);

      if (totalDays <= 0)
      {
        TimeOver();
      }
    }
  }

  private void TimeOver()
  {
    progress.Ending();
  }

  public void FastForward()
  {

    Time.timeScale *= fastTimeMultiplier;
    print("fast time "+Time.timeScale);
  }
  public void NormalTimeScale()
  {
    Time.timeScale = 1;
  }
}
