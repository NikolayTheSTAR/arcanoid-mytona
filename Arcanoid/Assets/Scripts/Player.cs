using UnityEngine;

/// <summary>
/// Класс представляющий платформу, управляемую игроком
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
	/// Компонент Animator платформы
	/// </summary>
    [SerializeField]
    private Animator anim;

    /// <summary>
	/// Поле для статического доступа к <see cref="anim"/>
	/// </summary>
    private static Animator anim_static;

    /// <summary>
	/// Активный объект Player
	/// </summary>
    private static Player player;

    /// <summary>
	/// Компонент Transform платформы
	/// </summary>
    public static Transform tran;

    /// <summary>
    /// Позиция мыши по оси X
	/// </summary>
	/// <remarks>
	/// Нужно для управления платформой
	/// </remarks>
    private float mousePosX;

    /// <summary>
	/// Тип эффекта, наложенного на платформу
	/// N - отсутствие эффекта
	/// S - увеличение
	/// </summary>
    private static char effectType = 'N';

    /// <summary>
	/// Истинно если активно действие эффекта
	/// </summary>
    private static bool isEffect;

    /// <summary>
	/// Счётчик времени
	/// </summary>
	/// <remarks>
	/// Нужен для ограничения продолжительности эффектов
	/// </remarks>
    private static float time = 0;

    /// <summary>
	/// Максимальное расстояние платформы от центра
	/// </summary>
	/// <remarks>
	/// Нужно для ограничения перемещения платформы
	/// </remarks>
    private const float maxDistance = 1.85f;

    /// <summary>
	/// Разница в размере платформы после увеличения
	/// </summary>
	/// <remarks>
	/// Нужно для корректного ограничения перемещения платформы после изменения размера
	/// </remarks>
    private const float effectSizeDifference = 0.2f;

    /// <summary>
	/// продолжительность эффектов (в секундах)
	/// </summary>
    private const float effectTime = 5;

    void Start()
    {
        player = this;
        tran = transform;
        anim_static = anim;
    }

    void Update()
    {
        // обычное перемещение платформы
        if (Input.GetMouseButton(0))
        {
            mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

            if (!isEffect)
            {
                if (Mathf.Abs(mousePosX) < maxDistance)
                {
                    tran.position = new Vector2(mousePosX, tran.position.y);
                }
                else
                {
                    tran.position = new Vector2(mousePosX < maxDistance ? -maxDistance : maxDistance, tran.position.y);
                }
            }
            else
            {
                float maxDistanceWithEffect = maxDistance - effectSizeDifference;
                if (Mathf.Abs(mousePosX) < maxDistanceWithEffect)
                {
                    tran.position = new Vector2(mousePosX, tran.position.y);
                }
                else
                {
                    if (mousePosX > maxDistanceWithEffect)
                    {
                        tran.position = new Vector2(maxDistanceWithEffect, tran.position.y);
                    }
                    else
                    {
                        tran.position = new Vector2(-maxDistanceWithEffect, tran.position.y);
                    }
                }
            }
        }

        // касание не во время партии
        if (!Game.isMove & Input.GetMouseButtonUp(0))
        {
            // переход на следующий уровень
            if (Game.isWin)
            {
                Game.NextLevel();
            }

            // перезапуск
            else if (Game.isFall)
            {
                Game.Replay();
            }

            // начало уровня
            else
            {
                Balls.StartMove();
            }
        }

        // подсчёт времени эффекта
        if (isEffect)
        {
            time += Time.deltaTime;
            if (time > effectTime)
            {
                // завершение эффекта
                anim_static.SetBool("Big", false);
            }
        }
    }

    /// <summary>
	/// Установка эффекта
	/// </summary>
	/// <param name="newEffectType">
	/// Новый тип эффекта
	/// </param>
    public static void SetEffect(char newEffectType)
    {
        effectType = newEffectType;
        anim_static.SetBool("Big", true);
        isEffect = true;
        time = 0;
    }

    /// <summary>
	/// Удаление эффекта
	/// </summary>
	/// <remarks>
	/// Вызывается после завершения анимации End
	/// </remarks>
    private void RemoveEffect()
    {
        effectType = 'N';
        isEffect = false;
    }
}