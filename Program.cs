﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
       
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());
            Application.Run(new MyForm());
            
        }
    }
}
