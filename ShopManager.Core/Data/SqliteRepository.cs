using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using Dapper;

public class SqliteRepository
{
    private readonly string _connectionString;

    public SqliteRepository(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";
    }

    public void InitializeDatabase()
    {
        using (var conn = new SqliteConnection(_connectionString))
        {
            conn.Open();
            // Ensure table exists
            conn.Execute(@"
                CREATE TABLE IF NOT EXISTS Parts (
                    PartID INTEGER PRIMARY KEY,
                    PartNumber TEXT,
                    Description TEXT,
                    Cost REAL,
                    QuantityOnHand INTEGER,
                    LastUpdated TEXT
                )");
        }
    }

    public List<Part> GetAllParts()
    {
        using (var conn = new SqliteConnection(_connectionString))
        {
            return conn.Query<Part>("SELECT * FROM Parts").ToList();
        }
    }

    // "Upsert" logic: Insert, or Update if ID exists
    public void SyncPart(Part part)
    {
        using (var conn = new SqliteConnection(_connectionString))
        {
            string sql = @"
                INSERT INTO Parts (PartID, PartNumber, Description, Cost, QuantityOnHand, LastUpdated)
                VALUES (@PartID, @PartNumber, @Description, @Cost, @QuantityOnHand, @LastUpdated)
                ON CONFLICT(PartID) DO UPDATE SET
                    PartNumber = excluded.PartNumber,
                    Description = excluded.Description,
                    Cost = excluded.Cost,
                    QuantityOnHand = excluded.QuantityOnHand,
                    LastUpdated = excluded.LastUpdated;";

            conn.Execute(sql, part);
        }
    }
}
