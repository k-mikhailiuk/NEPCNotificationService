using System.ComponentModel.DataAnnotations.Schema;

namespace ControlPanel.DataAccess.Entities;

public class LimitIdDescriptionDirectory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    public long LimitCode { get; set; }
    
    public string Name { get; set; }
    
    public string DescriptionRu { get; set; }
    
    public string DescriptionKg { get; set; }
    
    public string DescriptionEn { get; set; }
    
    public static LimitIdDescriptionDirectory Create(long limitCode, string name, string descriptionRu, string descriptionKg, string descriptionEn)
    {
        if (string.IsNullOrWhiteSpace(descriptionRu))
            throw new ArgumentException("DescriptionRu cannot be null or whitespace.", nameof(descriptionRu));
        
        if (string.IsNullOrWhiteSpace(descriptionKg))
            throw new ArgumentException("DescriptionKg cannot be null or whitespace.", nameof(descriptionKg));
        
        if (string.IsNullOrWhiteSpace(descriptionEn))
            throw new ArgumentException("DescriptionEn cannot be null or whitespace.", nameof(descriptionEn));
        
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        
        if (limitCode == 0)
            throw new ArgumentException("LimitCode cannot be null or default value.", nameof(limitCode));
        
        return new LimitIdDescriptionDirectory
        {
            LimitCode = limitCode,
            Name = name,
            DescriptionRu = descriptionRu,
            DescriptionKg = descriptionKg,
            DescriptionEn = descriptionEn
        }; 
    }
}