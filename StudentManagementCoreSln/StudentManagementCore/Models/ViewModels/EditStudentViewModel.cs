using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementCore.Models.ViewModels
{
    public class EditStudentViewModel : StudentCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingPhotopath { get; set; }
    }
}
