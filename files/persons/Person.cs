namespace App
{
    public record class Person
    {
        public string Name { get; set; }
        public Person(string name) => Name = name;


    }
}
