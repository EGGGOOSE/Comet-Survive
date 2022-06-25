using UnityEngine;


public class FPS : MonoBehaviour
{
    float _updateInterval = 1f;// ������������� ��������� �������� ��� ���������� ������� ������ �� 1 �������  
    float _accum = .0f;// ��������� �����  
    int _frames = 0;// ������� ������ ���� �������� �� ����� _updateInterval  
    float _timeLeft;
    string fpsFormat;

    void Start()
    {
        _timeLeft = _updateInterval;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(50, 50, 250, 250), fpsFormat);
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
