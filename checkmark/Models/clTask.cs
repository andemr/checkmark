using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace checkmark.Models
{
    public class clTask
    {
        [Key]
        public int TaskId { get; set; }  // Clave primaria

        [ForeignKey("userId")]
        public User user { get; set; }


        [Required(ErrorMessage = "La tarea debe tener un nombre")]
        [Display(Name = "Nombre de la tarea")]
        public string TaskName { get; set; }

        [Display(Name = "Descripcion de la tarea")]
        public string TaskDescription { get; set; }


        [Required]
        [Display(Name = "Nivel de Prioridad")]
        public Priority priorityLvl { get; set; }

        [Required]
        public Status statusLvl { get; set; }


        [Required]
        [Display(Name = "Fecha de Creacion")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Fecha de Finalizacion")]
        public DateTime DateDue { get; set; }


        // Relación con el usuario

        public int userId { get; set; }
    }
}
