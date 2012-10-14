using System.ComponentModel.DataAnnotations;

namespace MvcApplication1.Models
{
    public class Test
    {
        [RegularExpression("/[^<>]{3,100}/", ErrorMessage = "At least 3 chars")]
        public string Name { get; set; }

        public string AnotherProperty { get; set; }

        public int Id { get; set; }
    }
}