using MediatR;

namespace Acudir.Test.Application.Commands;

public class UpdatePersonCommand : IRequest<bool>
{
    public int id { get; set; }
    public string NombreCompleto { get; set; }
    public int  Edad { get; set; }
    public string Domicilio { get; set; }
    public string Telefono { get; set; }
    public string Profesion { get; set; }
}