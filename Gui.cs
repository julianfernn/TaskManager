using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskM
{
    public class GUI
    {
        public void user_interface()
        {
            try
            {
                Console.WriteLine("---------------------ADMINISTRADOR DE TAREAS---------------------\n" +
                             "1.Agregar tareas\n" +
                             "2.Modificar Tarea\n" +
                             "3.Borrar tareas\n" +
                             "4.Filtrar por nombre\n"+
                             "0.Cerrar programa");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " Error en metodo interface_usuario");
            }

        }
        public void name_interface()
        {
            try
            {
                Console.WriteLine("Ingrese el nombre de la tarea:\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " Error en metodo interface_nombre");
            }


        }

        public void date_interface()
        {
            try
            {
                Console.WriteLine("Ingrese la fecha de vencimiento en formato dd-mm-yyyy.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Error en interface_vencimiento");
            }
        }
        public void priority_interface()
        {
            try
            {
                Console.WriteLine("Prioridad:\n1.Urgente y crucial\n2.Importante pero no Urgente\n3.Puede esperar");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Error en interface_prioridad");
            }
        }
        public void customice_interface()
        {
            try
            {
                Console.WriteLine("Seleccione lo que desea modificar:\n1.Nombre\n2.Fecha\n3.Prioridad\n0.Atras");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Error en interface_prioridad");
            }
        }
        public void mod_interface()
        {
            try
            {
                Console.WriteLine("Seleccione la tarea que desea modificar");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Error en interface_modificar");
            }
        }
        public void remove_interface()
        {
            try
            {
                Console.WriteLine("Seleccione la tarea que desea borrar");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Error en remove_interface");
            }
        }
        public void name_filter() {
            try
            {
                Console.WriteLine("Filtrado por nombre, ingrese el nombre");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Error en name_filter");
            }
        }
    }
}
