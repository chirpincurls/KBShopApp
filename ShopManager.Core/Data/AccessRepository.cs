using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using Dapper;

public class AccessRepository
{
    // Note: You may need 'Microsoft.Jet.OLEDB.4.0' for older .mdb files 
    // or 'Microsoft.ACE.OLEDB.12.0' for newer ones.
    private readonly string _connectionString;

    public AccessRepository(string dbPath)
    {
        _connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Persist Security Info=False;";
    }

    public List<Part> GetAllParts()
    {
        using (var conn = new OleDbConnection(_connectionString))
        {
            // Access SQL is slightly different (e.g., brackets for reserved words)
            string sql = "SELECT PartID, PartNumber, Description, Cost, QuantityOnHand, LastUpdated FROM Parts";
            return conn.Query<Part>(sql).ToList();
        }
    }

    public void UpdatePart(Part part)
    {
        using (var conn = new OleDbConnection(_connectionString))
        {
            string sql = @"
                UPDATE Parts 
                SET PartNumber = @PartNumber, 
                    Description = @Description, 
                    Cost = @Cost, 
                    QuantityOnHand = @QuantityOnHand
                WHERE PartID = @PartID";
            
            conn.Execute(sql, part);
        }
    }
}
