using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationRendezVous.Models
{
    public class RendezVous
    {
        public required int Id { get; set; }

        [Required(ErrorMessage = "Le nom du patient est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le nom du patient ne peut pas dépasser 100 caractères.")]
        public required string NomPatient { get; set; }

        [Required(ErrorMessage = "La date et l'heure du rendez-vous sont obligatoires.")]
        [FutureDate(ErrorMessage = "La date et l'heure du rendez-vous doivent être dans le futur.")]
        public required DateTime DateHeure { get; set; }

        [Required(ErrorMessage = "Le motif du rendez-vous est obligatoire.")]
        [StringLength(200, ErrorMessage = "Le motif ne peut pas dépasser 200 caractères.")]
        public required string Motif { get; set; }
    }
}
