using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
  #region Singleton

  public static Status Instance { get; private set; }


  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(this.gameObject);
    }
    else
    {
      Instance = this;
    }
  }

  #endregion

  public int energy = 10;
  [SerializeField] private TextMeshProUGUI energyText;

  public int hunger = 10;
  [SerializeField] private TextMeshProUGUI hungerText;

  public int fulfillment = 10;
  [SerializeField] private TextMeshProUGUI fulfillmentText;

  public int boredom = 10;
  [SerializeField] private TextMeshProUGUI boredomText;

  [SerializeField] private Progress progress;

  public bool _tooHungry = false;
  public bool tooBored = false;

  public EnergyLevel energyLevel = EnergyLevel.High;

  [SerializeField] private MessagePanel messagePanel;

  [SerializeField] private GameObject expandButton;

  [Header("Feedback Text")] [SerializeField]
  private TextMeshProUGUI energyChangeText;

  [SerializeField] private TextMeshProUGUI hungerChangeText;

  [SerializeField] private TextMeshProUGUI fulfillmentChangeText;
  [SerializeField] private TextMeshProUGUI boredomChangeText;

  public enum EnergyLevel
  {
    NoEnergy,
    Low,
    Medium,
    High
  }

  private void Start()
  {
    UpdateEnergy(0);
    UpdateHunger(0);
    UpdateFulfillment(0);
    UpdateBoredom(0);

    energyChangeText.text = "";
    hungerChangeText.text = "";
    fulfillmentChangeText.text = "";
    boredomChangeText.text = "";

    if (progress == null)
    {
      Debug.LogError("progress not assigned");
    }
  }


  public void UpdateEnergy(int addition)
  {

    // print(1);
    energy += addition;

    if (energy < 0)
    {
      energy = 0;
    }
    else if (0 <= energy & energy < 10)
    {
      energyLevel = EnergyLevel.NoEnergy;
      energyText.color = Color.red;
    }
    else if (10 <= energy & energy < 20)
    {
      energyLevel = EnergyLevel.Low;
      energyText.color = Color.white;
    }
    else if (20 <= energy & energy < 30)
    {
      energyLevel = EnergyLevel.Medium;
      energyText.color = Color.white;
    }
    else if (30 <= energy)
    {
      energyLevel = EnergyLevel.High;
      energyText.color = Color.white;
    }
    else if (energy >= 100)
    {
      energy = 100;
    }

    energyText.text = "energy: " + energy;

    energyChangeText.text = AddSignToText(addition);
    StartCoroutine(DelayedResetChangeTexts());

  }

  public void UpdateHunger(int num)
  {
    hunger += num;

    if (hunger <= 0)
    {
      hunger = 0;
    }
    else if (hunger < 10)
    {
      hungerText.color = Color.red;

      energy /= 2;
      UpdateEnergy(0);

      _tooHungry = true;
    }
    else if (hunger >= 100)
    {
      hunger = 100;
    }
    else
    {
      hungerText.color = Color.white;
      _tooHungry = false;
    }
    // print("hunger add "+ num);
    // print("hunger updated to "+hunger);

    hungerText.text = "hunger: " + hunger;


    hungerChangeText.text = AddSignToText(num);
    StartCoroutine(DelayedResetChangeTexts());
  }


  public void UpdateFulfillment(int num)
  {
    fulfillment += num;

    if (fulfillment < 0)
    {
      fulfillment = 0;
    }

    fulfillmentText.text = "fulfillment: " + fulfillment;


    fulfillmentChangeText.text = AddSignToText(num);
    StartCoroutine(DelayedResetChangeTexts());
  }

  public void UpdateBoredom(int num)
  {
    boredom += num;

    if (boredom < 0)
    {
      boredom = 0;
    }

    if (boredom >= 20)
    {
      boredomText.color = Color.red;
      tooBored = true;
    }
    else if (boredom < 20)
    {
      boredomText.color = Color.white;
      tooBored = false;
    }

    boredomText.text = "boredom: " + boredom;


    boredomChangeText.text = AddSignToText(num);
    StartCoroutine(DelayedResetChangeTexts());
  }

  // public void UpdateChangeText(int energyChange, int hungerChange, int fulfillmentChange, int boredomChange)
  // {
  //   StartCoroutine(FlashChangeText(energyChange, hungerChange, fulfillmentChange, boredomChange));
  // }
  //
  // IEnumerator FlashChangeText(int energyChange, int hungerChange, int fulfillmentChange, int boredomChange)
  // {
  //   energyChangeText.text = energyChange.ToString();
  //   hungerChangeText.text = hungerChange.ToString();
  //   fulfillmentChangeText.text = fulfillmentChange.ToString();
  //   boredomChangeText.text = boredomChange.ToString();
  //
  //   yield return new WaitForSeconds(1f);
  //
  //   DelayedResetChangeTexts();
  // }

  IEnumerator DelayedResetChangeTexts()
  {
    yield return new WaitForSeconds(1f);
    energyChangeText.text = "";
    hungerChangeText.text = "";
    fulfillmentChangeText.text = "";
    boredomChangeText.text = "";
  }

  private string AddSignToText(int addition)
  {
    if (addition > 0)
    {
      return "+" + addition;
    }

    if (addition < 0)
    {
      return addition.ToString();
    }

    // if (addition == 0)
    // {
    //   return 0.ToString();
    // }

    return "aaa";
  }

  public void  BtnCollapse()
  {
    for (int i = 0; i < transform.childCount; i++)
    {
      transform.GetChild(i).gameObject.SetActive(false);
    }
    expandButton.SetActive(true);

  }

  public void BtnExpand()
  {
    for (int i = 0; i < transform.childCount ; i++)
    {
      transform.GetChild(i).gameObject.SetActive(true);
    }
    expandButton.SetActive(false);
  }
}
