namespace GradesApi.Models;


public class GradeItem {

    public long Id { get; set;}
    public string Name {get; set;}    
    public string Grade {get; set;} 
    public string? Secret {get; set;}   
    public bool IsComplete { get; set; }

}