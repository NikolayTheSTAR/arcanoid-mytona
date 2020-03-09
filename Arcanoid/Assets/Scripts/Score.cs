using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс представляющий логику счёта
/// </summary>
public class Score : MonoBehaviour
{
    /// <summary>
	/// Текст для отображения счёта
	/// </summary>
    [SerializeField] private Text text_s;

    /// <summary>
	/// Поле для статического доступа к text_s
	/// </summary>
    private static Text text_s_static;

    /// <summary>
	/// Активный счёт
	/// </summary>
    public static int score = 0;
    

    void Start()
    {   
        text_s_static = text_s;
        UpdateText();
    }

    /// <summary>
	/// Пополнение счёта
	/// </summary>
	/// <remarks>
	/// Вызывается после уничтожения блока
	/// </remarks>
    public static void AddScore()
    {
        score++;
        UpdateText();
    }

    /// <summary>
	/// Удаление счёта
	/// </summary>
	/// <remarks>
	/// Вызывается после проигрыша
	/// </remarks>
    public static void RemoveScore()
    {
        score = 0;
        UpdateText();
    }

    /// <summary>
	/// Обновление текста text_s
	/// </summary>
    private static void UpdateText()
    {
        text_s_static.text = "счёт: " + Convert.ToString(score);
    }
}