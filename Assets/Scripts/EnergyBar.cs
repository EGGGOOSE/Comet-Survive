using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour{
    
    private static Image EnergyBarImage;
    public float lerpSpeed = 1;

    private void Awake(){
        EnergyBarImage = GetComponent<Image>();
    }

    private void Update(){
        EnergyBarImage.fillAmount = Mathf.Lerp(EnergyBarImage.fillAmount, GameManager.Instance.Energy, lerpSpeed * Time.deltaTime);
        EnergyBarImage.color = Color.Lerp(new Color(205f/255f, 2f/255f, 1f), new Color(105f/255f, 182f/255f, 238f/255f), GameManager.Instance.Energy);
    }
}
