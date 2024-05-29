using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace TaskM
{
    
    class Controller
    {
        
        //variables globales publicas que se van a acceder desde otras clases
        public string beforeNameEvent = "";
        public string beforeDateEvent = "";
        public string beforePriorityEvent = "";
        public string deletedTask = "";
        public Boolean select_task = true;
        //variables privadas
        private ConsoleColor color = ConsoleColor.Red;
        private Errors error = new Errors();
        

        //verifica que un archivo existas donde se guardarán las tareas
        public void ExistenceFile()
        {
            try
            {
                if (!File.Exists("tareas.txt"))
                {
                    StreamWriter sw = new StreamWriter("tareas.txt", true);
                    sw.WriteLine("---------------------Tareas---------------------");
                    sw.Close();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " Error en el metodo que comprueba la existencia del archivo tareas.txt");
            }



        }
        //muestra las tareas
        public void ViewTasks()
        {
            try
            {
                ConsoleColor color = ConsoleColor.Cyan;
                StreamReader sr = new StreamReader("tareas.txt");
                string line;
                int iterator = 0;

                while ((line = sr.ReadLine()) != null)
                {

                    if (line != "---------------------Tareas---------------------")
                    {
                        iterator++;
                        Console.ForegroundColor = color;
                        System.Console.WriteLine(iterator + ".  " + line);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = color;
                        System.Console.WriteLine(line);
                        Console.ResetColor();
                    }

                }

                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Error en el metodo ver tareas");
            }


        }
        //agrega una tarea
        public void AddTask(string task)
        {
            try
            {
                
                StreamWriter sw = new StreamWriter("tareas.txt", true);
                sw.WriteLine(task);
                sw.Close();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Error en el metodo Agregar tareas");
            }
        }
        //edita una tarea
        public void EditTask(int task, int feature, string newContent)
        {
            try
            {
                List<string> lines = File.ReadAllLines("tareas.txt").ToList();
                string[] split = lines[task].Split('|');
                string name = split[0];
                string date = split[1];
                string priority = split[2];
                string newline = "";
                
                
                switch (feature - 1)
                {
                    case 0://nombre
                        beforeNameEvent = split[0];
                        split[0] = newContent;
                        break;
                    case 1://fecha
                        beforeDateEvent = split[1];
                        split[1] = newContent;
                        break;
                    case 2://prioridad
                        beforePriorityEvent = split[2];
                        split[2] = newContent;
                        break;

                    default:
                        break;
                }
                for (int i = 0; i < split.Length; i++)
                {
                    if (i == 0)
                    {
                        newline += split[i];
                    }
                    else
                    {
                        newline += "|" + split[i];
                    }
                }
                lines[task] = newline;
                File.WriteAllLines("tareas.txt", lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Error en el metodo Modificar tareas");
            }
        }

        //elimina una tarea
        public void DeleteTask(int task)
        {
            try
            {

                StreamReader sr = new StreamReader("tareas.txt");
                string linea;
                int contador = 0;

                while ((linea = sr.ReadLine()) != null)
                {
                    contador++;
                }

                sr.Close();



                if (task != 0 && task < contador)
                {
                    List<string> lineas = File.ReadAllLines("tareas.txt").ToList();
                    deletedTask = lineas.ElementAt(task);
                    lineas.RemoveAt(task);
                    File.WriteAllLines("tareas.txt", lineas);
                    Console.Clear();

                }
                else
                {
                    error.non_existence();
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Error en el metodo Borrar tareas");
            }
        }
        //valida que el usuario este ingresando una tarea existente
        public Boolean ValidateLines(int task)
        {
            
            StreamReader sr = new StreamReader("tareas.txt");
            string line;
            int iterator = 0;

            while ((line = sr.ReadLine()) != null)
            {
                iterator++;
            }

            sr.Close();
            if (task <= 0 || task > (iterator - 1))
            {
                return false;
            }
            else { return true; }
        }

        // Búsqueda por nombre
        public void SearchTasksByName(string name)
        {
            List<string> tasks = File.ReadAllLines("tareas.txt").ToList();

            // Convertir el término de búsqueda a minúsculas
            string lowerCaseName = name.ToLower();

            // Filtrar las tareas por nombre (ignorando mayúsculas y minúsculas)
            List<string> filteredTasks = tasks.Where(task => task.ToLower().Contains(lowerCaseName)).ToList();
            Console.Clear();
            Console.WriteLine("Tareas filtradas por nombre:");
            foreach (string task in filteredTasks)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(task);
                Console.ResetColor();
                
            }
        }


    }

}