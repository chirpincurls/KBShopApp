using System;

public class Part
{
    // Maps to the Primary Key in Access
    public int PartID { get; set; } 
    public string PartNumber { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public int QuantityOnHand { get; set; }
    
    // Crucial for sync: If the legacy DB has a timestamp, use it. 
    // If not, we rely on ID matching and value comparison.
    public DateTime? LastUpdated { get; set; } 
}
