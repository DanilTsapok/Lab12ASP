using System.ComponentModel.DataAnnotations;

namespace Lab12.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="print Firstname")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "print Secondname")]
        public string? SecondName { get; set; }

        [Required(ErrorMessage = "print Age")]
        public int Age { get; set; }

    }
}
