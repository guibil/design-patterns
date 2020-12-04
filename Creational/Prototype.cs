using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DesignPatterns.Creational
{
    public abstract class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Dept { get; set; }

        public abstract Student Clone1();
        public abstract Student Clone2();
    }

    public class EEStudent : Student
    {
        public override Student Clone1()
        {
            return new EEStudent()
            {
                Name = this.Name,
                Surname = this.Surname,
                Dept = this.Dept
            };
        }

        public override Student Clone2()
        {
            var str = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<EEStudent>(str);
        }
    }


    public class PrototypeDemo
    {
        public void Run()
        {
            int a1 = 1;
            int a2 = 2;
            int a3 = a1;
            a3 = a2;

            string s1 = "Tolunbek";
            string s2 = "Galipbek";
            s2 = s1;
            s2 = "Ali";



            var ee =
                new EEStudent
                {
                    Name = "Ali",
                    Surname = "Can",
                    Dept = "EE"
                };
            var e2 = ee.Clone2();

            // DeepCopy
            // ShallowCopy

            e2.Name = "Ahmet";

        }

    }
}
