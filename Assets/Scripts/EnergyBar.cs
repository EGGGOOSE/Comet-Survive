using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    private static Image EnergyBarImage;

    public static void SetValue(float value)
    {
        EnergyBarImage.fillAmount = value;
        if (EnergyBarImage.fillAmount < 0.2f)
        {
            EnergyBarImage.color = Color.red;
        }
        else if (EnergyBarImage.fillAmount < 0.4f)
        {
            EnergyBarImage.color = Color.yellow;
        }
        else
        {
            EnergyBarImage.color = Color.green;
        }
    }

    public static float GetValue()
    {
        return EnergyBarImage.fillAmount;
    }

    private void Start()
    {
        EnergyBarImage = GetComponent<Image>();
    }
}