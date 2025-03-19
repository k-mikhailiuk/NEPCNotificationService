using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Core.Extensions;

public static class UnifyProcessorExtension<T> where T : INotification
{
    public static async Task PreloadAndUnifyExtensionsAsync(
        List<T> entities,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var extensionKeys = new HashSet<(string extId, long notifId)>();

        foreach (var entity in entities)
        {
            if (entity.Extensions == null) continue;
            var notificationId = entity.NotificationId;

            foreach (var ext in entity.Extensions)
            {
                ext.NotificationId = notificationId;

                extensionKeys.Add((ext.ExtensionId, ext.NotificationId));
            }
        }

        var allExtensionIds = extensionKeys.Select(k => k.extId).Distinct().ToList();
        
        if(allExtensionIds.Count == 0)
            return;
        
        var inClause = string.Join(", ", allExtensionIds.Select(x => $"'{x}'"));

        var sql = $"SELECT * FROM [nepc].[NotificationExtensions] WHERE [ExtensionId] IN ({inClause})";

        var partialList = await unitOfWork.FromSql<NotificationExtension>(sql).ToListAsync(cancellationToken);
        
        var existingExtensions = partialList
            .Where(x => extensionKeys.Contains((x.ExtensionId, x.NotificationId)))
            .ToList();

        var extensionsCache = new Dictionary<(string extId, long notifId), NotificationExtension>();
        foreach (var extensionFromDb in existingExtensions)
        {
            var key = (extensionFromDb.ExtensionId, extensionFromDb.NotificationId);
            extensionsCache[key] = extensionFromDb;
        }

        foreach (var entity in entities)
        {
            if (entity.Extensions == null) continue;

            for (var i = 0; i < entity.Extensions.Count; i++)
            {
                var extension = entity.Extensions[i];
                extension.NotificationId = entity.NotificationId;

                var key = (extension.ExtensionId, extension.NotificationId);
                if (extensionsCache.TryGetValue(key, out var existingExt))
                {
                    entity.Extensions[i] = existingExt;
                }
                else
                {
                    extensionsCache[key] = extension;
                }
            }
        }
    }
}