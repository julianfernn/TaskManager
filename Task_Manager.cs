using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Numerics;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskM
{
    //inicio los delegados
    public delegate void taskAddedHandler(object sender, string name, string date, string priority);
    public delegate void taskEditedHandler(object sender, int position, int feature, string beforeContent, string content);
    public delegate void taskDeletedHandler(object sender, int tas, string task_);


    class Task_Manager
    {
        //declaro los manejadores
        public event taskAddedHandler taskAdded;
        public event taskEditedHandler taskEdited;
        public event taskDeletedHandler taskDeleted;

        //instancio la clase que necesito
        private Controller admin = new Controller();
        private Task task;
        Errors error = new Errors();
        GUI gui = new GUI();
        //variable entera para hacer la seleccion a traves de un switch
        private int select = 0;
        private string input="", name="", u_date="", priority="";
        //variables booleanas para los ciclos
        public System.Boolean select_task = true;
        private System.Boolean user_Menu =true ,user_Menu_name = true, user_Menu_date=true, user_Menu_priority=true, user_Menu_task=true, user_Menu_custom=true, user_Menu_remove = true;
        private int borrar_=0, n_tarea=0;
        //variable para el color
        ConsoleColor color = ConsoleColor.Red;
        
        public void Manager()
        {
            try
            {
                

                do
                {
                    
                    admin.ViewTasks();
                    gui.user_interface();
                    input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                    {
                        error.enter_value();
                        user_Menu = true;
                    }
                    else
                    {
                        if (int.TryParse(input, out select))
                        {
                            select = int.Parse(input);
                            if (select >= 0 && select <= 4)
                            {
                                switch (select)
                                {
                                    case 0:
                                        user_Menu = false;

                                        break;

                                    case 1:
                                        Console.Clear();
                                        do
                                        {
                                           
                                            gui.name_interface();
                                            input = Console.ReadLine();

                                            //verifica la entrada
                                            if (string.IsNullOrWhiteSpace(input))
                                            {
                                                name = "Sin nombre";
                                                Console.Clear();
                                                user_Menu_name = false;
                                                
                                            }
                                            else {
                                                name = input;
                                                Console.Clear();
                                                user_Menu_name = false;
                                                
                                            }
                                        }
                                        while (user_Menu_name != false);
                                        do
                                        {
                                            gui.date_interface();
                                            input = Console.ReadLine();
                                            //verifica que la entrada no sea nula
                                            if (string.IsNullOrEmpty(input))
                                            {
                                                error.enter_value();
                                                user_Menu_date = true;
                                            }
                                            else
                                            {
                                                //si la entrada contiene algo verifica que sea un tipo date
                                                if (DateTime.TryParseExact(input, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                                                {
                                                    // La cadena se analizó correctamente como fecha
                                                    u_date = date.ToString("dd-MM-yyyy");
                                                    Console.Clear();
                                                    user_Menu_date = false;
                                                    
                                                }
                                                else
                                                {
                                                    // La cadena no se pudo analizar como fecha
                                                    error.date_error();
                                                    user_Menu_date = true;
                                                }
                                            }
                                        }
                                        while (user_Menu_date != false);
                                        do
                                        {
                                          
                                            gui.priority_interface();
                                            input = Console.ReadLine();
                                            //verifica que la entrada no sea nula
                                            if (string.IsNullOrEmpty(input))
                                            {
                                                error.enter_value();
                                                user_Menu_priority = true;
                                            }
                                            else
                                            {
                                                //verifica que la entrada sea un numero
                                                if (int.TryParse(input, out select))
                                                {
                                                    select = int.Parse(input);
                                                    //verifica que el numero no este fuera de rango
                                                    if (select >= 1 && select <= 3)
                                                    {
                                                        switch (select)
                                                        {
                                                            case 1:
                                                                priority = "Urgente y crucial";
                                                                task = new Task(name, u_date, priority);
                                                                Console.Clear();
                                                                //dispara el evento
                                                                taskAdded?.Invoke(this, name, u_date, priority);
                                                                user_Menu_priority = false;
                                                                break;
                                                            case 2:
                                                                priority = "Importante pero no Urgente";
                                                                task = new Task(name, u_date, priority);
                                                                Console.Clear();
                                                                //dispara el evento
                                                                taskAdded?.Invoke(this, name, u_date, priority);
                                                                user_Menu_priority = false;
                                                                break;
                                                            case 3:
                                                                priority = "Puede esperar";
                                                                task = new Task(name, u_date, priority);
                                                                Console.Clear();
                                                                //dispara el evento
                                                                taskAdded?.Invoke(this, name, u_date, priority);
                                                                user_Menu_priority = false;
                                                                break;
                                                        }
                                                    }
                                                    else{
                                                        error.valueout_of_range();
                                                        user_Menu_priority = true;
                                                    }
                                                }
                                                else
                                                {
                                                    error.only_numbers();
                                                    user_Menu_priority = true;
                                                }
                                            }

                                        }
                                        while (user_Menu_priority != false);
                                        break;
                                    case 2:
                                        Console.Clear();
                                        do
                                        {
                                           
                                            admin.ViewTasks();
                                            gui.mod_interface();
                                            Console.WriteLine("Si quiere volver al menu principal escriba (-1)");
                                            input = Console.ReadLine();
                                            
                                            //verifica que la entrada no sea nula
                                            if (string.IsNullOrEmpty(input)){
                                                error.enter_value();
                                                user_Menu_task = true;
                                            }
                                            else{
                                                //verifica que la entrada sea un numero
                                                if (int.TryParse(input, out n_tarea)){
                                                    n_tarea = int.Parse(input);
                                                    if (n_tarea == -1 ){
                                                        Console.Clear();
                                                        break;
                                                    }
                                                    //valida que el usuario haya seleccionado una tarea valida
                                                        if (admin.ValidateLines(n_tarea) == false)
                                                    {
                                                        error.non_existence();
                                                        user_Menu_task = true;
                                                    }
                                                    else{
                                                        Console.Clear();
                                                        do
                                                        {
                                                            admin.ViewTasks();
                                                            gui.customice_interface();
                                                            input = Console.ReadLine();
                                                            //verifica que la entrada no sea nula
                                                            if (string.IsNullOrEmpty(input))
                                                            {
                                                                error.enter_value();
                                                                user_Menu_custom = true;
                                                            }
                                                            else{
                                                                //verifica que la entrada sea un numero
                                                                if (int.TryParse(input, out select))
                                                                {
                                                                    select = int.Parse(input);
                                                                    if (select == 0) {
                                                                        user_Menu_custom = false;
                                                                        Console.Clear();
                                                                        break;
                                                                    }
                                                                    //verifica que la entrada este en el rango valido
                                                                    if (select >= 1 && select <= 3)
                                                                    {
                                                                        switch (select){
                                                                            case 0:
                                                                                
                                                                                break;
                                                                            case 1:
                                                                                Console.Clear();
                                                                                admin.ViewTasks();
                                                                                gui.name_interface();
                                                                                name = Console.ReadLine();
                                                                                admin.EditTask(n_tarea, 1, name);
                                                                                Console.Clear();
                                                                                //dispara el evento
                                                                                taskEdited?.Invoke(this, n_tarea, 1, admin.beforeNameEvent, name);
                                                                                break;
                                                                            case 2:
                                                                                Console.Clear();
                                                                                do
                                                                                {
                                                                                    admin.ViewTasks();
                                                                                    gui.date_interface();
                                                                                    input = Console.ReadLine();
                                                                                    //verifica que la entrada no sea nula
                                                                                    if (string.IsNullOrEmpty(input))
                                                                                    {
                                                                                        error.enter_value();
                                                                                        user_Menu_date = true;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        //verifica de que la entrada sea una fecha valida
                                                                                        if (DateTime.TryParseExact(input, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                                                                                        {
                                                                                            // La cadena se analizó correctamente como fecha
                                                                                            admin.EditTask(n_tarea, 2, date.ToString("dd-MM-yyyy"));
                                                                                            Console.Clear();
                                                                                            taskEdited?.Invoke(this, n_tarea, 2, admin.beforeDateEvent, date.ToString("dd-MM-yyyy"));
                                                                                            user_Menu_date = false;

                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            // La cadena no se pudo analizar como fecha
                                                                                            error.date_error();
                                                                                            user_Menu_date = true;
                                                                                        }
                                                                                    }
                                                                                }while (user_Menu_date != false);

                                                                                break;
                                                                            case 3:
                                                                                Console.Clear ();
                                                                                do
                                                                                {
                                                                                    admin.ViewTasks();
                                                                                    gui.priority_interface();
                                                                                    input = Console.ReadLine();
                                                                                    //verifica que la entrada no sea nula
                                                                                    if (string.IsNullOrEmpty(input))
                                                                                    {
                                                                                        error.enter_value();
                                                                                        user_Menu_priority = true;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        //verifica que la entrada es un numero
                                                                                        if (int.TryParse(input, out select))
                                                                                        {
                                                                                            select = int.Parse(input);
                                                                                            //verifica que este en el rango
                                                                                            if (select >= 1 && select <= 3)
                                                                                            {
                                                                                                switch (select)
                                                                                                {
                                                                                                    case 1:
                                                                                                        priority = "Urgente y crucial";
                                                                                                        admin.EditTask(n_tarea, 3, "Urgente y crucial");
                                                                                                        Console.Clear();
                                                                                                        //dispara el evento
                                                                                                        taskEdited?.Invoke(this, n_tarea, 3, admin.beforePriorityEvent, "Urgente y crucial");
                                                                                                        user_Menu_priority = false;
                                                                                                        break;
                                                                                                    case 2:
                                                                                                        admin.EditTask(n_tarea, 3, "Importante pero no Urgente");
                                                                                                        Console.Clear();
                                                                                                        //dispara el evento
                                                                                                        taskEdited?.Invoke(this, n_tarea, 3, admin.beforePriorityEvent, "Importante pero no Urgente");
                                                                                                        user_Menu_priority = false;
                                                                                                        break;
                                                                                                    case 3:
                                                                                                        admin.EditTask(n_tarea, 3, "Puede esperar");
                                                                                                        Console.Clear();
                                                                                                        //dispara el evento
                                                                                                        taskEdited?.Invoke(this, n_tarea, 3, admin.beforePriorityEvent, "Puede esperar");
                                                                                                        user_Menu_priority = false;
                                                                                                        break;
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                error.valueout_of_range();
                                                                                                user_Menu_priority = true;
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            error.only_numbers();
                                                                                            user_Menu_priority = true;
                                                                                        }
                                                                                    }

                                                                                }
                                                                                while (user_Menu_priority != false);
                                                                                break;
                                                                        }
                                                                    }
                                                                    else{
                                                                        error.valueout_of_range();
                                                                        user_Menu_custom = true;
                                                                    }
                                                                }
                                                                else{
                                                                    error.only_numbers();
                                                                    user_Menu_custom = true;
                                                                }
                                                            }

                                                        } while (user_Menu_custom != false);
                                                    }
                                                }
                                                else {
                                                    error.only_numbers();
                                                }
                                            }
                                        }
                                        while(user_Menu_task!=false);
                                        break;
                                    case 3:
                                        Console.Clear();
                                        do {
                                            admin.ViewTasks();
                                            gui.remove_interface();
                                            Console.WriteLine("Si quiere volver al menu principal escriba (-1)");
                                            input = Console.ReadLine();
                                            //verifica que la entrada no sea nula
                                            if (string.IsNullOrEmpty(input))
                                            {
                                                error.enter_value();
                                                user_Menu_remove = true;
                                            }
                                            else {
                                                //verifica que la entrada sea un numero
                                                if (int.TryParse(input, out n_tarea))
                                                {

                                                    n_tarea = int.Parse(input);
                                                    if (n_tarea == -1)
                                                    {
                                                        Console.Clear();
                                                        break;
                                                    }
                                                    if (admin.ValidateLines(n_tarea) == false)
                                                    {
                                                        error.non_existence();
                                                        user_Menu_remove = true;
                                                    }
                                                    else
                                                    {
                                                       
                                                        admin.DeleteTask(n_tarea);
                                                        //dispara el evento
                                                        taskDeleted?.Invoke(this, n_tarea, admin.deletedTask);
                                                    }
                                                }
                                                else {
                                                    error.only_numbers();
                                                    user_Menu_custom = true;
                                                }
                                            }
                                        } while (user_Menu_remove != false);
                                        break;
                                    case 4:
                                        Console.Clear();
                                        do
                                        {
                                            
                                            gui.remove_interface();
                                            Console.WriteLine("Si quiere volver al menu principal escriba (-1)");
                                            input = Console.ReadLine();
                                            
                                            if (input.Equals("-1")){
                                                Console.Clear();
                                                break;
                                            }
                                            if (string.IsNullOrEmpty(input))
                                            {
                                                error.enter_value();
                                                user_Menu_remove = true;
                                            }
                                            else
                                            {
                                                admin.SearchTasksByName(input);
                                            }
                                        } while (user_Menu_remove != false);
                                        break;
                                }
                            
                            }
                            else{
                                error.valueout_of_range();
                                user_Menu = true;
                            }
                            
                        }
                        else{
                        error.only_numbers();
                        user_Menu = true;
                        }
                }

                } while (user_Menu != false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en GUI: " + ex.Message);
            }
        }
    }
}