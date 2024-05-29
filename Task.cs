using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskM
{
    class Task
    {
        private string Name;
        private string Date;
        private string Priority;
        private string Content;
        Controller admin = new Controller();
        public Task(string name, string date, string priority)
        {
            this.Name = name;
            this.Date = date;
            this.Priority = priority;
            Content = name + "|"+ date + "|" + priority;
            admin.AddTask(Content);
        }
    }
}
