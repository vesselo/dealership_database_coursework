using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sql_lab3
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new authorization());
            Application.Run(new main_menu());
            //Application.Run(new customers_suppliers());
            //Application.Run(new orders());
            //Application.Run(new cars_available());
            //Application.Run(new car_maintenance());
            //Application.Run(new employees());
            //Application.Run(new registration());
            //Application.Run(new add_employee());
            //Application.Run(new add_supplier());
            //Application.Run(new new_order());
            //Application.Run(new mm());
        }
    }
}
