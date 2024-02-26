using System.ComponentModel.DataAnnotations;

namespace Ktsoft.Models;

public class Person {
    public int Id { get; set; }
    public string? FamilyName { get; set; }
    public string? FirstName { get; set; }
    [DataType(DataType.Date)] public DateTime DoB { get; set; }
    public bool Gender { get; set; }
}