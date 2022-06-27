using TMPro;
using UnityEngine;

public class Progress : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI dayLeftText;
  [SerializeField] private TextMeshProUGUI moodText;
  [SerializeField] private TextMeshProUGUI hwText;

  private int _daysLeft = 3;
  private int _hwProgress = 0; //percentage

  [Header("Ending")] [SerializeField] private GameObject endingPanel;
  [SerializeField] private TextMeshProUGUI hwResult;
  [SerializeField] private TextMeshProUGUI moodResult;


  public void UpdateDayLeftText(int num)
  {
    _daysLeft = num;
    dayLeftText.text = num + " Days left";
  }

  public void UpdateHw(int percentage)
  {
    _hwProgress += percentage;
    if (_hwProgress >= 100)
    {
      _hwProgress = 100;
      Status.Instance.UpdateFulfillment(20); //todo: worked?
    }

    hwText.text = "Hw: " + _hwProgress + "%";
  }

  public void UpdateMood(int energy, int hunger, int fulfillment, int boredom)
  {
    CalculateMood(energy, hunger, fulfillment, boredom);
    moodText.text = "Mood: " + moodText.text;
  }

  private void CalculateMood(int energy, int hunger, int fulfillment, int boredom)
  {
    // int plus = fulfillment + energy;
    // int minus = boredom + hunger;
    // moodText.text = (plus - minus).ToString();

    print("update mood");

    if (hunger < 10 && energy < 20)
    {
      moodText.text = "Dying";
    }
    else if (hunger < 20)
    {
      moodText.text = "Hungry";
    }
    else if (energy < 20)
    {
      moodText.text = "Tired";
    }
    else if (boredom > 30)
    {
      moodText.text = "Very Bored";
    }
    else if (fulfillment < 20)
    {
      moodText.text = "Unhappy";
    }
    else if (boredom > 20)
    {
      moodText.text = "Bored";
    }
    else if (_daysLeft < 3 && _hwProgress < 70)
    {
      moodText.text = "Stressed";
    }
    else if (fulfillment > 20 && boredom < 20)
    {
      moodText.text = "Happy";
    }
    else
    {
      moodText.text = "okay";
    }
  }

  public void Ending()
  {
    endingPanel.SetActive(true);

    // hwResult.text = _hwProgress;
    if (_hwProgress == 100)
    {
      hwResult.text = "successfully completed";
      hwResult.color = Color.green;
    }
    else
    {
      hwResult.text = "DID NOT complete";
      hwResult.color = Color.red;
    }

    moodResult.text = moodText.text.Split(':')[1];
  }
}
