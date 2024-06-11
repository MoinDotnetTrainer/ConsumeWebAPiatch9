using System.ComponentModel.DataAnnotations;

namespace ConsumeWebAPIBatch9.Models
{
    public class MyModel
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? EmailID { get; set; }
        public string? Password { get; set; }
        public DateTime? DoB { get; set; }
        public string? gender { get; set; }
        public string? Dept { get; set; }
        public decimal? Salary { get; set; }
        public bool? Status { get; set; }
        public string? Address { get; set; }

    }
}
