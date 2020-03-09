using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс представляющий логику перехода по уровням
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
	/// Текст для отображения уровня
	/// </summary>
    [SerializeField] private Text levelText;

    /// <summary>
	/// Поле для статического доступа к levelText
	/// </summary>
    private static Text levelText_static;

    /// <summary>
	/// Активный уровень
	/// </summary>
    private static int level = 1;

    /// <summary>
	/// Свойство для инкапсуляции level
	/// </summary>
    public static int Level
    {
        get { return level; }
        set
        {
            if (value > 0)
            {
                level = value;
            }
        }
    }

    void Start()
    {
        levelText_static = levelText;
        UpdateText();
    }

    /// <summary>
	/// Повышение уровня
	/// </summary>
	/// <remarks>
	/// Вызывается после прохождения уровня
	/// </remarks>
    public static void AddLevel()
    {
        level++;
        UpdateText();
    }

    /// <summary>
	/// Удвление уровня
	/// </summary>
	/// <remarks>
	/// Вызывается после проигрыша
	/// </remarks>
    public static void RemoveLevel()
    {
        level = 1;
        UpdateText();
    }

    /// <summary>
	/// Обновление текста levelText
	/// </summary>
    private static void UpdateText()
    {
        levelText_static.text = "уровень: " + Convert.ToString(level);
    }
}