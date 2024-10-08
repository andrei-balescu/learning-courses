namespace DesignPatterns.Structural.Facade;

/// <summary>
/// Request object containing the user submitted data
/// </summary>
public class OrderRequest
{
    public string Name { get; set; }
    public string CardNumber { get; set; }
    public decimal Amount { get; set; }
    public string Address { get; set; }
    public IEnumerable<string> ItemIds { get; set; }
}