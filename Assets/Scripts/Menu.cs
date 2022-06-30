using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour{

    private const int ImageMusicOn = 0;
    private const int ImageMusicOff = 1;
    private const int ImageSoundOn = 2;
    private const int ImageSoundOff = 3;
    private const int ImageLightsOn = 4;
    private const int ImageLightsOff = 5;

    public GameObject mainMenu;
    public Text bestHeight, money;

    public GameObject upgrades;
    
    public GameObject settings;
    public GameObject musicButton, soundButton, lightButton;

    public AudioSource soundSource;

    public Sprite[] images = new Sprite[6];

    private void Start(){
        bool allow = PlayerPrefs.GetInt("AllowMusic", 1) == 1;
        musicButton.GetComponent<Image>().sprite = images[allow ? ImageMusicOn : ImageMusicOff];
        
        allow = PlayerPrefs.GetInt("AllowSound", 1) == 1;
        soundButton.GetComponent<Image>().sprite = images[allow ? ImageSoundOn : ImageSoundOff];
        soundSource.mute = PlayerPrefs.GetInt("AllowSound", 1) == 0;

        allow = PlayerPrefs.GetInt("AllowLights", 0) == 1;
        lightButton.GetComponent<Image>().sprite = images[allow ? ImageLightsOn : ImageLightsOff];

        bestHeight.text = "Best Height: " + PlayerPrefs.GetInt("BestHeight", 0);
        money.text = PlayerPrefs.GetInt("Money", 0).ToString();
    }

    public void StartGame(){
        soundSource.PlayOneShot(soundSource.clip);
        SceneManager.LoadScene(1);
    }

    public void MainMenu(){
        upgrades.SetActive(false);
        settings.SetActive(false);
        mainMenu.SetActive(true);
        soundSource.PlayOneShot(soundSource.clip);
    }

    public void OpenUpgrades(){
        mainMenu.SetActive(false);
        upgrades.SetActive(true);
        soundSource.PlayOneShot(soundSource.clip);
    }

    public void OpenSettings(){
        mainMenu.SetActive(false);
        settings.SetActive(true);
        soundSource.PlayOneShot(soundSource.clip);
    }

    public void SettingsMusic(){
        bool allow = PlayerPrefs.GetInt("AllowMusic", 1) == 1;
        PlayerPrefs.SetInt("AllowMusic", allow ? 0 : 1);

        musicButton.GetComponent<Image>().sprite = images[!allow ? ImageMusicOn : ImageMusicOff];
        soundSource.PlayOneShot(soundSource.clip);
    }

    public void SettingsSound(){
        bool allow = PlayerPrefs.GetInt("AllowSound", 1) == 1;
        PlayerPrefs.SetInt("AllowSound", allow ? 0 : 1);
        
        soundButton.GetComponent<Image>().sprite = images[!allow ? ImageSoundOn : ImageSoundOff];
        soundSource.mute = allow;
        soundSource.PlayOneShot(soundSource.clip);
    }

    public void SettingsLights(){
        bool allow = PlayerPrefs.GetInt("AllowLights", 0) == 1;
        PlayerPrefs.SetInt("AllowLights", allow ? 0 : 1);
        
        lightButton.GetComponent<Image>().sprite = images[!allow ? ImageLightsOn : ImageLightsOff];
        soundSource.PlayOneShot(soundSource.clip);
    }
}