using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс представляющий бонус
/// </summary>
public class Bonus : MonoBehaviour
{
    /// <summary>
    /// Массив вариантов спрайта бонуса
    /// </summary>
    [SerializeField]
    private Sprite[] spriteVariant = new Sprite[4];

    /// <summary>
    /// Рендерер бонуса
    /// </summary>
    [SerializeField]
    private SpriteRenderer rend;

    /// <summary>
    /// Аниматор бонуса
    /// </summary>
    [SerializeField]
    private Animator anim;

    /// <summary>
    /// Вероятность появления бонуса
	/// Используется после уничтожения блока
    /// </summary>
    public const float bonusOpportunity = 0.25f;

    /// <summary>
	/// Скорость падения бонуса
	/// </summary>
    private const float speed = 2;

    /// <summary>
	/// Диаметр бонуса
	/// Нужен для правильной проверки выхода за пределы карты
	/// </summary>
    private const float diaBonus = 0.5f;

    /// <summary>
	/// Тип бонуса
	/// F - Ускорение шара
	/// L - Замедление шара
	/// A - Создание клона шара
	/// S - Увеличение платформы
	/// </summary>
    private char type;

    /// <summary>
	/// Истинно во время падения бонуса
	/// </summary>
    private bool isMove = true;

    void Start()
    {
        int i = Random.Range(0, 4);

        switch (i)
        {
            case 0:
                type = 'F';
                break;
            case 1:
                type = 'L';
                break;
            case 2:
                type = 'A';
                break;
            case 3:
                type = 'S';
                break;
        }
        rend.sprite = spriteVariant[i];
    }

    void Update()
    {
        if (isMove & Game.isMove)
        {
            Move();
        }
    }

    /// <summary>
	/// Движение бонуса
	/// </summary>
	/// <remarks>
	/// Представляет собой равномерное падение
	/// </remarks>
    private void Move()
	{
        transform.Translate(new Vector2(0, -speed * Time.deltaTime));

        if (transform.position.y < -(Game.maxPosY + diaBonus))
        {
            Destroy();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") & Game.isMove)
        {
            isMove = false;
            anim.SetTrigger("Get");
            
            switch (type)
            {
                case 'F':
                case 'L':
                case 'A':
                    Ball.SetEffect(type);
                    break;
                case 'S':
                    Player.SetEffect(type);
                    break;
            }
        }
    }

    /// <summary>
	/// Удаление бонуса
	/// </summary>
    private void Destroy()
    {
        Destroy(gameObject);
    }
}