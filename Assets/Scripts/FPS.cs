using UnityEngine;


public class FPS : MonoBehaviour
{
    private float _updateInterval = 1f;// ”станавливаем временной интервал дл€ обновлени€ частоты кадров до 1 секунды  
    private float _accum = .0f;// —уммарное врем€  
    private int _frames = 0;// —колько кадров было запущено за врем€ _updateInterval  
    private float _timeLeft;
    public static string fpsFormat;

    void Start()
    {
        _timeLeft = _updateInterval;
    }


    void Update()
    {
        _timeLeft -= Time.deltaTime;
        //Time.timeScale может контролировать скорость выполнени€ Update и LateUpdate,  
        //Time.deltaTime рассчитываетс€ в секундах, врем€ до завершени€ последнего кадра  
        // ƒелим, чтобы получить врем€, использованное в соответствующем кадре  
        _accum += Time.timeScale / Time.deltaTime;
        ++_frames;// Ќомер кадра  

        if (_timeLeft <= 0)
        {
            float fps = _accum / _frames;
            //Debug.Log(_accum + "__" + _frames);  
            fpsFormat = System.String.Format("{0:F2}FPS", fps);// —охран€ем два дес€тичных знака  
            //Debug.LogError(fpsFormat);

            _timeLeft = _updateInterval;
            _accum = .0f;
            _frames = 0;
        }
    }
}
