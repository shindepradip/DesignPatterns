using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    public class Page1
    {
        public Page1()
        {
            this.TestMethod();
        }

        public virtual void TestMethod()
        {
            Console.WriteLine("I am virtual");
        }
    }

    public class Page2 : Page1
    {
        public Page2()
        {
            this.TestMethod();
        }

        public override void TestMethod()
        {
            Console.WriteLine("I am derived");
        }
    }




    public class Logger
    {
        private static Logger _Instance;

        // Lock synchronization object
        private static object syncLock = new object();

        private Logger()
        {

        }

        public static Logger GetLogger()
        {
            if (_Instance == null)
            {
                lock (syncLock)
                {
                    if (_Instance == null)
                    {
                        Console.WriteLine("I am new");
                        _Instance = new Logger();
                    }
                }
            }
            else
            {
                Console.WriteLine("I am old");
            }

            return _Instance;
        }


    }



    interface IStudentRepository
    {
        void studentinfo();
    }

    static class abc1
    {
        public static void studentinfo(this StudentService str)
        {
            Console.WriteLine("string1");
        }
    }

    class StudentRepository : IStudentRepository
    {
        public void studentinfo()
        {
            //databse call etc.......
            Console.WriteLine("I am student repository");
        }
    }

    class TestRepository : IStudentRepository
    {
        public IEnumerable<string> getdetails(Func<string, bool> predicate)
        {
            List<string> str = new List<string>();
            return str.Where(predicate);

            //databse call etc.......
            Console.WriteLine("I am student repository");
            StudentService stud = new StudentService();
            stud.studentinfo();
        }

        public void studentinfo()
        {
            throw new NotImplementedException();
        }
    }

    class StudentService : IDisposable
    {
        private readonly IStudentRepository _repository;

        public StudentService()
        { }

        public int a;
        public int b;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public void studentinfo()
        {
            Console.WriteLine("I am non extension");
            //_repository.studentinfo();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    class studenttest : StudentService
    {

    }



    public class ABC { }

    public class PQR { }



    interface I1
    {
        void abc();
    }

    interface I2
    {
        void abc();
    }

    interface I3 : I1, I2
    {

    }

    class test
    {
        //void I1.abc()
        //{
        //    Console.WriteLine("I am I1");
        //}

        //void I2.abc()
        //{
        //    Console.WriteLine("I am I2");
        //}
    }

    class test1 : test
    {
        //void I1.abc()
        //{
        //    Console.WriteLine("I am I1");
        //}

        //void I2.abc()
        //{
        //    Console.WriteLine("I am I2");
        //}
    }

    class Program
    {
        static void Main(string[] args)
        {
            Page1 p1 = new Page2();
            p1.TestMethod();

            Logger.GetLogger();
            Logger.GetLogger();
            Logger.GetLogger();
            Console.Read();

            List<string> str = new List<string> 
            {
                "Pradip",
                "Pradip",
                "Ashwini"
            };

            StudentService obj = new StudentService()
            {
                a = 12,
                b = 13
            };

            var lst = str.Where(item => item.Contains("Pradip"));

            StudentService stud = new StudentService();
            stud.studentinfo();
            //I1 obj = new test();
            //obj.abc();

            //I2 obj1 = new test();
            //obj1.abc();

            Console.Read();
        }

        static void show()
        {

            StudentService stud = new StudentService(new StudentRepository());
            stud.studentinfo();

            StudentService test = new StudentService(new TestRepository());
            test.studentinfo();
        }
    }
}
