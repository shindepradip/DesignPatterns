using DesignPatterns.Behavioral;
using DesignPatterns.Creational;
using DesignPatterns.Structural;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DesignPatterns
{
    class Clients
    {
        #region Creational Design patterns client calls
        public static class Creational : Object
        {

            /// <summary>
            /// Rating(Frequency of use) - 4/5
            /// Ensure a class has only one instance and provide a global point of access to it.    
            /// *Real Example -> Singleton pattern as a LoadBalancing object. 
            /// Only a single instance (the singleton) of the class can be created because servers may dynamically 
            /// come on- or off-line and every request must go throught the one object that has knowledge about 
            /// the state of the (web) farm. 
            /// </summary>
            public static void Singleton()
            {
                LoadBalancer b1 = LoadBalancer.GetLoadBalancer();
                LoadBalancer b2 = LoadBalancer.GetLoadBalancer();
                LoadBalancer b3 = LoadBalancer.GetLoadBalancer();
                LoadBalancer b4 = LoadBalancer.GetLoadBalancer();

                // Same instance?
                if (b1 == b2 && b2 == b3 && b3 == b4)
                {
                    Console.WriteLine("Same instance\n");
                }

                // Load balance 15 server requests
                LoadBalancer balancer = LoadBalancer.GetLoadBalancer();
                for (int i = 0; i < 15; i++)
                {
                    string server = balancer.Server;
                    Console.WriteLine("Dispatch Request to: " + server);
                }
            }

            /// <summary>
            /// Rating(Frequency of use) - 5/5
            /// Define an interface for creating an object, but let subclasses decide which class to instantiate. 
            /// Factory Method lets a class defer instantiation to subclasses. 
            /// *Real Example -> Factory method offering flexibility in creating different documents. 
            /// The derived Document classes Report and Resume instantiate extended versions of the Document class. 
            /// Here, the Factory Method is called in the constructor of the Document base class. 
            /// </summary>
            public static void Factory()
            {
                // Note: constructors call Factory Method
                Document[] documents = new Document[2];

                documents[0] = new Resume();
                documents[1] = new Report();

                // Display document pages
                foreach (Document document in documents)
                {
                    Console.WriteLine("\n" + document.GetType().Name + "--");
                    foreach (Page page in document.Pages)
                    {
                        Console.WriteLine(" " + page.GetType().Name);
                    }
                }

            }

            /// <summary>
            /// Rating(Frequency of use) - 5/5
            /// Provide an interface for creating families of related or dependent objects without specifying their concrete classes. 
            /// *Real Example -> the creation of different animal worlds for a computer game using different factories. 
            /// Although the animals created by the Continent factories are different, the interactions among the animals remain the same.
            /// </summary>
            public static void AbstractFactory()
            {
                // Create and run the African animal world
                ContinentFactory africa = new AfricaFactory();
                AnimalWorld world = new AnimalWorld(africa);
                world.RunFoodChain();

                // Create and run the American animal world
                ContinentFactory america = new AmericaFactory();
                world = new AnimalWorld(america);
                world.RunFoodChain();
            }

            /// <summary>
            /// Rating(Frequency of use) - 2/5
            /// Separate the construction of a complex object from its representation so that the same construction process 
            /// can create different representations. 
            /// *Real Example -> Builder pattern in which different vehicles are assembled in a step-by-step fashion. 
            /// The Shop uses VehicleBuilders to construct a variety of Vehicles in a series of sequential steps. 
            /// </summary>
            public static void Builder()
            {
                VehicleBuilder builder;

                // Create shop with vehicle builders
                Shop shop = new Shop();

                // Construct and display vehicles
                builder = new ScooterBuilder();
                shop.Construct(builder);
                builder.Vehicle.Show();

                builder = new CarBuilder();
                shop.Construct(builder);
                builder.Vehicle.Show();

                builder = new MotorCycleBuilder();
                shop.Construct(builder);
                builder.Vehicle.Show();
            }


            /// <summary>
            /// Rating(Frequency of use) - 3/5
            /// Specify the kind of objects to create using a prototypical instance, and create new objects by copying this prototype. 
            /// *Real Example -> Prototype pattern in which new Color objects are created by copying pre-existing, 
            /// user-defined Colors of the same type. 
            /// </summary>
            public static void Prototype()
            {
                ColorManager colormanager = new ColorManager();

                // Initialize with standard colors
                colormanager["red"] = new Color(255, 0, 0);
                colormanager["green"] = new Color(0, 255, 0);
                colormanager["blue"] = new Color(0, 0, 255);

                // User adds personalized colors
                colormanager["angry"] = new Color(255, 54, 0);
                colormanager["peace"] = new Color(128, 211, 128);
                colormanager["flame"] = new Color(211, 34, 20);

                // User clones selected colors
                Color color1 = colormanager["red"].Clone() as Color;
                Color color2 = colormanager["peace"].Clone() as Color;
                Color color3 = colormanager["flame"].Clone() as Color;

            }
        }
        #endregion

        #region Structural Design Patterns client calls
        public static class Structural
        {
            /// <summary>
            /// Rating(Frequency of use) -> 4/5 
            /// Convert the interface of a class into another interface clients expect. 
            /// Adapter lets classes work together that couldn't otherwise because of incompatible interfaces. 
            /// *Real Example -> demonstrates the use of a legacy chemical databank. 
            /// Chemical compound objects access the databank through an Adapter interface. 
            /// </summary>
            public static void Adaptor()
            {
                // Non-adapted chemical compound
                Compound unknown = new Compound("Unknown");
                unknown.Display();

                // Adapted chemical compounds
                Compound water = new RichCompound("Water");
                water.Display();

                Compound benzene = new RichCompound("Benzene");
                benzene.Display();

                Compound ethanol = new RichCompound("Ethanol");
                ethanol.Display();
            }

            /// <summary>
            /// Rating(Frequency of use) -> 3/5 
            /// Decouple an abstraction from its implementation so that the two can vary independently. 
            /// *Real Example -> Bridge pattern in which a BusinessObject abstraction is decoupled from the implementation in DataObject. 
            /// The DataObject implementations can evolve dynamically without changing any clients. 
            /// </summary>
            public static void Bridge()
            {
                // Create RefinedAbstraction
                Customers customers = new Customers("Chicago");

                // Set ConcreteImplementor
                customers.Data = new CustomersData();

                // Exercise the bridge
                customers.Show();
                customers.Next();
                customers.Show();
                customers.Next();
                customers.Show();
                customers.Add("Henry Velasquez");

                customers.ShowAll();
            }

            /// <summary>
            /// Rating(Frequency of use) -> 4/5 
            /// Compose objects into tree structures to represent part-whole hierarchies. 
            /// Composite lets clients treat individual objects and compositions of objects uniformly.
            /// *Real Example -> Composite pattern used in building a graphical tree structure made up of primitive nodes 
            /// (lines, circles, etc) and composite nodes (groups of drawing elements that make up more complex elements). 
            /// </summary>
            public static void Composite()
            {
                // Create a tree structure 
                CompositeElement root =
                  new CompositeElement("Picture");
                root.Add(new PrimitiveElement("Red Line"));
                root.Add(new PrimitiveElement("Blue Circle"));
                root.Add(new PrimitiveElement("Green Box"));

                // Create a branch
                CompositeElement comp =
                  new CompositeElement("Two Circles");
                comp.Add(new PrimitiveElement("Black Circle"));
                comp.Add(new PrimitiveElement("White Circle"));
                root.Add(comp);

                // Add and remove a PrimitiveElement
                PrimitiveElement pe =
                  new PrimitiveElement("Yellow Line");
                root.Add(pe);
                root.Remove(pe);

                // Recursively display nodes
                root.Display(1);

            }

            /// <summary>
            /// Rating(Frequency os use) -> 3/5 
            /// Attach additional responsibilities to an object dynamically. 
            /// Decorators provide a flexible alternative to subclassing for extending functionality. 
            /// *Real Example ->  Decorator pattern in which 'borrowable' functionality is added to existing library items (books and videos). 
            /// </summary>
            public static void Decorator()
            {
                // Create book
                Book book = new Book("Worley", "Inside ASP.NET", 10);
                book.Display();

                // Create video
                Video video = new Video("Spielberg", "Jaws", 23, 92);
                video.Display();

                // Make video borrowable, then borrow and display
                Console.WriteLine("\nMaking video borrowable:");

                Borrowable borrowvideo = new Borrowable(video);
                borrowvideo.BorrowItem("Customer #1");
                borrowvideo.BorrowItem("Customer #2");

                borrowvideo.Display();

            }

            /// <summary>
            /// Rating(Frequency of use) - 5/5
            /// Provide a unified interface to a set of interfaces in a subsystem. 
            /// Façade defines a higher-level interface that makes the subsystem easier to use. 
            /// *Real Example -> Facade pattern as a MortgageApplication object which provides a simplified interface 
            /// to a large subsystem of classes measuring the creditworthyness of an applicant.
            /// </summary>
            public static void Facade()
            {
                // Facade
                //Injecting different sub systems/classes via constructor...
                //Dependancy injection pattern.
                IMortgage mortgage = new Mortgage(new Bank(), new Loan(), new Credit());

                // Evaluate mortgage eligibility for customer
                ICustomer customer = new Customer("Ann McKinsey");
                bool eligible = mortgage.IsEligible(customer, 125000);

                Console.WriteLine("\n" + customer.Name +
                    " has been " + (eligible ? "Approved" : "Rejected"));

            }

            /// <summary>
            /// Rating(Frequency of use) -> 1/5
            /// Use sharing to support large numbers of fine-grained objects efficiently. 
            /// *Real Example -> Flyweight pattern in which a relatively small number of Character objects is shared many times 
            /// by a document that has potentially many characters. 
            /// </summary>
            public static void Flyweight()
            {
                // Build a document with text
                string document = "AAZZBBZB";
                char[] chars = document.ToCharArray();

                CharacterFactory factory = new CharacterFactory();

                // extrinsic state
                int pointSize = 10;

                // For each character use a flyweight object
                foreach (char c in chars)
                {
                    pointSize++;
                    Character character = factory.GetCharacter(c);
                    character.Display(pointSize);
                }
            }

            /// <summary>
            /// Rating(Frequency of use) - 4/5
            /// Provide a surrogate or placeholder for another object to control access to it. 
            /// Proxy pattern for a Math object represented by a MathProxy object. 
            /// Different Example WCF/Web service Client proxy object.
            /// </summary>
            public static void Proxy()
            {
                // Create math proxy
                MathProxy proxy = new MathProxy();

                // Do the math
                Console.WriteLine("4 + 2 = " + proxy.Add(4, 2));
                Console.WriteLine("4 - 2 = " + proxy.Sub(4, 2));
                Console.WriteLine("4 * 2 = " + proxy.Mul(4, 2));
                Console.WriteLine("4 / 2 = " + proxy.Div(4, 2));// Create math proxy
            }
        }
        #endregion

        #region Behavioral Design Patterns client calls
        public static class Behavioral
        {
            /// <summary>
            /// Avoid coupling the sender of a request to its receiver by giving more than one object a chance to handle the request. 
            /// Chain the receiving objects and pass the request along the chain until an object handles it. 
            /// *Real Example -> Chain of Responsibility pattern in which several linked managers and 
            /// executives can respond to a purchase request or hand it off to a superior. 
            /// Each position has can have its own set of rules which orders they can approve.
            /// </summary>
            public static void ChainOfResp()
            {
                // Setup Chain of Responsibility
                Approver larry = new Director();
                Approver sam = new VicePresident();
                Approver tammy = new President();

                larry.SetSuccessor(sam);
                sam.SetSuccessor(tammy);

                // Generate and process purchase requests
                Purchase p = new Purchase(2034, 350.00, "Assets");
                larry.ProcessRequest(p);

                p = new Purchase(2035, 32590.10, "Project X");
                larry.ProcessRequest(p);

                p = new Purchase(2036, 122100.00, "Project Y");
                larry.ProcessRequest(p);

            }

            /// <summary>
            /// Rating(Frequency of use) -> 4/5
            /// Encapsulate a request as an object, thereby letting you parameterize clients with different requests, 
            /// queue or log requests, and support undoable operations. 
            /// *Real Example -> Command pattern used in a simple calculator with unlimited number of undo's and redo's. 
            /// Note that in C#  the word 'operator' is a keyword. Prefixing it with '@' allows using it as an identifier. 
            /// </summary>
            public static void Command()
            {
                // Create user and let her compute
                User user = new User();

                // User presses calculator buttons
                user.Compute('+', 100);
                user.Compute('-', 50);
                user.Compute('*', 10);
                user.Compute('/', 2);

                // Undo 4 commands
                user.Undo(4);

                // Redo 3 commands
                user.Redo(3);
            }

            public static void Interpreter()
            {
            }

            /// <summary>
            /// Rating(Frequency of use) -> 5/5
            /// Provide a way to access the elements of an aggregate object sequentially without exposing its underlying representation. 
            /// *Real Example -> Iterator pattern which is used to iterate over a collection of items and 
            /// skip a specific number of items each iteration. 
            /// </summary>
            public static void Iterator()
            {
                // Build a collection
                Collection collection = new Collection();
                collection[0] = new Item("Item 0");
                collection[1] = new Item("Item 1");
                collection[2] = new Item("Item 2");
                collection[3] = new Item("Item 3");
                collection[4] = new Item("Item 4");
                collection[5] = new Item("Item 5");
                collection[6] = new Item("Item 6");
                collection[7] = new Item("Item 7");
                collection[8] = new Item("Item 8");

                // Create iterator
                Iterator iterator = collection.CreateIterator();

                // Skip every other item
                iterator.Step = 2;

                Console.WriteLine("Iterating over collection:");

                for (Item item = iterator.First();
                    !iterator.IsDone; item = iterator.Next())
                {
                    Console.WriteLine(item.Name);
                }
            }

            /// <summary>
            /// Rating(Frequency of use) -> 2/5
            /// Define an object that encapsulates how a set of objects interact. 
            /// Mediator promotes loose coupling by keeping objects from referring to each other explicitly, 
            /// and it lets you vary their interaction independently. 
            /// *Real Example -> Mediator pattern facilitating loosely coupled communication between different Participants 
            /// registering with a Chatroom. The Chatroom is the central hub through which all communication takes place. 
            /// At this point only one-to-one communication is implemented in the Chatroom, but would be trivial to change to one-to-many. 
            /// </summary>
            public static void Mediator()
            {
                // Create chatroom
                Chatroom chatroom = new Chatroom();

                // Create participants and register them
                Participant George = new Beatle("George");
                Participant Paul = new Beatle("Paul");
                Participant Ringo = new Beatle("Ringo");
                Participant John = new Beatle("John");
                Participant Yoko = new NonBeatle("Yoko");

                chatroom.Register(George);
                chatroom.Register(Paul);
                chatroom.Register(Ringo);
                chatroom.Register(John);
                chatroom.Register(Yoko);

                // Chatting participants
                Yoko.Send("John", "Hi John!");
                Paul.Send("Ringo", "All you need is love");
                Ringo.Send("George", "My sweet Lord");
                Paul.Send("John", "Can't buy me love");
                John.Send("Yoko", "My sweet love");
            }

            public static void Memento()
            {
            }

            /// <summary>
            /// Rating(Frequency of use) -> 5/5
            /// Define a one-to-many dependency between objects so that when one object changes state, 
            /// all its dependents are notified and updated automatically. 
            /// *Real Example -> Observer pattern in which registered investors are notified every time a stock changes value. 
            /// </summary>
            public static void Observer()
            {
                // Create IBM stock and attach investors
                IBM ibm = new IBM("IBM", 120.00);
                ibm.Attach(new Investor("Sorros"));
                ibm.Attach(new Investor("Berkshire"));

                // Fluctuating prices will notify investors
                ibm.Price = 120.10;
                ibm.Price = 121.00;
                ibm.Price = 120.50;
                ibm.Price = 120.75;
            }

            /// <summary>
            /// Rating(Frequency of use) -> 3/5
            /// Allow an object to alter its behavior when its internal state changes. The object will appear to change its class. 
            /// *Real Example -> State pattern which allows an Account to behave differently depending on its balance. 
            /// The difference in behavior is delegated to State objects called RedState, SilverState and GoldState. 
            /// These states represent overdrawn accounts, starter accounts, and accounts in good standing. 
            /// </summary>
            public static void State()
            {
                // Open a new account
                Account account = new Account("Jim Johnson");

                // Apply financial transactions
                account.Deposit(500.0);
                account.Deposit(300.0);
                account.Deposit(550.0);
                account.PayInterest();
                account.Withdraw(2000.00);
                account.Withdraw(1100.00);
            }

            /// <summary>
            /// Rating(Frequency of use) -> 4/5
            /// Define a family of algorithms, encapsulate each one, and make them interchangeable. 
            /// Strategy lets the algorithm vary independently from clients that use it. 
            /// *Real Example -> Strategy pattern which encapsulates sorting algorithms in the form of sorting objects. 
            /// This allows clients to dynamically change sorting strategies including Quicksort, Shellsort, and Mergesort. 
            /// </summary>
            public static void Strategy()
            {
                // Two contexts following different strategies
                SortedList studentRecords = new SortedList();

                studentRecords.Add("Samual");
                studentRecords.Add("Jimmy");
                studentRecords.Add("Sandra");
                studentRecords.Add("Vivek");
                studentRecords.Add("Anna");

                studentRecords.SetSortStrategy(new QuickSort());
                studentRecords.Sort();

                studentRecords.SetSortStrategy(new ShellSort());
                studentRecords.Sort();

                studentRecords.SetSortStrategy(new MergeSort());
                studentRecords.Sort();
            }

            /// <summary>
            /// Rating(Frequency of use) -> 3/5
            /// Define the skeleton of an algorithm in an operation, deferring some steps to subclasses. 
            /// Template Method lets subclasses redefine certain steps of an algorithm without changing the algorithm's structure. 
            /// Real Example -> Template method named Run() which provides a skeleton calling sequence of methods. 
            /// Implementation of these steps are deferred to the CustomerDataObject subclass which implements the Connect,
            /// Select, Process, and Disconnect methods. 
            /// </summary>
            public static void TemplateMethod()
            {
                DataAccessObject daoCategories = new Categories();
                daoCategories.Run();

                DataAccessObject daoProducts = new Products();
                daoProducts.Run();

            }

            public static void Visitor()
            {
            }
        }
        #endregion
    }
}
