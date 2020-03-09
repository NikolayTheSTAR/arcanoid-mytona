using UnityEngine;

/// <summary>
/// Класс представляющий мяч
/// </summary>
public class Ball : Balls
{
    /// <summary>
    /// Анимация мяча
    /// </summary>
    [SerializeField]
    private Animator anim;

    /// <summary>
    /// Диаметр мяча
    /// </summary>
    private const float diaBall = 0.5f;

    /// <summary>
    /// Истинно при движении вверх
    /// </summary>
    private bool up = true;

    /// <summary>
    /// Истинно при движении вправо
    /// </summary>
    private bool right = true;

    void Start()
    {
        name = "Ball";

        Balls.AddBall(this);

        switch(effectType)
        {
            case 'F':
                anim.SetInteger("SpeedIndex", 1);
                break;

            case 'L':
                anim.SetInteger("SpeedIndex", -1);
                break;
        }
    }

    void Update()
    {
        if (Game.isMove)
        {
            float step = speed * Time.deltaTime;

            if (Balls.effectType == 'F')
            {
                step *= Balls.speedEffectChange;
            }
            else if (effectType == 'L')
            {
                step /= Balls.speedEffectChange;
            }

            transform.Translate(new Vector2((right ? step : -step), (up ? step : -step)));

            // проверка на выход за границы

            // по оси X
            if (right)
            {
                if (transform.position.x > Game.maxPosX)
                {
                    right = false;
                }
            }
            else
            {
                if (transform.position.x < -Game.maxPosX)
                {
                    right = true;
                }
            }

            // по оси Y
            if (up)
            {
                if (transform.position.y > Game.maxPosY - Game.upIndent)
                {
                    up = false;
                }
            }
            else
            {
                if (transform.position.y < -(Game.maxPosY + diaBall))
                {
                    Fall();
                }
            }
        }
    }

    /// <summary>
    /// Падение мяча
    /// </summary>
	/// <remarks>
	/// Вызывается после ухода за нижний предел игрового поля
	/// </remarks>
    private void Fall()
    {
        Balls.RemoveBall(this);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (transform.position.y > Player.tran.position.y)
            {
                up = true;
            }
        }

        if (col.CompareTag("Block"))
        {
            // отражение

            float deltaX = col.transform.position.x - transform.position.x;
            float deltaY = col.transform.position.y - transform.position.y;

            // вертикальное
            if (Mathf.Abs(deltaY) >= Mathf.Abs(deltaX))
            {
                up = !(deltaY > 0);
            }

            // горизонтальное
            else
            {
                right = !(deltaX > 0);
            }

            col.GetComponent<Block>().Damage();
        }
    }
}