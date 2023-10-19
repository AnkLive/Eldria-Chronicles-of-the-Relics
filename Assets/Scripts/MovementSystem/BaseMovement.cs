using UnityEngine;


    #region Интерфесы

    /// <summary>
    /// Содержит информацию о способностях объекта двигаться
    /// </summary>
    public interface IMovable
    {
        public void Move(float moveY);
        public bool MoveToPosition(Vector2 pointB);
        public float CalculateAcceleration();
    }
    /// <summary>
    /// Содержит информацию о способностях объекта прыгать
    /// </summary>
    public interface IBounce
    {
        public void PerformJump();
        public float CalculateGravityModifier();
    }
    /// <summary>
    /// Содержит информацию о способностях объекта совершать бросок
    /// </summary>
    public interface IDash
    {
        public void PerformDash();
    }
    /// <summary>
    /// Содержит информацию о способностях объекта определять, стоит ли он на земле
    /// </summary>
    public interface IGrounded
    {
        public void CheckGroundedStatus();
        public float CalculateGravityModifier();
    }

    #endregion

    /// <summary>
    /// Содержит информацию о способностях объекта двигаться
    /// </summary>
    public abstract class BaseMovement : MonoBehaviour, IMovable, IBounce, IDash, IGrounded, IInputInitialize 
    {

        #region Методы

        /// <summary>
        /// Получение ссылок на компоненты
        /// </summary>
        //public abstract void Initialize();
        /// <summary>
        /// Реализация прыжка
        /// </summary>
        public abstract void PerformJump();
        /// <summary>
        /// Реализация движения
        /// </summary>
        public abstract void Move(float moveY);
        /// <summary>
        /// Определяет тип ускорения объекта
        /// </summary>
        /// <returns>Возвращает тип ускорения</returns>
        public abstract float CalculateAcceleration();
        /// <summary>
        /// Реализация движения из точки А в точку Б
        /// </summary>
        /// <param name="endPoint">Точка в которую объект будет двигаться</param>
        /// <returns>Возвращает булево значение, достиг ли объект точки</returns>
        public abstract bool MoveToPosition(Vector2 endPoint);
        /// <summary>
        /// Вызов IEnumerator для реализации броска
        /// </summary>
        public abstract void PerformDash();
        /// <summary>
        /// Вычисление силы действующей на объект при падении
        /// </summary>
        /// <returns>Возвращает значение силы с которой объект будет двигаться по Y координате</returns>
        public abstract float CalculateGravityModifier();
        /// <summary>
        /// Определение нахождения объекта на земле
        /// </summary>
        public abstract void CheckGroundedStatus();
        /// <summary>
        /// Получение пользовательского ввода
        /// </summary>
        public abstract void SetInputData();
        /// <summary>
        /// Сбор входных данных
        /// </summary>
        public abstract void GatherInputs();

        #endregion

    }
