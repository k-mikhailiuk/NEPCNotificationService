using System.ComponentModel.DataAnnotations.Schema;

namespace ControlPanel.DataAccess.Entities;

public class LimitIdDescriptionDirectory
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string DescriptionRu { get; set; }
    
    public string DescriptionKg { get; set; }
    
    public string DescriptionEn { get; set; }
}