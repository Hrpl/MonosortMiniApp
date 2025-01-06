namespace MonosortMiniApp.Domain.Models;

public class BaseDictionaryModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Photo {  get; set; }
    public bool IsExistence { get; set; }
}
