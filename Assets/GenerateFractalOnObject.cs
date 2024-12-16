using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFractalOnObject : MonoBehaviour
{
    public int textureWidth = 512;
    public int textureHeight = 512;
    public float zoom = 1.5f;
    public Vector2 offset = new Vector2(0, 0);
    public Gradient colorGradient; // Для раскраски фрактала

    void Start()
    {
        // Создаём текстуру для фрактала
        Texture2D fractalTexture = new Texture2D(textureWidth, textureHeight);

        // Генерация фрактала
        for (int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                // Перевод координат пикселя в комплексное пространство
                float complexX = Map(x, 0, textureWidth, -zoom, zoom) + offset.x;
                float complexY = Map(y, 0, textureHeight, -zoom, zoom) + offset.y;

                int iterations = MandelbrotIterations(new Complex(complexX, complexY));

                // Применяем цветовую градацию
                float t = Mathf.Sqrt((float)iterations / 100.0f); // Плавное распределение цвета
                Color color = colorGradient.Evaluate(t);

                fractalTexture.SetPixel(x, y, color);
            }
        }

        // Применяем текстуру
        fractalTexture.Apply();
        GetComponent<Renderer>().material.mainTexture = fractalTexture;
    }

    float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }

    int MandelbrotIterations(Complex c)
    {
        Complex z = Complex.Zero;
        int iterations = 0;

        // Главный цикл вычислений для фрактала Мандельброта
        while (z.MagnitudeSquared() < 4.0 && iterations < 100)
        {
            z = z * z + c;
            iterations++;
        }

        return iterations;
    }
}

public struct Complex
{
    public float Real;
    public float Imaginary;

    public Complex(float real, float imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public static Complex Zero { get { return new Complex(0, 0); } }

    public static Complex operator *(Complex a, Complex b)
    {
        return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
    }

    public static Complex operator +(Complex a, Complex b)
    {
        return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    public float MagnitudeSquared()
    {
        return Real * Real + Imaginary * Imaginary;
    }
}