using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Address
{
    [Key]
    public Guid Id { get; set; }
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Zip { get; set; } = null!;
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
    public Address(string street, string city, string zip, Guid userId)
    {
        Id = Guid.NewGuid();
        Street = street;
        City = city;
        Zip = zip;
        UserId = userId;
    }
}