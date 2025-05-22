using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace checkmark.Models
{

    public enum Priority {Low = 1, Medium = 2, High = 3}
    public enum Status {Pending = 1, InProgress = 2, Completed = 3}

    public class User
    {
        [Key]
        public int UserId { get; set; } //pk de la tabla de usuarios

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La direccion de EMail es obligatoria.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Direccion de EMail")]
        public string UserEmailAddress { get; set; }

        [Required(ErrorMessage = "La contraseña es oblgatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string UserPassword { get; set; }

        //Validacion de coincidencia entre contraseñas

        [NotMapped]
        [Compare("UserPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        [DataType(DataType.Password)]
        [Display(Name = "Repita la contraseña")]
        public string UserPasswordCheck { get; set; }

        //Relacion con tabla tareas
        public ICollection<clTask> Tasks { get; set; } = new List<clTask>();

    }
}
