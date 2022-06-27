using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackholeInfo : MonoBehaviour
{
    public Text text;
    public GameObject blackHole;
    private Blackhole BlackHole;

    private void Start()
    {
        BlackHole = blackHole.GetComponent<Blackhole>();
    }

    // Update is called once per frame
    private void Update()
    {
        text.text = "Blackhole speed: " + string.Format("{0:F2}", BlackHole.speed) + " \n"+ "Blackhole distance: " + (int)( GameManager.Instance.ball.transform.position.y - blackHole.transform.position.y) + " \n" + FPS.fpsFormat + " ";
    }
}

