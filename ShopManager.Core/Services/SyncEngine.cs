using System;
using System.Linq;

public class SyncEngine
{
    private readonly AccessRepository _accessRepo;
    private readonly SqliteRepository _sqliteRepo;

    public SyncEngine(string accessPath, string sqlitePath)
    {
        _accessRepo = new AccessRepository(accessPath);
        _sqliteRepo = new SqliteRepository(sqlitePath);
        _sqliteRepo.InitializeDatabase();
    }

    public void RunSync()
    {
        Console.WriteLine("Starting Sync...");

        // 1. Load both datasets into memory (For large datasets, do this in batches/pages)
        var accessParts = _accessRepo.GetAllParts().ToDictionary(p => p.PartID);
        var sqliteParts = _sqliteRepo.GetAllParts().ToDictionary(p => p.PartID);

        // 2. Sync Access -> SQLite (Import legacy data)
        foreach (var accessPart in accessParts.Values)
        {
            bool existsInSqlite = sqliteParts.TryGetValue(accessPart.PartID, out var sqlitePart);

            if (!existsInSqlite)
            {
                Console.WriteLine($"New part found in Access: {accessPart.PartNumber}. Adding to SQLite.");
                _sqliteRepo.SyncPart(accessPart);
            }
            else
            {
                // Compare data to see if Access has updates
                // If your tables have a 'LastModified' date, compare that. 
                // Otherwise, compare hash or fields.
                if (IsDifferent(accessPart, sqlitePart))
                {
                    Console.WriteLine($"Update found in Access for {accessPart.PartNumber}. Updating SQLite.");
                    _sqliteRepo.SyncPart(accessPart);
                }
            }
        }

        // 3. Sync SQLite -> Access (Push new changes back to legacy)
        // WARNING: Be careful with ID generation. If you create a new item in SQLite, 
        // it needs an ID that won't conflict with Access.
        foreach (var sqlitePart in sqliteParts.Values)
        {
            if (!accessParts.ContainsKey(sqlitePart.PartID))
            {
                // Logic to insert into Access would go here.
                // Note: Access usually auto-increments IDs, so inserting with a specific ID 
                // requires 'SET IDENTITY_INSERT' equivalent or careful handling.
                Console.WriteLine($"New part in SQLite: {sqlitePart.PartNumber}. (Implementation required to push to Access)");
            }
            else
            {
                // Check if SQLite is newer than Access (requires timestamps)
                // or if you treat SQLite as the "Editor" and Access as the "Viewer"
                var accessPart = accessParts[sqlitePart.PartID];
                
                // Example: If SQLite has changed and Access hasn't
                if (IsDifferent(sqlitePart, accessPart)) 
                {
                     Console.WriteLine($"Pushing changes for {sqlitePart.PartNumber} back to Access...");
                     _accessRepo.UpdatePart(sqlitePart);
                }
            }
        }
        
        Console.WriteLine("Sync Complete.");
    }

    private bool IsDifferent(Part a, Part b)
    {
        // Simple field comparison
        return a.PartNumber != b.PartNumber ||
               a.Description != b.Description ||
               a.Cost != b.Cost ||
               a.QuantityOnHand != b.QuantityOnHand;
    }
}
