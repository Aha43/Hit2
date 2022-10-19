using Hit2;

Console.WriteLine("Hello, World!");

var hit = new Hit();

hit.Do("CreatePerson")
    .Do("UpdatePerson")
        .Do("DeletePerson");

Console.WriteLine(hit.ToString());
