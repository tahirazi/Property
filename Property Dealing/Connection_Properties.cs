using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Property_Dealing
{
    class Connection_Properties
    {
        public static string SrvName = @".";
        public static string DbName = @"saiban";
        public static string UsrName = "sa";
        public static string Pasword = "123";
        public static string GetConnectionString()
        {
            return "Data Source=" + SrvName + "; initial catalog=" + DbName + "; User ID=" + UsrName + "; Password=" + Pasword + ";";
        }
    }
}
