using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational
{

    public enum AvailableDatabase
    {
        MsSql,
        MySql
    }

    public interface IDatabaseFactory
    {
        IDatabase GetDatabase();
    }

    public class MySqlFactory : IDatabaseFactory
    {
        public IDatabase GetDatabase()
        {
            return new MySqlAdapter();
        }
    }

    public class MsSqlFactory : IDatabaseFactory
    {
        public IDatabase GetDatabase()
        {
            return new MsSqlAdapter();
        }
    }

    public class DatabaseFactory11
    {
        public static readonly IDictionary<AvailableDatabase, Func<IDatabaseFactory>> Creators =
            new Dictionary<AvailableDatabase, Func<IDatabaseFactory>>()
            {
                { AvailableDatabase.MsSql, () => new MsSqlFactory() },
                { AvailableDatabase.MySql, () => new MySqlFactory() },

                // Reflection....
            };

        public static IDatabaseFactory CreateInstance(AvailableDatabase enumModuleName)
        {
            return Creators[enumModuleName]();
        }
    }




    public interface IDatabase
    {
        void Add();
        void Delete();
        void Update();
    }

    public class MySqlAdapter : IDatabase
    {
        public void Add()
        {
        }

        public void Delete()
        {
        }

        public void Update()
        {
        }
    }

    public class MsSqlAdapter : IDatabase
    {
        public void Add()
        {
        }

        public void Delete()
        {
        }

        public void Update()
        {
        }
    }



    public class MyBusiness
    {
        private IDatabase db;
        public MyBusiness(IDatabaseFactory factory)
        {
            this.db = factory.GetDatabase();
        }

        public void SaveData()
        {
            this.db.Add();
        }

        public void DeleteExistingCustomer()
        {
            this.db.Delete();
        }
    }


}
