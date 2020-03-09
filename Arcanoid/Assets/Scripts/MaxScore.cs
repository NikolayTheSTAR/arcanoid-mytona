using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс представляющий логику рекордного счёта
/// </summary>
public class MaxScore : Score
{
    /// <summary>
	/// Текст для отображения рекордного счёта
	/// </summary>
    [SerializeField] private Text text_ms;

    /// <summary>
	/// Поле для статического доступа к text_ms
	/// </summary>
    private static Text text_ms_static;

    /// <summary>
	/// Рекордный счёт
	/// </summary>
	/// <remarks>
	/// Обновляется после кажного поражения
	/// </remarks>
    private static int maxScore = 0;
    

    void Start()
    {
        if (PlayerPrefs.HasKey("MaxScore"))
        {
            maxScore = PlayerPrefs.GetInt("MaxScore");
        }
        
        text_ms_static = text_ms;

        UpdateMaxScore();
        UpdateText();
    }

    /// <summary>
	/// Обновление рекордного счёта
	/// </summary>
	/// <remarks>
	/// Вызывается после кажного поражения
	/// </remarks>
    private static void UpdateMaxScore()
    {
        if (score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetInt("MaxScore", maxScore);
        }
    }

    /// <summary>
	/// Обновление текста text_ms
	/// </summary>
    private static void UpdateText()
    {
        text_ms_static.text = "рекорд: " + Convert.ToString(maxScore);
    }
}