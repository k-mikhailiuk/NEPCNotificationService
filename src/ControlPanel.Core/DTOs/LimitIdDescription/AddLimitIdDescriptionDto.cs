namespace ControlPanel.Core.DTOs.LimitIdDescription;

public class AddLimitIdDescriptionDto
{
    public int LimitCode { get; set; }
    
    public string Name { get; set; }
    
    public string DescriptionRu { get; set; }
    
    public string DescriptionKg { get; set; }
    
    public string DescriptionEn { get; set; }
}