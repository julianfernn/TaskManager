using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskM
{
    class Errors
    {
        private ConsoleColor color = ConsoleColor.Red;
        public void only_numbers()
        {
            Console.Clear();
            Console.ForegroundColor = color;
            System.Console.WriteLine("Error: Debe ingresar solo numeros");
            Console.ResetColor();
        }
        public void enter_value()
        {
            Console.Clear();
            Console.ForegroundColor = color;
            System.Console.WriteLine("Error: Debe ingresar una entrada valida no vacía");
            Console.ResetColor();
        }
        public void valueout_of_range()
        {
            Console.Clear();
            Console.ForegroundColor = color;
            System.Console.WriteLine("Error: Su seleccion está fuera de rango");
            Console.ResetColor();
        }
        public void non_existence()
        {
            Console.Clear();
            Console.ForegroundColor = color;
            System.Console.WriteLine("Error: No existe la tarea");
            Console.ResetColor();
        }
        public void date_error()
        {
            Console.Clear();
            Console.ForegroundColor = color;
            System.Console.WriteLine("Error: Formato de fecha no válido. Por favor, ingresa la fecha en formato dd-mm-yyyy.");
            Console.ResetColor();
        }
    }
}