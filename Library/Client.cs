using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library
{
    public class Client : IClient
    {
        public Client(String firstName, String lastName, String pesel, Int32 age, Gender sex)
        {
            if (!NameIsValid(firstName))
            {
                throw new Exception("Name not good");
            }
            if (!NameIsValid(lastName))
            {
                throw new Exception("Family name not good");
            }
            if (!PESELIsValid(pesel))
            {
                throw new Exception("PESEL should be in valid format");
            }
            if (!AgeIsValid(age))
            {
                throw new Exception("Age should should be between 0-139");
            }
            FirstName = firstName;
            LastName = lastName;
            PESEL = pesel;
            Age = age;
            Sex = sex;
            Borrowed = new List<IBorrow>();
        }

        public Client()
        {
        }

        private static bool NameIsValid(String input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            if (Regex.IsMatch(input, @"^[A-ZĆŁŚŹŻ][a-ząćęłńóśźż]{2,39}$"))
                return true;
            else
                return false;
        }

        private static bool PESELIsValid(String input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            if (Regex.IsMatch(input, @"^[0-9]{11}$"))
                return true;
            else
                return false;
        }

        private static bool AgeIsValid(Int32 input)
        {
            if (input > 0 && input < 139)
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            var client = obj as Client;
            return client != null &&
                   FirstName == client.FirstName &&
                   LastName == client.LastName &&
                   PESEL == client.PESEL;
        }

        public String FullName => FirstName + " " + LastName;
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PESEL { get; set; }
        public Int32 Age { get; set; }
        public Gender Sex { get; set; }
        public List<IBorrow> Borrowed { get; set; }
    }
}

