namespace Aggregator.DataAccess.Abstractions.Repositories.AcsOtp;

/// <summary>
/// Интерфейс репозитория для работы с AcsOtp.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="AcsOtp"/>.
/// </remarks>
public interface IAcsOtpRepository : IRepository<DataAccess.Entities.AcsOtp.AcsOtp>
{
    /// <summary>
    /// Асинхронно добавляет объект <see cref="DataAccess.Entities.AcsOtp.AcsOtp"/> с применением шифрования.
    /// </summary>
    /// <param name="entity">Объект <see cref="DataAccess.Entities.AcsOtp.AcsOtp"/> для добавления.</param>
    /// <returns>Задача, представляющая асинхронную операцию добавления объекта с шифрованием.</returns>
    void AddWithEncryption(DataAccess.Entities.AcsOtp.AcsOtp entity);
}