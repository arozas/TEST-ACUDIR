namespace Acudir.Test.Application.Responses;

public class PersonResponse 
{
    public int id { get; set; }
    public string NombreCompleto { get; set; }
    public int  Edad { get; set; }
    public string Domicilio { get; set; }
    public string Telefono { get; set; }
    public string Profesion { get; set; }
    public bool Active { get; set; }
}