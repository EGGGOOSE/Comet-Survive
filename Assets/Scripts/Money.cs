using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text text;

    public static int money = 0;

    // Update is called once per frame
    private void Update()
    {
        text.text = " Money: " + money + " Combo: " + GameManager.Instance.ball.combo;
    }
}
