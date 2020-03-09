using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс представляющий родителя мячей
/// </summary>
public class Balls : MonoBehaviour
{
    /// <summary>
	/// Компонент Transform родителя мячей
	/// </summary>
    protected static Transform tran;

    /// <summary>
	/// Объект клона шара
	/// </summary>
	/// <remarks>
	/// Нужен для создания клона мяча
	/// </remarks>
    [SerializeField]
    private GameObject ballClone;

    /// <summary>
	/// Поле для статического доступа к <see cref="ballClone"/>
	/// </summary>
    private static GameObject BallClone;

    /// <summary>
	/// Лист с компонентами Transform всех мячей
	/// </summary>
    private static List<Transform> ballsTranList = new List<Transform>();

    /// <summary>
	/// Лист с компонентами Animator всех мячей
	/// </summary>
    private static List<Animator> ballsAnimList = new List<Animator>();

    /// <summary>
	/// Начальная позиция мяча относительно <see cref="Player"/>
	/// </summary>
    private static Vector3 startPos = new Vector3(0, 0.5f, -1);

    /// <summary>
	/// Активная скорость движения мяча
	/// </summary>
    protected static float speed = 3;

    /// <summary>
	/// Стандартная скорость движения мяча
	/// </summary>
    private const float standartSpeed = 3;

    /// <summary>
	/// Тип эффекта, наложенного на мячи
	/// N - отсутствие эффекта
	/// F - ускорение
	/// L - замедление
	/// </summary>
    protected static char effectType = 'N';

    /// <summary>
	/// Истинно пока наложен эффект
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
	/// Коэффициент изменения скорости во время ускорения/замедления
	/// </summary>
    protected const float speedEffectChange = 1.5f;

    /// <summary>
	/// Продолжительность эффектов
	/// </summary>
    protected const float effectTime = 5;
    
    void Start()
    {
        tran = transform;

        if (ballClone != null)
        {
            BallClone = ballClone;
        }
    }

    void Update()
    {
        if (isEffect)
        {
            time += Time.deltaTime;
            if (time > effectTime)
            {
                RemoveEffect();
            }
        }
    }

    /// <summary>
	/// Начало движения мячей
	/// </summary>
	/// <remarks>
	/// Вызывается во время начала уровня
	/// </remarks>
    public static void StartMove()
    {
        Game.isMove = true;
        Game.isWin = false;
        Game.isFall = false;

        foreach(Transform b in ballsTranList)
        {
            b.parent = Balls.tran;
        }
    }

    /// <summary>
	/// Добавление мяча
	/// </summary>
	/// <param name="newBall">
	/// Новый мяч
	/// </param>
    protected static void AddBall(Ball newBall)
    {
        ballsTranList.Add(newBall.transform);
        ballsAnimList.Add(newBall.GetComponent<Animator>());
    }

    /// <summary>
	/// Удаление мяча
	/// </summary>
	/// <param name="removedBall">
	/// Удаляющийся мяч
	/// </param>
    protected static void RemoveBall(Ball removedBall)
    {
        ballsTranList.Remove(removedBall.transform);
        ballsAnimList.Remove(removedBall.GetComponent<Animator>());

        if (ballsTranList.Count == 0)
        {
            Game.isMove = false;
            Game.isFall = true;
            Camera.main.GetComponent<Animator>().SetBool("Fall", true);
            speed = standartSpeed;
        }
    }

    /// <summary>
	/// Удаление всех мячей
	/// </summary>
    public static void ClearBalls()
    {
        ballsTranList.Clear();
        ballsAnimList.Clear();
        effectType = 'N';
    }

    /// <summary>
	/// Повышение скорости мячей
	/// </summary>
	/// <remarks>
	/// Вызывается после перехода на новый уровень
	/// </remarks>
    public static void AddSpeed()
    {
        speed++;
    }

    /// <summary>
	/// Установка эффекта
	/// </summary>
	/// <param name="newEffectType">
	/// Добавляемый эффект
	/// </param>
    public static void SetEffect(char newEffectType)
    {
        switch(newEffectType)
        {
            case 'F':
                effectType = newEffectType;
                foreach(Animator a in ballsAnimList)
                {
                    a.SetInteger("SpeedIndex", 1);
                }
                isEffect = true;
                time = 0;
                break;

            case 'L':
                effectType = newEffectType;
                foreach(Animator a in ballsAnimList)
                {
                    a.SetInteger("SpeedIndex", -1);
                }
                isEffect = true;
                time = 0;
                break;

            case 'A':
                CreateBallClone();
                break;
        }
    }

    /// <summary>
	/// Удаление эффекта
	/// </summary>
    private static void RemoveEffect()
    {
        effectType = 'N';
        foreach(Animator a in ballsAnimList)
        {
            a.SetInteger("SpeedIndex", 0);
        }
        isEffect = false;
    }

    /// <summary>
	/// Создание клона мяча
	/// </summary>
    protected static void CreateBallClone()
    {
        Instantiate(BallClone, Player.tran.position + startPos, Quaternion.identity, Balls.tran);
    }
}