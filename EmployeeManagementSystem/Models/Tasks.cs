using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Tasks
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string TaskDescription { get; set; }
        public string Status { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
