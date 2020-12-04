using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.Structural;
using Newtonsoft.Json;

namespace DesignPatterns.Creational
{

    public class DbConf
    {
        public string Ip { get; set; }
    }

    public static class DatabaseFactory
    {
        public static Database GetMsSql(string database, string serverIp, string username, string password)
        {
            var fileContent = System.IO.File.ReadAllText("c:\\txt.json");
            var c = JsonConvert.DeserializeObject<DbConf>(fileContent);

            return new Database(database, c.Ip, username, password, Database.DatabaseType.MsSql);
        }

        public static Database GetMySql(string database, string serverIp, string username, string password)
        {
            return new Database(database, serverIp, username, password, Database.DatabaseType.MySql);
        }

        public static Database GetFile(string database)
        {
            return new Database(database, null, null, null, Database.DatabaseType.File);
        }
    }

    public class Database
    {
        public enum DatabaseType
        {
            MsSql,
            MySql,
            File
        }

        private DatabaseType typeOfDb;
        private string database;
        private string serverIp;
        private string username;
        private string password;

        public Database(string database, string serverIp, string username, string password, DatabaseType typeOfDb)
        {
            this.database = database;
            this.serverIp = serverIp;
            this.username = username;
            this.password = password;
            this.typeOfDb = typeOfDb;
        }


        public static Database GetMsSql(string database, string serverIp, string username, string password)
        {
            return new Database(database, serverIp, username, password, DatabaseType.MsSql);
        }

        public static Database GetMySql(string database, string serverIp, string username, string password)
        {
            return new Database(database, serverIp, username, password, DatabaseType.MySql);
        }

        public static Database GetFile(string database)
        {
            return new Database(database, null, null, null, DatabaseType.File);
        }


        public void ExecuteQuery(string sqlQuery)
        {
            Console.WriteLine("Executing query... ");
        }
    }

    public class FactoryMethodDemo
    {
        public void Run()
        {
            var db1 =
                DatabaseFactory.GetFile("ebrosur");

            db1.ExecuteQuery("INSERT INTO MyTAble....");
        }
    }
}
