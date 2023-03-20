namespace dtp6_contacts
{
    //TODO: implement methods: save, delete , edit, list and new.
    class MainClass
    {
        //Array of Person objects.
        static List<Person> contactList = new List<Person>();
        class Person
        {
            private string persname, surname, birthdate;
            private string[] phone, address;

            public string Persname
            {
                get { return persname; }
                set { persname = value; }
            }

            public string Surname
            {
                get { return surname; }
                set { surname = value; }
            }
            public string[] Phone
            {
                get { return phone; }
                set { phone = value; }
            }
            public string[] Address
            {
                get { return address; }
                set { address = value; }
            }
            public string Birthdate
            {
                get { return birthdate; }
                set { birthdate = value; }
            }


            public Person(string persname, string surname, string[] phone, string[] address, string birthdate)
            {
                this.persname = persname;
                this.surname = surname;
                this.phone = phone;
                this.address = address;
                this.birthdate = birthdate;
            }
        }
        public static void Main(string[] args)
        {
            string[] command;
            Console.WriteLine("Hello and welcome to the contact list\nType 'help' for help.");
            do
            {
                Console.Write($"Command: ");
                command = Console.ReadLine().Split(' ');
                if (command[0] == "quit")
                {
                    Console.WriteLine("Goodbye");
                }
                else if (command[0] == "load")
                {
                    LoadFile(command);
                }
                else if (command[0] == "save")
                {
                    SaveFile(command);
                }
                else if (command[0] == "new")
                {
                    New(command);
                }
                else if (command[0] == "help")
                {
                    Help();
                }
                else if (command[0] == "list")
                {
                    print(command);
                }
                else if (command[0] == "delete")
                {
                    delete(command);
                }
                else if (command[0] == "edit")
                {
                    edit(command);
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command[0]}'");
                }
            } while (command[0] != "quit");
        }
        //

        //adds new person object to the list to be implemented
        private static void New(string[] commandLine)
        {
            if (commandLine.Length < 2)
            {
                string[] ph = new string[1];
                string[] ad = new string[1];
                Console.Write("personal name: ");
                string persname = Console.ReadLine();
                Console.Write("surname: ");
                string surname = Console.ReadLine();
                Console.Write("phone: ");
                ph[0] = Console.ReadLine();
                Console.Write("address: ");
                ad[0] = Console.ReadLine();
                Console.Write("birthdate: ");
                string birthdate = Console.ReadLine();
                contactList.Add(new Person(persname, surname, ph, ad, birthdate));

            }
            else
            {
                try
                {
                    int comlength = commandLine.Length;
                    string tosplit = "";
                    int i = 1;
                    while (i < comlength)
                    {
                        tosplit = tosplit + " " + commandLine[i];
                        i++;
                    }
                    string[] splits = tosplit.Split("|");
                    string[] phonesplits = splits[2].Split(";");
                    string[] addresssplits = splits[3].Split(";");
                    contactList.Add(new Person(splits[0], splits[1], phonesplits, addresssplits, splits[4]));
                }
                catch { Console.WriteLine("the input was not of type name|surname|phone|address|birthdate"); }
            }
        }
        //saves the array of objects to a file
        private static void SaveFile(string[] commandLine)
        {
            string lastFileName;
            if (commandLine.Length < 2)
            {
                lastFileName = "address.lis.txt";
            }
            else { lastFileName = commandLine[1]; }
            {
                using (StreamWriter outfile = new StreamWriter(GetPath(lastFileName)))
                {
                    foreach (Person p in contactList)
                    {

                        if (p != null)
                            outfile.WriteLine($"{p.Persname}|{p.Surname}|{getString(p.Phone)}|{getString(p.Address)}|{p.Birthdate}");
                    }
                }
            }
        }

        //gets a string from a list of strings
        public static string getString(string[] strings)
        {
            string a = "";
            foreach (string b in strings)
            {
                if (b != null && a.Length == 0)
                {
                    a = b;
                }
                else if (b != null)
                {
                    a = a + ";" + b;
                }
            }
            return a;
        }
        //adds person objects from the file to the list of objects
        private static void LoadFile(string[] commandLine)
        {
            string lastFileName;
            {
                if (commandLine.Length < 2)
                {
                    lastFileName = "address.lis.txt";
                }
                else { lastFileName = commandLine[1]; }
                try
                {
                    using (StreamReader infile = new StreamReader(GetPath(lastFileName)))
                    {
                        string line;
                        while ((line = infile.ReadLine()) != null)
                        {
                            string[] attrs = line.Split('|');
                            string[] phones = attrs[2].Split(';');
                            string[] addresses = attrs[3].Split(';');
                            Person person = new Person(attrs[0], attrs[1], phones, addresses, attrs[4]);
                            contactList.Add(person);
                        }
                    }
                }
                catch { Exception file = null;
                    Console.WriteLine("could not find the file at the project directory");
                }
            }
        }

        //returns a string which is path of a textfile in the project directory
        public static string GetPath(string commandLine)
        {
            string lastFileName;
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            lastFileName = commandLine;
            string path = projectDirectory + "\\" + lastFileName;
            return path;
        }
        //prints all the Objects in list or of specfic person
        public static void print(string[] commandLine)
        {
            foreach (Person p in contactList)
            {

                if (commandLine.Length < 2)
                {
                    printPerson(p);
                }
                else
                {
                    if (commandLine[1] == p.Persname)
                    {
                        printPerson(p);
                    }
                }

            }
        }

        //prints a single Person Object
        private static void printPerson(Person p)
        {
            Console.WriteLine($"Name: {p.Persname} {p.Surname}");
            int ind = 1;
            Console.WriteLine("Phone: ");
            foreach (string a in p.Phone)
            {
                Console.WriteLine($"{ind}: {a}");
                ind++;
            }
            int index = 1;
            Console.WriteLine("Address: ");
            foreach (string b in p.Address)
            {
                Console.WriteLine($"{index}: {b}");
            }
            Console.WriteLine($"Birthdate: {p.Birthdate}");
            Console.WriteLine();
        }

        //deletes an object from the list at the specific index or name
        public static void delete(string[] commandline)
        {
            int index = 0;
            int theindex = -1;
            if (commandline.Length > 1) {
                string nameinput = commandline[1] + " " + commandline[2];
                for (int i = 0; i < contactList.Count; i++)
                {
                    Person person = contactList[i];
                    string temp = person.Persname + " " + person.Surname;
                    if (temp == nameinput)
                    {
                        contactList.RemoveAt(i);
                    }
                    index++;
                }
            }
            else { contactList.Clear(); }

        }

        public static void edit(string[] commandline)
        {
            if (commandline.Length > 1)
            {
                foreach (Person a in contactList)
                {
                    try
                    {
                        if (commandline[1] + " " + commandline[2] == a.Persname + " " + a.Surname)
                        {
                            printPerson(a);
                            Console.Write("name: ");
                            a.Persname = Console.ReadLine();
                            Console.Write("surname: ");
                            a.Surname = Console.ReadLine();
                            for (int i = 0; i < a.Phone.Length; i++)
                            {
                                Console.WriteLine($"number index {i + 1}");
                                string newphone = Console.ReadLine();
                                a.Phone[i] = newphone;
                            }
                            for (int i = 0; i < a.Address.Length; i++)
                            {
                                Console.WriteLine("number index {}");
                                string newaddress = Console.ReadLine();
                                a.Address[i] = newaddress;
                            }
                            Console.WriteLine("birthdate: ");
                            a.Birthdate = Console.ReadLine();
                        }
                        
                    }
                    catch { Console.WriteLine("there is noone named by that name in thelist");
                        break;
                    }
                }
            }
            else { Console.WriteLine("the command is not valid enter 'edit name surname' "); }
        }

        //prints all available commands
        private static void Help()
        {
            Console.WriteLine("Avaliable commands: ");
            Console.WriteLine("  load        - load contact list data from the file address.lis");
            Console.WriteLine("  load /file/ - load contact list data from the file");
            Console.WriteLine("  new        - create new person");
            Console.WriteLine("  new /persname/ /surname/ - create new person with personal name and surname");
            Console.WriteLine("  quit        - quit the program");
            Console.WriteLine("  save         - save contact list data to the file previously loaded");
            Console.WriteLine("  save /file/ - save contact list data to the file");
            Console.WriteLine();
        }
    }
}
