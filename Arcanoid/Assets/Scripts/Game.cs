using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс представляющий основную игровую логику
/// </summary>
public abstract class Game : MonoBehaviour
{
    /// <summary>
	/// Истинно пока идёт партия
	/// </summary>
    public static bool isMove = false;

    /// <summary>
	/// Истинно во время победы
	/// </summary>
    public static bool isWin = false;

    /// <summary>
	/// Истинно во время поражения
	/// </summary>
    public static bool isFall = false;

    /// <summary>
	/// Граница игрового поля по оси X
	/// </summary>
    public const float maxPosX = 2.56f;

    /// <summary>
	/// Граница игрового поля по оси Y
	/// </summary>
    public const float maxPosY = 4.75f;

    /// <summary>
	/// Отступ игрового поля сверху экрана
	/// </summary>
	/// <remarks>
	/// Нужен для правильного отображения статистики
	/// </remarks>
    public const float upIndent = 0.55f;

    /// <summary>
	/// Перезапуск игры
	/// </summary>
    public static void Replay()
    {
        Score.RemoveScore();
        LevelManager.RemoveLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isMove = false;
        isWin = false;
        isFall = false;
    }

    /// <summary>
	/// Победа
	/// </summary>
	/// <remarks>
	/// Вызывается после прохождения уровня
	/// </remarks>
    public static void Win()
    {
        Balls.ClearBalls();
        Balls.AddSpeed();
        Game.isMove = false;
        Game.isWin = true;
        Camera.main.GetComponent<Animator>().SetBool("Win", true);
        LevelManager.AddLevel();
    }

    /// <summary>
	/// Пуруход на следующий уровень
	/// </summary>
    public static void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isMove = false;
        isWin = false;
        isFall = false;
    }
}