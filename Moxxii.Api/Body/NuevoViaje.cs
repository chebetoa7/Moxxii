namespace Moxxii.Api.Body
{
    public class NuevoViaje
    {
        public int idPasajero { get; set; }
        public double latInitial { get; set; }
        public double longInitial { get; set; }
        public string city { get; set; }
        public string dictric { get; set; }
        public bool status { get; set; }
        public string confirmationStatus { get; set; }
        public string TypeUser { get; set; }
        public string Disponibility { get; set; }
    }
}
