using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural
{
    /*
    
    #### 1
     The primary purpose of the adapter pattern is to change the interface 
        of class/library A to the expectations of client B. 
        The typical implementation is a wrapper class or set of classes. 
        The purpose is not to facilitate future interface changes, 
        but current interface incompatibilities.
     
     */


    public class MySqlRecord
    {
        public string Id { get; set; }
        public string NameSurname { get; set; }
    }
    public class MsSqlRecord
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class ExcelRow
    {
        public string Col0 { get; set; }
        public string Col1 { get; set; }
        public string Col2 { get; set; }
    }

    public class MySqlSource
    {
        private List<MySqlRecord> records = new List<MySqlRecord>()
        {
            new MySqlRecord()
            {
                Id = "1",
                NameSurname = "Ali Can",
            },
            new MySqlRecord()
            {
                Id = "2",
                NameSurname = "Mustafa Ayas",
            }
        };

        public List<MySqlRecord> GetList() => records;
    }
    public class MsSqlSource
    {
        private List<MsSqlRecord> records = new List<MsSqlRecord>()
        {
            new MsSqlRecord()
            {
                Id = 1,
                Name= "Ahmet",
                Surname = "Can",
            },
            new MsSqlRecord()
            {
                Id = 2,
                Name= "Mehmet",
                Surname = "Ayas",
            }
        };

        public List<MsSqlRecord> GetList() => records;
    }
    public class ExcelSource
    {
        private List<ExcelRow> records = new List<ExcelRow>()
        {
            new ExcelRow()
            {
                Col0 = "1",
                Col1 = "Ömer",
                Col2 = "Yılmaz",
            },

        };

        public List<ExcelRow> GetList() => records;
    }

    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class DataAdapter
    {
        private MsSqlSource msSqlSource = new MsSqlSource();
        private MySqlSource mySqlSource = new MySqlSource();
        private ExcelSource excelSource = new ExcelSource();

        public List<Person> GetList()
        {
            var list = new List<Person>();
            var mssqlList = msSqlSource.GetList();
            list.AddRange(mssqlList.Select(x => new Person()
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname
            }));


            var mysqlList = mySqlSource.GetList();
            list.AddRange(mysqlList.Select(x => new Person()
            {
                Id = long.Parse(x.Id),
                Name = x.NameSurname.Split(' ').FirstOrDefault(),
                Surname = x.NameSurname.Split(' ').LastOrDefault(),
            }));


            var excelList = excelSource.GetList();
            list.AddRange(excelList.Select(x => new Person()
            {
                Id = long.Parse(x.Col0),
                Name = x.Col1,
                Surname = x.Col2,
            }));

            return list;
        }
    }
}
