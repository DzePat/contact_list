﻿namespace dtp6_contacts
{
    //TODO: implement methods: save, delete , edit, list and new.
    class MainClass
    {
        //Array of Person objects.
        static Person[] contactListold = new Person[100];
        static List<Person> contactList = contactListold.ToList<Person>();
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
                int comlength = commandLine.Length;
                string tosplit = "";
                int i = 1;
                while(i < comlength) { 
                    tosplit = tosplit+" "+ commandLine[i];
                    i++;
                }
                string[] splits = tosplit.Split("|");
                string[] phonesplits = splits[2].Split(";");
                string[] addresssplits = splits[3].Split(";");
                contactList.Add(new Person(splits[0], splits[1], phonesplits, addresssplits, splits[4]));
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
            foreach(string b in strings)
            {
                if (b != null && a.Length == 0)
                {
                    a = b;
                }
                else if(b != null)
                {
                    a = a + ";" +b;
                }
            }
            return a;
        }
        //adds person objects from the file to the array of objects
        private static void LoadFile(string[] commandLine)
        {
            string lastFileName;
            {
                if (commandLine.Length < 2)
                {
                    lastFileName = "address.lis.txt";
                }
                else { lastFileName= commandLine[1];}
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
        //prints all the Objects in the array
        public static void print(string[] commandLine)
        {
            foreach (Person p in contactList)
            {
                if (p != null)
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
        }

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
