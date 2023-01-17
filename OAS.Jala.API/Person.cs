namespace OAS.Jala.API;


public abstract class Person
{
    public int Id { get; set; }
}

public enum Descriminator
{
    User,
    Manager
}