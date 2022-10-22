using Hit2;

var hit = new Hit(o => o.RelaxMode = true);

Console.WriteLine(hit.ToString());

var records = await hit.RunTestAsync("Person-Crud-1");
Console.WriteLine(records.ToString());
