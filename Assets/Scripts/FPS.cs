using UnityEngine;


public class FPS : MonoBehaviour
{
    private float _updateInterval = 1f;// ������������� ��������� �������� ��� ���������� ������� ������ �� 1 �������  
    private float _accum = .0f;// ��������� �����  
    private int _frames = 0;// ������� ������ ���� �������� �� ����� _updateInterval  
    private float _timeLeft;
    public static string fpsFormat;

    void Start()
    {
        _timeLeft = _updateInterval;
    }


    void Update()
    {
        _timeLeft -= Time.deltaTime;
        //Time.timeScale ����� �������������� �������� ���������� Update � LateUpdate,  
        //Time.deltaTime �������������� � ��������, ����� �� ���������� ���������� �����  
        // �����, ����� �������� �����, �������������� � ��������������� �����  
        _accum += Time.timeScale / Time.deltaTime;
        ++_frames;// ����� �����  

        if (_timeLeft <= 0)
        {
            float fps = _accum / _frames;
            //Debug.Log(_accum + "__" + _frames);  
            fpsFormat = System.String.Format("{0:F2}FPS", fps);// ��������� ��� ���������� �����  
            //Debug.LogError(fpsFormat);

            _timeLeft = _updateInterval;
            _accum = .0f;
            _frames = 0;
        }
    }
}
