﻿namespace Acudir.Test.Core.Entities;

public class Person : BaseModel
{
    public string NombreCompleto { get; set; }
    public int  Edad { get; set; }
    public string Domicilio { get; set; }
    public string Telefono { get; set; }
    public string Profesion { get; set; }
}