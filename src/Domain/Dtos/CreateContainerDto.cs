using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;

public class CreateContainerDto
{
    public string Name { get; set; } = string.Empty;
    public string Compose { get; set; } = string.Empty;
    public bool StartOnCreate { get; set; } = false;
}