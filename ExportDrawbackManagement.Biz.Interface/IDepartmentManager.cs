using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IDepartmentManager
    {
       DataSet getDepartments();
       void addDepartment(T_Department item);
       void updateDepartment(T_Department item);
       void DeleteDepartmentById(int id);
       
    }
}
