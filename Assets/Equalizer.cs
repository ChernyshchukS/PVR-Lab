using UnityEngine;

public class Equalizer : MonoBehaviour
{
    public GameObject cubePrefab; // ������ �� ������ ����
    public int numberOfCubes = 64; // ���������� �����
    public float scaleMultiplier = 10.0f; // ��������� ��������
    public float maxScale = 20.0f; // ������������ �������
    public float audioSensitivity = 5.0f; // ���������������� � �����
    public float cubSize = 50.0f; // ���������� ����� ������

    public Vector3 startPosition = new Vector3(0, 0, 0); // ��������� ������� �����

    private GameObject[] cubes;

    void Start()
    {
        cubes = new GameObject[numberOfCubes];

        // �������� � ����������� �����
        for (int i = 0; i < numberOfCubes; i++)
        {
            cubes[i] = Instantiate(cubePrefab, transform);

            // ���������������� ����� ������������ ��������� �������
            cubes[i].transform.position = startPosition + new Vector3(i * cubSize, 0, 0);
        }
    }

    void Update()
    {
        // ��������� ������� �����
        float[] spectrumData = new float[numberOfCubes];
        AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        // ���������� �������� �����
        for (int i = 0; i < numberOfCubes; i++)
        {
            float scale = spectrumData[i] * scaleMultiplier * audioSensitivity;
            scale = Mathf.Clamp(scale, 0.1f, maxScale); // ����������� ��������

            cubes[i].transform.localScale = new Vector3(1, scale, 1);
        }
    }
}