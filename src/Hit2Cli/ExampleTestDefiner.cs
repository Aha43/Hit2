using Hit2;

namespace Hit2Cli
{
    public class ExampleTestDefiner : ITestDefiner
    {
        public void Define(Hit hit)
        {
            hit.Do("CreatePerson").With("Name", "Arne").And("LastName", "Halvrsen")
                .Do("ReadPerson")
                    .Do("UpdatePerson-1").With("LastName", "Halvorsen")
                        .Do("DeletePerson")
                        .AsUnitTest("Person-Crud-1")

                .From("ReadPerson").Do("UpdatePerson-2").With("FirstName", "Arne")
                    .Do("DeletePerson")
                    .AsUnitTest("Person-Crud-2");
        }
    }
}
