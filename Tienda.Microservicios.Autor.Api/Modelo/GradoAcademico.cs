namespace Tienda.Microservicios.Autor.Api.Modelo
{
    public class GradoAcademico
    {
        public int GradoAcademicoId { get; set; }
        public string Nombre { get; set; }
        public string CentroAcademico { get; set; }
        public DateTime FechaGrado { get; set; }
        public int AutorLibroId { get; set; }
        public AutorLibro AutorLibro { get; set; } // Relación con AutorLibro
        public string GradoAcademicoGuid { get; set; } // GUID único para el grado académico
    }
}
