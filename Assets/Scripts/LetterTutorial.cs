using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Panel").gameObject.SetActive(true);
        // Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.anyKey)
      {
        transform.Find("Panel").gameObject.SetActive(false);
        Time.timeScale = 1;

        this.enabled = false;
      }

    }
}
