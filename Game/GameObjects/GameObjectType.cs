namespace SharpPixel.Game.GameObjects
{
    /// <summary>
    /// Тип объекта, используется для коллиженов
    /// </summary>
    public enum GameObjectType
    {
        /// <summary>
        /// Триггер
        /// </summary>
        Trigger,
        
        /// <summary>
        /// Игрок
        /// </summary>
        Player,

        /// <summary>
        /// Препятствие
        /// </summary>
        Obstacle,

        /// <summary>
        /// Собираемый объект
        /// </summary>
        Collectable,

        /// <summary>
        /// Гражданские авто
        /// </summary>
        Civilian,

        /// <summary>
        /// Вражеские авто
        /// </summary>
        Enemy,

        /// <summary>
        /// Снаряды игрока
        /// </summary>
        PlayerBullet,

        /// <summary>
        /// Снаряды противника
        /// </summary>
        EnemyBullet
    }
}
