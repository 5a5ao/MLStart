namespace Server;

public record class Person
{
    #region Property

    public string Name { get; set; }
    
    #endregion

    #region .ctor

    public Person(string name) => Name = name;

    #endregion

}

