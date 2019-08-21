using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Fitness.Web
{
    public static class ConnectionStringManager
    {
        public static string GetConnectionString(string alias)
        {
            Configuration config;
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return config.ConnectionStrings.ConnectionStrings[alias].ConnectionString;
            //return ConfigurationManager.ConnectionStrings[alias].ConnectionString;
        }


        public static void AddConnectionString(string alias, string connectinoString)
        {
            ConnectionStringSettings newConnectionString = new ConnectionStringSettings() { Name = alias, ConnectionString = connectinoString };
            Configuration config;
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings.Add(newConnectionString);
            config.Save();
        }
    }
}