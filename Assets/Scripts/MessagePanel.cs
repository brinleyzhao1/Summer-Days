using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessagePanel : MonoBehaviour
{


  [SerializeField] private TextMeshProUGUI messageText;

  public void UpdateText(string message)
  {
    messageText.text = message;
  }
}
