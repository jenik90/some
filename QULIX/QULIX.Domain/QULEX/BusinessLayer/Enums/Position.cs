namespace QULIX.Domain.QULEX.BusinessLayer.Enums
{
    using QULIX.Domain.QULEX.Core.Attributes;

    /// <summary>
    /// The position.
    /// </summary>
    public enum Position
    {
        /// <summary>
        /// The developer.
        /// </summary>
        [StringValue("Разработчик ПО")]
        Developer = 1,

        /// <summary>
        /// The tester.
        /// </summary>
        [StringValue("Тестировщик ПО")]
        Tester = 2,

        /// <summary>
        /// The business analyst.
        /// </summary>
        [StringValue("Бизнес аналитик")]
        BusinessAnalyst = 3,

        /// <summary>
        /// The manager.
        /// </summary>
        [StringValue("Менеджер")]
        Manager = 4
    }
}
