using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Practica_II.Pages
{
    public class NominaModel : PageModel
    {
        public List<Person> Persons { get; set; }

        private readonly ILogger<NominaModel> _logger;


        public NominaModel(ILogger<NominaModel> logger)
        {
            _logger = logger;

        }

        public void OnGet()
        {
            Persons = new List<Person> {
                new Person()
                {
                    Nombres = "Gabriela",
                    Apellidos = "Diaz Guerrero",
                    Cargo = "Medico",
                    Salario =   50000,
                },
                new Person()
                {
                    Nombres = "Juan Jose",
                    Apellidos = "Leonardo Amparo",
                    Cargo = "Administrador de Sistema",
                    Salario = 60000,
                },
                new Person()
                {
                    Nombres = "Carlos",
                    Apellidos = "Castillo Herrera",
                    Cargo = "Doctor",
                    Salario = 80000,
                },
                new Person()
                {
                    Nombres = "Richard",
                    Apellidos = "Acosta Cruz",
                    Cargo = "Contable",
                    Salario = 60000,
                },
                new Person()
                {
                    Nombres = "Leonardo",
                    Apellidos = "Amparo Castillo",
                    Cargo = "Auditor",
                    Salario = 95000,
                },

            };
        }
    }

    public class Person
    {
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public String Cargo { get; set; }
        public double Salario { get; set; }


        public double AFP
        {
            get
            {
                return Valor_AFP(Salario);
            }

        }

        public double ARS
        {
            get
            {
                return Valor_ARS(Salario);
            }
        }

        public double ISR
        {
            get
            {
                return Valor_ISR(Salario - AFP - ARS);
            }
        }

        public double Total_Descuento
        {
            get
            {
                return Descuento(AFP + ARS + ISR);
            }
        }

        public double Salario_Neto
        {
            get
            {
                return Sala_Neto(Salario);
            }
        }

        private double Sala_Neto(double Sala_Neto)
        {
            Sala_Neto = Salario - Total_Descuento;

            return Sala_Neto;
        }

        private double Descuento(double Descuento)
        {
            Descuento = AFP + ARS + ISR;

            return Descuento;
        }

        private double Valor_AFP(double salario)
        {
            double SalarioMinimo = 13482.00 * 20;

            if (salario > SalarioMinimo)
            {
                salario = SalarioMinimo;
            }
            return (salario * (2.87) / 100);
        }

        private double Valor_ARS(double salario)
        {
            double SalarioMinimo = 13482.00 * 10;

            if (salario > SalarioMinimo)
            {
                salario = SalarioMinimo;

            }

            return (salario * (3.04) / 100);

        }
        private double Valor_ISR(double salario)
        {
            //Evaluando mi variable ISR = 0
            double ISR = 0;

            // Si el salario es igual o esta entre 34,685 y 52,027.42 se calcula con una tasa de 15%
            if ((salario >= 34685) && (salario <= 52027.42))
            {
                ISR = ((salario - 34685) * 15 / 100);
            }
            // Si el salario es igual o esta entre >= 52,027.43 y 72260.25 se calcula con una tasa de 20%
            else if ((salario >= 52027.43) && (salario <= 72260.25))
            {
                ISR = 2601.33 + ((salario - 52027.43) * 20 / 100);
            }
            // Si el salario es igual o esta por encima de 72260.25 se calculara con una tasa del 25%
            else if (salario >= 72260.25)
            {
                ISR = 6648 + ((salario - 72260.25) * 25 / 100);
            }

            return ISR;
        }
    }

}
