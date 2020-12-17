using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural
{
    /*
        A Composite is a pattern that is useful anytime you may need to selectively treat 
        a group of objects that are part of a hierarchy as "the same" when they are in fact different. 
        Typically the examples used talk in terms of treating leaves and nodes the same, 
        but the pattern can also be extended to heterogeneous lists. 

        Exlanation 2:
        The composite design pattern is used when two classes have a common parent, 
        and one sibling can contain objects of its own and its sibling’s type. 
        For example, a file and a folder are types of data (parent abstract class), 
        but a folder can contain many files and folders.

        
        Component: The abstract parent class.
        Leaf: The child class, which is the smaller sibling and cannot contain anything (e.g., a file).
        Composite: The child class which can contain itself and the leaf (e.g., a folder).

     */
    public abstract class Data
    {
        protected int size;
        public abstract int getSize();
    }
    public class File : Data
    {
        public File(int i)
        {
            size = i;
        }

        public override int getSize()
        {
            return size;
        }
    }
    public class Folder : Data
    {
        List<Data> d = new List<Data>();
        public void Add(Data f)
        {
            this.d.Add(f);
        }

        public override int getSize()
        {
            int sum = 0;
            for (int i = 0; i < d.Count; i++)
                sum = sum + d[i].getSize();

            return sum;
        }
    }

    public class DemoComposite
    {
        public void Execute()
        {
            // Folder 1 -> file1, file2 and folder2:
            var folder1 = new Folder();
            var f1 = new File(5);
            var f2 = new File(10);
            folder1.Add(f1);
            folder1.Add(f2);

            var folder2 = new Folder();

            var f3 = new File(5);
            folder2.Add(f3);

            folder1.Add(folder2);

            // Folder 3 -> f4:
            var folder3 = new Folder();
            var f4 = new File(10);
            folder3.Add(f4);


            int size = folder3.getSize();
            int size2 = f4.getSize();
        }
    }
}
