using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text text;

    public static int money;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
            money = PlayerPrefs.GetInt("Money", Money.money);
        else
            money = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        text.text = " Money: " + money;
    }
}
