using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    private static Image EnergyBarImage;
    public float lerpSpeed = 1;

    private void Awake()
    {
        EnergyBarImage = GetComponent<Image>();
    }

    private void Update()
    {
        EnergyBarImage.fillAmount = Mathf.Lerp(EnergyBarImage.fillAmount, GameManager.Instance.Energy, lerpSpeed * Time.deltaTime);
        EnergyBarImage.color = Color.Lerp(Color.red, Color.green, GameManager.Instance.Energy);

    }
}