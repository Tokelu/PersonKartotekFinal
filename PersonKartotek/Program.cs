using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using DomainModel;
using Infrastructure;


namespace PersonKartotek
{
    public class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Hello ... " +
                              "\n ... " +
                              "\n .. " +
                              "\n . " +
                              "\n" +
                              "Starting Database\n");





            DBUtilities entry = new DBUtilities();

            AddressCity _newcity = new AddressCity()
            {
                City = "Aarhus",
                PostalCode = "8000"
            };
            entry.AddCity(ref _newcity);

            Address _newaddress = new Address()
            {
                RoadName = "Vestergade",
                HouseNumber = "22A",
                Story = "",
                IsRegisteredAddress = "Primær",
                AddressType = "Home",
                CityID = _newcity.CityID
                
            };
            
            _newcity.Addresses =new List<Address>();
            _newcity.Addresses.Add(_newaddress);
            entry.AddAddressToDB(ref _newaddress);

            Person _newPerson = new Person()
            {
                FirstName = "Test",
                MiddleName = "Tester",
                LastName = "Testersen",
                ContactType = "Privat",
                //Note = "Hader DAB",
            };
            _newPerson.AddressRelations.Add(new PersonAddressRelations(){Address = _newaddress});
            _newaddress.Persons = new List<Person>();
            _newaddress.Persons.Add(_newPerson);
            entry.AddPerson(ref _newPerson);

            var _person = entry.GetPersonByName(ref _newPerson);


            var kartotek = new DBUtilities();
            var PersonsInDB = kartotek.GetAllThePersonS();

            foreach (Person Persons in PersonsInDB)
            {
                Console.WriteLine("Personens ID: \t"+Persons.PersonID);
                Console.WriteLine("Personens Fornavn er: \t" + Persons.FirstName);
                Console.WriteLine("Personens Mellemnavn er: \t" + Persons.MiddleName);
                Console.WriteLine("Personens Efternavn er: \t" + Persons.LastName);
                Console.WriteLine("Kontakt typen for personen er \t" + Persons.ContactType);
                //Console.WriteLine("Note om Personen \t" + Persons.Note);
            }












            

            Console.WriteLine();






            //DBCalls test = new DBCalls();
             
            //test.CreateEntryInDB();
            //var FN = "test";
            //var LN = "tester";
            //var MN = "testesen";
            //var CT = "noob";
            //var Note = "træt af dab";

            //DBUtilities newperson = new DBUtilities(0,FN,MN,LN,CT,Note);



            //var temp = newperson.ListPersons();
            //foreach (Person person in temp)
            //{
            //    Console.WriteLine("ID \t" + person.PersonID);
            //    Console.WriteLine("Fornavn \t" + person.FirstName);
            //    Console.WriteLine("Mellemnavn \t" + person.MiddleName);
            //    Console.WriteLine("Efternavn \t" + person.LastName);
            //}







            

            //Console.WriteLine("Creating new entry... ");
            //var NewEntry = new Person();

            //Console.WriteLine("Please enter First Name: ");
            //NewEntry.FirstName = (Console.ReadLine());


            //Console.WriteLine("Please enter Middle Name (Optional): ");
            //NewEntry.MiddleName = (Console.ReadLine());


            //Console.WriteLine("Please enter Last Name: ");
            //NewEntry.LastName = (Console.ReadLine());


            //Console.WriteLine("Please enter contact type (Business, Private etc.: ");
            //NewEntry.ContactType = (Console.ReadLine());



            //Console.WriteLine("Please enter any notes");
            //NewEntry.Note = (Console.ReadLine());

            Console.ReadLine();
        }
    }
}
