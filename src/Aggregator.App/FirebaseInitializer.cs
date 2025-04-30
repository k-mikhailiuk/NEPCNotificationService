using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Aggregator.App;

/// <summary>
/// Предоставляет статический метод для инициализации Firebase на основе заданного JSON.
/// </summary>
/// <remarks>
/// Ожидается, что в конфигурационном файле (App.config/Web.config) будет ключ
/// <c>FireBaseCredentialsFile</c>, указывающий путь к JSON-файлу с данными учётных данных Firebase.
/// </remarks>
public static class FirebaseInitializer
{
    private static readonly object _lockObject = new();
		
    /// <summary>
    /// Инициализирует приложение Firebase, используя JSON-файл с учётными данными, 
    /// заданный в конфигурации приложения под ключом <c>FireBaseCredentialsFile</c>.
    /// Бросает исключение, если файл не найден.
    /// </summary>
    /// // <param name="credentialsFilePath">Относительный путь к файлу JSON с ключом учётных данных Firebase.</param>
    /// <exception cref="System.IO.FileNotFoundException">
    /// Генерируется, если файл с указанным путём не существует.
    /// </exception>
		
    public static void Init(string credentialsFilePath)
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
			
        var fullPath = Path.GetFullPath(Path.Combine(baseDir, credentialsFilePath));

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"Firebase JSON не найден: {fullPath}");
        }
			
        lock (_lockObject)
        {
            if (FirebaseApp.DefaultInstance == null) 
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(fullPath)
                });
            }
        }
    }
}