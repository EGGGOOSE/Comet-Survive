using System;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour{
    
    public Text text;
    public static int money;

    private void Start(){
        money = 0;
    }

    private void Update(){
        text.text = " Money: " + money;
    }
}
