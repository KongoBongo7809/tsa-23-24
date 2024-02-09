using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinCounter : MonoBehaviour
{
    public Text coinText;

    // Update is called once per frame
    void Update()
    {
        coinText.text = "" + (SceneManager.GetActiveScene().buildIndex - 2) * 100;
    }
}
