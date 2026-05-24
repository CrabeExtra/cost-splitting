namespace Round_2.Application.DTOs;

public class AddPersonDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
}

public class PersonDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}