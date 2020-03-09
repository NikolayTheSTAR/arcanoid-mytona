using UnityEngine;

/// <summary>
/// Класс представляющий родителя блоков
/// </summary>
public class Blocks : MonoBehaviour
{
    /// <summary>
    /// Массив блоков
    /// </summary>
    [SerializeField]
    private Block[] blockArray = new Block[18];

    /// <summary>
    /// Массив вариантов цвета блоков
    /// </summary>
    [SerializeField]
    private Color[] colorVariant = new Color[4];

    /// <summary>
    /// Свойство для инкапсуляции colorVariant
    /// </summary>
    public static Color[] ColorVariant
    {
        get { return blocks.colorVariant; }
        set { blocks.colorVariant = value; }
    }

    /// <summary>
    /// Объект бонуса
    /// Нужен для создания бонусов
    /// </summary>
    [SerializeField]
    private GameObject bonusObject;

    /// <summary>
    /// Свойство для инкапсуляции bonusObject
    /// </summary>
    public static GameObject BonusObject
    {
        get { return blocks.bonusObject; }
        set { blocks.bonusObject = value; }
    }

    /// <summary>
    /// Трансформ родителя бонусов
    /// </summary>
    [SerializeField]
    private Transform bonusParent;

    /// <summary>
    /// Свойство для инкапсуляции bonusParent
    /// </summary>
    public static Transform BonusParent
    {
        get { return blocks.bonusParent; }
        set { blocks.bonusParent = value; }
    }

    /// <summary>
    /// Количество блоков
    /// </summary>
    public static int blockCount;

    /// <summary>
    /// Начальное количество блоков
    /// </summary>
    private const int StartBlockCound = 18;
    private static Blocks blocks;

    void Start()
    {
        blocks = this;
        blockCount = StartBlockCound;
        UpdateBlocks();
    }

    /// <summary>
    /// Обновление блоков
    /// </summary>
    private void UpdateBlocks()
    {
        // перебор блоков
        foreach(Block b in blockArray)
        {
            b.UpdateColor();
            b.UpdateHP();
        }
    }
}