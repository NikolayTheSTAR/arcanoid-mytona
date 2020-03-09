using UnityEngine;

/// <summary>
/// Класс представляющий блок
/// </summary>
public class Block : MonoBehaviour
{
    /// <summary>
    /// Рендерер блока
    /// </summary>
    [SerializeField]
    private SpriteRenderer rend;

    /// <summary>
    /// Анимация блока
    /// </summary>
    [SerializeField]
    private Animator anim;

    /// <summary>
    /// Прочность блока
    /// </summary>
    public int HP;

    /// <summary>
    /// Максимальная прочность блока
    /// </summary>
    private const int maxHP = 3;

    /// <summary>
    /// Обновление цвета блока
    /// </summary>
    public void UpdateColor()
    {
        float r = Random.Range(0f, 1f);

        // занесение рандомного цвета
        if (r <= 0.25f)
        {
            rend.color = Blocks.ColorVariant[0];
        }
        else if (r <= 0.5f)
        {
            rend.color = Blocks.ColorVariant[1];
        }
        else if (r <= 0.75f)
        {
            rend.color = Blocks.ColorVariant[2];
        }
        else
        {
            rend.color = Blocks.ColorVariant[3];
        }
    }

    /// <summary>
    /// Обновление прочности блока
    /// </summary>
    public void UpdateHP()
    {
        if (LevelManager.Level == 1)
        {
            HP = 1;
        }
        else
        {
            HP = Random.Range(1, maxHP + 1);
        }
    }

    /// <summary>
    /// Удар по блоку
    /// </summary>
    public void Damage()
    {
        HP--;

        if (HP <= 0)
        {
            anim.SetTrigger("Destroy");
            Blocks.blockCount--;
            Score.AddScore();

            if (Blocks.blockCount <= 0)
            {
                Game.Win();
            }
            else
            {
                if (LevelManager.Level >= 3)
                {
                    float r = Random.Range(0f, 1f);

                    if (r <= Bonus.bonusOpportunity)
                    {
                        CreateBonus();
                    }
                }
            }
        }
        else anim.SetTrigger("Damage");
    }

    /// <summary>
    /// Создание бонуса
    /// </summary>
    private void CreateBonus()
    {
        Instantiate(Blocks.BonusObject, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity, Blocks.BonusParent);
    }

    /// <summary>
    /// Удаление блока
    /// </summary>
    void Destroy()
    {
        Destroy(gameObject);
    }
}