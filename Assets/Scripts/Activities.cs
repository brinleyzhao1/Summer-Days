using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Activities : MonoBehaviour
{
  [SerializeField] private TimeManager timeManager;

  [SerializeField] private Status status;
  [SerializeField] private Progress progress;

  [SerializeField] private MessagePanel messagePanel;

  [SerializeField] private SFX sfx;
  private bool inMiddleOfOneActivity = false;

  private Image currentProgressBar;


  [Header("Expandable Options")] [SerializeField]
  private GameObject eatOptions;

  [SerializeField] private GameObject sleepOptions;


  [Header("Time, Energy, Hunger, Fulfillment, Boredom, HW")]
  public int[] readingEffect = new int[6] {40, -2, -5, 5, -5, 0};
  public int[] potionEffect = new int[6] {30, -4, -4, -2, 5, 4};
  public int[] napEffect = new int[6] {30, 10, -1, -3, 1, 0};
  public int[] sleepEffect = new int[6] {300, 60, -3, -5, 1, 0};
  public int[] studyEffect = new int[6] {50, -7, -4, -4, 5, 7};
  public int[] snackEffect = new int[6] {10, 1, 20, 1, -1, 0};
  public int[] mealEffect = new int[6] {20, 1, 40, -1, -1, 0};
  public int[] musicEffect = new int[6] {20, -2, -2, 2, -3, 0};
  public int[] walkEffect = new int[6] {30, -4, -4, 2, -2, 0};



  private void Start()
  {
    CloseAllOptions();

    if (timeManager == null)
    {
      Debug.LogError("timeManager not assigned");
    }

    if (progress == null)
    {
      Debug.LogError("progress not assigned");
    }

    if (messagePanel == null)
    {
      Debug.LogError("messagePanel not assigned");
    }
  }

  private void CloseAllOptions()
  {
    eatOptions.SetActive(false);
    sleepOptions.SetActive(false);
  }

  //potential activities: write letter to friends; work to earn money; facetime friend via globe
  public void BtnSleep()
  {
    if (IfTooHungry()) return;
    if (IfInMiddleOfOtherActivity()) return;

    currentProgressBar = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
    CloseAllOptions();

    sleepOptions.SetActive(true);
  }

  #region Sleep Options

  public void BtnNap()
  {
    if (IfTooHungry()) return;
    if (IfInMiddleOfOtherActivity()) return;

    StartCoroutine(UpdateStatusAndProgress(napEffect));
  }

  public void BtnLongSleep()
  {
    if (IfTooHungry()) return;
    if (IfInMiddleOfOtherActivity()) return;

    StartCoroutine(UpdateStatusAndProgress(sleepEffect));
  }

  #endregion


  public void BtnEat()
  {
    if (IfInMiddleOfOtherActivity()) return;

    currentProgressBar = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
    CloseAllOptions();

    eatOptions.SetActive(true);

    // StartCoroutine(UpdateStatusAndProgress(20, 1, 25, -1, -1, 0));
  }

  #region eating options

  public void BtnEatSnack()
  {
    if (IfInMiddleOfOtherActivity()) return;


    StartCoroutine(UpdateStatusAndProgress(snackEffect));
  }

  public void BtnEatMeal()
  {
    if (IfInMiddleOfOtherActivity()) return;


    StartCoroutine(UpdateStatusAndProgress(mealEffect));
  }

  #endregion


  public void BtnRead()
  {
    if (IfInMiddleOfOtherActivity()) return;
    if (IfTooHungry()) return;
    if (IfTooTired()) return;

    currentProgressBar = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>();

    StartCoroutine(UpdateStatusAndProgress(readingEffect));
    // StartCoroutine(UpdateStatusAndProgress(40, -2, -5, 5, -5, 0));
  }

  public void BtnMusic()
  {
    if (IfInMiddleOfOtherActivity()) return;
    if (IfTooHungry()) return;
    if (IfTooTired()) return;

    currentProgressBar = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
    StartCoroutine(UpdateStatusAndProgress(musicEffect));
  }

  public void BtnWalk()
  {
    if (IfInMiddleOfOtherActivity()) return;
    if (IfTooHungry()) return;
    if (IfTooTired()) return;

    currentProgressBar = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
    StartCoroutine(UpdateStatusAndProgress(walkEffect));
  }

  public void BtnPotion()
  {
    if (IfInMiddleOfOtherActivity()) return;
    if (IfTooHungry()) return;
    if (IfTooTired()) return;
    if (IfTooBored()) return;

    currentProgressBar = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
    StartCoroutine(UpdateStatusAndProgress(potionEffect));
  }

  public void BtnStudy()
  {
    if (IfInMiddleOfOtherActivity()) return;
    if (IfTooHungry()) return;
    if (IfTooTired()) return;
    if (IfTooBored()) return;

    currentProgressBar = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
    StartCoroutine(UpdateStatusAndProgress(studyEffect));
  }


  #region PrivateFunctions

  private bool IfTooHungry()
  {
    if (status._tooHungry)
    {
      messagePanel.gameObject.SetActive(true);
      messagePanel.UpdateText("hungryyyyy");
      return true;
    }

    return false;
  }

  private bool IfInMiddleOfOtherActivity()
  {
    if (inMiddleOfOneActivity)
    {
      sfx.PlayError();
      // messagePanel.gameObject.SetActive(true);
      // messagePanel.UpdateText("hungryyyyy");
      return true;
    }

    return false;
  }

  private bool IfTooTired()
  {
    if (status.energyLevel == Status.EnergyLevel.NoEnergy)
    {
      messagePanel.gameObject.SetActive(true);
      messagePanel.UpdateText("tireddddd");
      return true;
    }

    return false;
  }

  private bool IfTooBored()
  {
    if (status.tooBored)
    {
      messagePanel.gameObject.SetActive(true);
      messagePanel.UpdateText("I need a break!!");
      return true;
    }

    return false;
  }


  private IEnumerator UpdateStatusAndProgress( int[] arr)
  {
    // int time, int energy, int hunger, int fulfillment, int boredom, int hw
    int time = arr[0];
    int energy = arr[1];
    int hunger = arr[2];
    int fulfillment = arr[3];
    int boredom = arr[4];
    int hw = arr[5];

    CloseAllOptions();

    // StartCoroutine(Countdown(time));
    yield return Countdown(time);
    status.UpdateHunger(hunger);
    status.UpdateEnergy(energy);

    status.UpdateFulfillment(fulfillment);
    status.UpdateBoredom(boredom);
    progress.UpdateHw(hw);


    // status.UpdateChangeText(energy, hunger, fulfillment, boredom);

    progress.UpdateMood(status.energy, status.hunger, status.fulfillment, status.boredom);
  }

  // private IEnumerator Wait(int time)
  // {
  //   yield return Countdown(time);
  // }

  private IEnumerator Countdown(int time)
  {
    inMiddleOfOneActivity = true;

    // time = time - 1;
    timeManager.FastForward();
    float timeLeft = time;
    while (timeLeft > 0)
    {
      currentProgressBar.fillAmount = 1 - timeLeft / time;
      yield return new WaitForSeconds(0.1f);
      timeLeft -= 0.1f;
      // currentButton.fillAmount = 1 - timeLeft / time;

      // timeManager.AddHour(1);
    }

    timeManager.NormalTimeScale();
    sfx.PlaySuccessSFX();
    inMiddleOfOneActivity = false;
  }

  #endregion
}
