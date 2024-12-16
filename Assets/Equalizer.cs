using UnityEngine;

public class Equalizer : MonoBehaviour
{
    public GameObject cubePrefab; // Ссылка на префаб куба
    public int numberOfCubes = 64; // Количество кубов
    public float scaleMultiplier = 10.0f; // Множитель масштаба
    public float maxScale = 20.0f; // Максимальный масштаб
    public float audioSensitivity = 5.0f; // Чувствительность к аудио
    public float cubSize = 50.0f; // Расстояние между кубами

    public Vector3 startPosition = new Vector3(0, 0, 0); // Начальная позиция кубов

    private GameObject[] cubes;

    void Start()
    {
        cubes = new GameObject[numberOfCubes];

        // Создание и расстановка кубов
        for (int i = 0; i < numberOfCubes; i++)
        {
            cubes[i] = Instantiate(cubePrefab, transform);

            // Позиционирование кубов относительно начальной позиции
            cubes[i].transform.position = startPosition + new Vector3(i * cubSize, 0, 0);
        }
    }

    void Update()
    {
        // Получение спектра аудио
        float[] spectrumData = new float[numberOfCubes];
        AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        // Обновление масштаба кубов
        for (int i = 0; i < numberOfCubes; i++)
        {
            float scale = spectrumData[i] * scaleMultiplier * audioSensitivity;
            scale = Mathf.Clamp(scale, 0.1f, maxScale); // Ограничение масштаба

            cubes[i].transform.localScale = new Vector3(1, scale, 1);
        }
    }
}