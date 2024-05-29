using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Diagnostics.Tracing;

namespace TaskM
{
    class TaskM
    {
       
        static void Main(string[] args)
        {
            //inicializar el administrador de tareas
            Console.Title = "Administrador de tareas";
            Task_Manager taskManager = new Task_Manager();
            Controller administrador = new Controller();
            //se verifica primero que exista el archivo tareas
            administrador.ExistenceFile();
            //se hace las suscripcion a los eventos
            taskManager.taskAdded += OntaskAdded;
            taskManager.taskEdited += OntaskEdited;
            taskManager.taskDeleted += OntaskDeleted;

            taskManager.Manager();
            

        }
        //evento que se dispara cuand se agrega una tarea
        private static void OntaskAdded(object sender, string name, string date, string priority)
        {
            ConsoleColor color = ConsoleColor.Red;
            System.Console.Write("La tarea ");
            Console.ForegroundColor = color;
            System.Console.Write(name + "|" + date + "|" + priority);
            Console.ResetColor();
            Console.WriteLine(" Se agregó EXITOSAMENTE.");
        }
        //evento que se dispara cuando se edita
        private static void OntaskEdited(object sender,int position ,int feature, string beforeContent, string newContent)
        {
            ConsoleColor color = ConsoleColor.Red;
            switch (feature)
            {
                case 1:
                    System.Console.Write("Se modificó EXITOSAMENTE el nombre ");
                    Console.ForegroundColor = color;
                    System.Console.Write(beforeContent);
                    Console.ResetColor();
                    System.Console.Write(" por el nuevo nombre: ");
                    Console.ForegroundColor = color;
                    System.Console.WriteLine(newContent);
                    Console.ResetColor();
                    break;
                case 2:
                    System.Console.Write("Se modificó EXITOSAMENTE la fecha ");
                    Console.ForegroundColor = color;
                    System.Console.Write(beforeContent);
                    Console.ResetColor();
                    System.Console.Write(" por la nueva fecha: ");
                    Console.ForegroundColor = color;
                    System.Console.WriteLine(newContent);
                    Console.ResetColor();
                    break;
                case 3:
                    System.Console.Write("Se modificó EXITOSAMENTE la prioridad ");
                    Console.ForegroundColor = color;
                    System.Console.Write(beforeContent);
                    Console.ResetColor();
                    System.Console.Write(" por la nueva prioridad: ");
                    Console.ForegroundColor = color;
                    System.Console.WriteLine(newContent);
                    Console.ResetColor();
                    break;
            }

        }
        //evento que se dispara cuando se borra
        private static void OntaskDeleted(object sender, int task, string task_c)
        {
            ConsoleColor color = ConsoleColor.Red;
            System.Console.Write("La tarea ");
            Console.ForegroundColor = color;
            System.Console.Write(task+". "+ task_c);
            Console.ResetColor();
            Console.WriteLine(" Se eliminó EXITOSAMENTE.");
        }
    }
}
