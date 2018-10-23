using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using DomainModel;

namespace Infrastructure
{

    


    public class DBUtilities
    {

        public Person NewContact;

        public DBUtilities()
        {
            NewContact = new Person()
            {
                PersonID = 0,
                FirstName = "",
                MiddleName = "",
                LastName = "",
                ContactType = "",
                //Note = "",
                //AddressID = 0,
                Phones = null,
                EmailAddresses = null
                
            };
        }

        private SqlConnection OpenConnection
        {
            get
            {
                var con = new SqlConnection(
                    //Publish Database to get string.
                    //Tonni Surface Connectionstring - LocalDB
                    //
                    //@"Data 
                    //        Source=(localdb)\MSSQLLocalDB;
                    //        Initial Catalog=PersonKartotekDB;
                    //        Integrated Security=True;
                    //        Persist Security Info=False;
                    //        Pooling=False;
                    //        MultipleActiveResultSets=False;
                    //        Connect Timeout=60;
                    //        Encrypt=False;
                    //        TrustServerCertificate=True");


                    //Connection to AU DB Server:
                    //@"Data 
                    //        Source=st-i4dab.uni.au.dk; 
                    //        Initial Catalog=E18I4DABau543236;
                    //        User ID=E18I4DABau543236;
                    //        Password=E18I4DABau543236;
                    //        Connect Timeout=30;
                    //        Encrypt=False;
                    //        TrustServerCertificate=True;
                    //        ApplicationIntent=ReadWrite;
                    //        MultiSubnetFailover=False");


                    @"Data Source=st-i4dab.uni.au.dk; Initial Catalog=E18I4DABau543236;User ID=E18I4DABau543236;Password=E18I4DABau543236;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");



                // Tonni Stationær Connectionstring til LocalDB
                //@"Data 
                //        Source = (localdb)\MSSQLLocalDB; 
                //        Integrated Security = True; 
                //        Persist Security Info = False; 
                //        Pooling = False; 
                //        MultipleActiveResultSets = False; 
                //        Connect Timeout = 60; 
                //        Encrypt = False; 
                //        TrustServerCertificate = True");

                con.Open();
                return con;
            }
        }


        #region Person_Address Relations Tools

        public void AddPersonAddressRelation(ref PersonAddressRelations personAddressRelations)
        {
            string insertStringParam = @"INSERT INTO PersonAddressRelations (PersonID, AddressID)
                                                OUTPUT INSERTED.PersonID
                                                VALUES(@PersonID, @AddressID";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonID", personAddressRelations.PersonID);
                cmd.Parameters.AddWithValue("@AddressID", personAddressRelations.AddressID);
            }
        }


        public void DeletePersonAddressRelation(ref PersonAddressRelations personAddressRelations)
        {
            string DeleteString = @"DELETE FROM PersonAddressRelation where (PersonID=@PersonID)";
            using (SqlCommand cmd = new SqlCommand(DeleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonID", personAddressRelations.PersonID);
                var count = cmd.ExecuteNonQuery();
                personAddressRelations = null;
            }
        }

        public List<PersonAddressRelations> GetAddressByPersonIDPersonAddressRelation(ref Person person)
        {
            string GetAddressByName = @"Select * FROM PersonAddressRelation WHERE (PersonID=@PersonID)";
            using (SqlCommand cmd = new SqlCommand(GetAddressByName, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonID", person.PersonID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                var list = new List<PersonAddressRelations>();
                while (rdr.Read())
                {
                    PersonAddressRelations personAddressRelations = new PersonAddressRelations();
                    personAddressRelations.PersonID = (int)rdr["PersonID"];
                    personAddressRelations.AddressID= (int)rdr["AddressID"];

                    personAddressRelations.Address = GetAddressByID(personAddressRelations.AddressID);
                    personAddressRelations.Person = person;


                    list.Add(personAddressRelations);
                }

                return list;
            }
        }

        #endregion

        #region Person Tools


        public void AddPerson(ref Person person)
        {
            string insertStringParam = @"INSERT INTO [Person] (FirstName, MiddleName, LastName, ContactType)
                                                OUTPUT INSERTED.PersonID
                                                VALUES(@Firstname, @MiddleName, @LastName, @ContactType)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", person.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                cmd.Parameters.AddWithValue("@ContactType", person.ContactType);
                //cmd.Parameters.AddWithValue("@Note", person.Note);
                // cmd.Parameters.AddWithValue("@AddressID", person.AddressID);
                person.PersonID = (int)cmd.ExecuteScalar();
            }
        }

        public void UpdatePerson(ref Person person)
        {
            string insertStringParam = @"UPDATE Person
                                                SET FirstName=@Firstname, MiddleName=@MiddleName, LastName=@LastName, ContactType=@ContactType)
                                                WHERE PersonID=@PersonID";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", person.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                cmd.Parameters.AddWithValue("@ContactType", person.ContactType);
                cmd.Parameters.AddWithValue("@PersonID", person.PersonID);
                //cmd.Parameters.AddWithValue("@Note", person.Note);
                var count = cmd.ExecuteNonQuery();
            }
        }

        public void DeletePerson(ref Person person)
        {
            string DeleteString = @"DELETE FROM Person WHERE (PersonID=@PersonID)";
            using (SqlCommand cmd = new SqlCommand(DeleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonID", person.PersonID);
                var count = cmd.ExecuteNonQuery();
                person = null;
            }
        }

        public Person GetPersonByName(ref Person person)
        {
            string GetByName = @"SELECT TOP  1 * FROM Person WHERE (LastName=@LastName)";
            using (var cmd = new SqlCommand(GetByName, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    NewContact.PersonID = (int)rdr["PersonID"];
                    NewContact.FirstName = (string)rdr["FirstName"];
                    NewContact.MiddleName = (string)rdr["MiddleName"];
                    NewContact.LastName = (string)rdr["LastName"];
                    NewContact.ContactType = (string)rdr["ContactType"];
                    //NewContact.Note = (string)rdr["Note"];
                    //NewContact.EmailAddresses = (List<EmailAddr>)rdr["EmailAddress"];
                    //NewContact.Phones = (List<Phone>)rdr["PhoneNumber"];
                    NewContact.AddressRelations = GetAddressByPersonIDPersonAddressRelation(ref NewContact);
                    person = NewContact;
                }
            }

            return person;
        }

        public List<Person> GetAllThePersonS()
        {
            string GetAllPersons = @"SELECT  * FROM Person";
            using (var cmd = new SqlCommand(GetAllPersons, OpenConnection))
            {
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                List<Person> Persons = new List<Person>();
                Person _person = null;

                while (rdr.Read())
                {
                    _person = new Person();
                    _person.PersonID = (int) rdr["PersonID"];
                    _person.FirstName = (string)rdr["FirstName"];
                    _person.MiddleName = (string)rdr["MiddleName"];
                    _person.LastName = (string)rdr["LastName"];
                    _person.ContactType = (string)rdr["ContactType"];
                    //_person.Note = (string)rdr["Note"];
                    //_person.EmailAddresses = (List<EmailAddr>)rdr["EmailAddress"];
                    //_person.Phones = (List<Phone>)rdr["PhoneNumber"];
                    Persons.Add(_person);
                }
            return Persons;
            }

        }

        public void GetPersonByID(ref Person person)
        {
            string GetByID =
                @"SELECT FirstName, MiddleName, LastName, ContactType FROM Person Where ([PersonID] = @PersonID)";
            using (var cmd = new SqlCommand(GetByID, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonID", person.PersonID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    NewContact.PersonID = person.PersonID;
                    NewContact.FirstName = (string)rdr["FirstName"];
                    NewContact.MiddleName = (string)rdr["MiddleName"];
                    NewContact.LastName = (string)rdr["LastName"];
                    NewContact.ContactType = (string)rdr["ContactType"];
                    //NewContact.Note = (string)rdr["Note"];

                    person = NewContact;
                }
            }
        }

        public List<Person> ListPersons()
        {
            string ListingPersons = @"SELECT * FROM Person";
            using (var cmd = new SqlCommand(ListingPersons, OpenConnection))
            {
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                List<Person> list = new List<Person>();
                Person person = null;
                while (rdr.Read())
                {
                    person = new Person();
                    person.PersonID = (int)rdr["PersonID"];
                    NewContact.FirstName = (string)rdr["FirstName"];
                    NewContact.MiddleName = (string)rdr["MiddleName"];
                    NewContact.LastName = (string)rdr["LastName"];
                    NewContact.ContactType = (string)rdr["ContactType"];
                    //NewContact.Note = (string)rdr["Note"];


                    list.Add(person);
                }
                return list;
            }
        }

        #endregion


        public void AddCity(ref AddressCity city)
        {
            string insertStringParam = @"INSERT INTO [City] (CityName, PostalCode)
                                        OUTPUT INSERTED.CityID
                                        VALUES (@City, @PostalCode)";

            using (var cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@City", city.City);
                cmd.Parameters.AddWithValue("@PostalCode", city.PostalCode);
                city.CityID = (int)cmd.ExecuteScalar();
            }
        }



        #region Address Tools

        public Address GetAddressByID(int AddressID)
        {
            //Ved ikke om nedenstående kan gøres.... (ift. WHERE Person AND AddressType)
            //string ListAddresses = @"SELECT * FROM [Address] WHERE ([Person] = @PersonID) AND ([IsRegisteredAddress = @IsRegisteredAddress])";
            string AddressQuery = @"SELECT * FROM [Address] WHERE ( AddressID = @AddressID )";
            using (var cmd = new SqlCommand(AddressQuery, OpenConnection))
            {
                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@AddressID", AddressID);
                rdr = cmd.ExecuteReader();
                
                Address PrimaryAddr = null;

                if (rdr.Read())
                {
                    PrimaryAddr = new Address();
                    PrimaryAddr.AddressID = (int)rdr["AddressID"];
                    PrimaryAddr.RoadName = (string)rdr["RoadName"];
                    PrimaryAddr.HouseNumber = (string)rdr["HouseNumber"];
                    PrimaryAddr.Story = (string)rdr["Story"];
                    PrimaryAddr.IsRegisteredAddress = (string)rdr["IsRegisteredAddress"];
                    PrimaryAddr.AddressType = (string)rdr["AddressType"];
                }
                return PrimaryAddr;
            }
        }

        public void AddAddressToDB(ref Address address)
        {
            string insertStringParam =
                @"INSERT INTO [Address] (RoadName, HouseNumber, Story, AddressType, IsRegisteredAddress, CityID)
                                                OUTPUT INSERTED.AddressID
                                                VALUES(@RoadName, @HouseNumber, @Story, @AddressType, @IsRegisteredAddress, @CityID)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@RoadName", address.RoadName);
                cmd.Parameters.AddWithValue("@HouseNumber", address.HouseNumber);
                cmd.Parameters.AddWithValue("@Story", address.Story);
                cmd.Parameters.AddWithValue("@AddressType", address.AddressType);
                cmd.Parameters.AddWithValue("@IsRegisteredAddress", address.IsRegisteredAddress);
               // cmd.Parameters.AddWithValue("@PersonID", address.PersonID);
                cmd.Parameters.AddWithValue("@CityID", address.CityID);
                address.AddressID = (int)cmd.ExecuteScalar();
            }
        }

        public void UpdateAddressOnPerson(ref Address address)
        {
            string updateString = @"UPDATE Address
                                    SET RoadName = @RoadName, HouseNumber = @HouseNumber, Story = @Story, AddressType = @AddressType, IsRegisteredAddress = @IsRegisteredAddress
                                    WHERE AddressID = @AddressID";
            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@RoadName", address.RoadName);
                cmd.Parameters.AddWithValue("@HouseNumber", address.HouseNumber);
                cmd.Parameters.AddWithValue("@Story", address.Story);
                cmd.Parameters.AddWithValue("@AddressType", address.AddressType);
                cmd.Parameters.AddWithValue("@IsRegisteredAddress", address.IsRegisteredAddress);
                var count = cmd.ExecuteNonQuery();
            }
        }


        public void DeleteAddressOnPerson(ref Address address)
        {
            string deleteString = @"DELETE FROM Address WHERE (AddressID = @AddressID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AddressID", address.AddressID);
                var count = cmd.ExecuteNonQuery();
                address = null;
            }
        }


        #endregion


        #region Phone Tools

        public List<Phone> GetPersonsPhones(ref Person person)
        {
            string ListPhoneNumbers = @"Select * FROM [Phone] WHERE ([Person] =@PersonID)";
            using (var cmd = new SqlCommand(ListPhoneNumbers, OpenConnection))
            {
                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@PersonID", person.PersonID);
                rdr = cmd.ExecuteReader();
                List<Phone> PhoneNumbersList = new List<Phone>();
                Phone PhoneNumber = null;
                while (rdr.Read())
                {
                    PhoneNumber = new Phone();
                    PhoneNumber.PhoneNumber = (string)rdr["PhoneNumber"];
                    PhoneNumber.PhoneType = (string)rdr["PhoneType"];
                    PhoneNumber.PhoneID = (int)rdr["PhoneID"];
                    PhoneNumber.Provider = (string)rdr["Provider"];
                    PhoneNumbersList.Add(PhoneNumber);
                }
                return PhoneNumbersList;
            }
        }

        public void AddPhoneNumberOnPerson(Phone phone)
        {
            string insertStringParam = @"INSERT INTO [Phone] (PhoneNumber, PhoneType, Provider, PersonID)
                                                OUTPUT INSERTED.PhoneID
                                                VALUES (@PhoneNumber, @PhoneType, @Provider, @PersonID)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PhoneNumber", phone.PhoneNumber);
                cmd.Parameters.AddWithValue("@PhoneType", phone.PhoneType);
                cmd.Parameters.AddWithValue("@Provider", phone.Provider);
                cmd.Parameters.AddWithValue("@PersonID", phone.PersonID);
                phone.PhoneID = (int)cmd.ExecuteScalar();
            }
        }

        public void UpdatePhoneNumberOnPerson(ref Phone phone)
        {
            string updateString = @"UPDATE Phone
                                  SET PhoneNumber = @PhoneNumber, PhoneType = @PhoneType, Provider = @Provider
                                  WHERE PhoneID = @PhoneId";
            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PhoneNumber", phone.PhoneNumber);
                cmd.Parameters.AddWithValue("@PhoneType", phone.PhoneType);
                cmd.Parameters.AddWithValue("@Provider", phone.Provider);
                var count = cmd.ExecuteNonQuery();
            }
        }


        public void DeletePhoneNumberOnPerson(ref Phone phone)
        {
            string deleteString = @"DELETE FROM Phone Where (PhoneID = @PhoneID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PhoneID", phone.PhoneID);
                var count = (int)cmd.ExecuteNonQuery();
                phone = null;
            }
        }
        #endregion


        #region Email Address Tools

        public List<EmailAddr> GetPersonsEmailAddrs(ref Person person)
        {
            string ListEmailAddys = @"SELECT * FROM [EmailAddr] WHERE ([Person] = @PersonID)";
            using (var cmd = new SqlCommand(ListEmailAddys, OpenConnection))
            {
                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@PersonID", person.PersonID);
                rdr = cmd.ExecuteReader();
                List<EmailAddr> MailAddyList = new List<EmailAddr>();
                EmailAddr Emails = null;
                while (rdr.Read())
                {
                    Emails = new EmailAddr();
                    Emails.EmailID = (int)rdr["EmailID"];
                    Emails.IsPrimaryEmail = (bool)rdr["IsPrimaryEmail"];
                    Emails.EmailAddress = (string)rdr["EmailAddress"];

                    MailAddyList.Add(Emails);
                }

                return MailAddyList;
            }
        }

        public void AddEmailAddressOnPerson(ref EmailAddr emailAddr)
        {
            string insertStringParam = @"INSERT INTO [EmailAddr] (EmailAddress, IsPrimaryEmail, PersonID)
                                                OUTPUT INSERTED.EmailID
                                                VALUES (@EmailAddress, @IsPrimaryEmail, @PersonID)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@EmailAddress", emailAddr.EmailAddress);
                cmd.Parameters.AddWithValue("@IsPrimaryEmail", emailAddr.IsPrimaryEmail);
                cmd.Parameters.AddWithValue("@PersonId", emailAddr.PersonID);
                emailAddr.EmailID = (int)cmd.ExecuteScalar();
            }
        }


        public void UpdateEmailAddressOnPerson(ref EmailAddr emailAddr)
        {
            string updateString = @" UPDATE EmailAddress
                                     SET EmailAddress = @EmailAddress, IsPrimaryEmail = @IsPrimaryEmail
                                     WHERE EmailID = @EmailID";
            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@EmailAddress", emailAddr.EmailAddress);
                cmd.Parameters.AddWithValue("@IsPrimaryEmail", emailAddr.IsPrimaryEmail);
                var count = cmd.ExecuteNonQuery();
            }
        }


        public void DeleteEmailAddressOnPerson(ref EmailAddr emailAddr)
        {
            string deleteString = @"DELETE FROM EmailAddress Where (EmailID = @EmailID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@EmailID", emailAddr.EmailID);
                var count = cmd.ExecuteNonQuery();
                emailAddr = null;
            }
        }
        #endregion


        //public void GetPersonFullTree(ref Person personTree)
        //{
        //    string PersonFullTree =
        //        @"SELECT Person.PersonID, Person.FirstName, Person,MiddleName, Person.LastName, Person.ContactType
        //                 Address.AddressID, Address.RoadName, Address.HouseNumber, Address.Story, Address.AddressType, Address.IsRegisteredAddress
        //                 Phone.PhoneID, Phone.PhoneNumber, Phone.PhoneType, Phone.Provider
        //                 EmailAddr.EmailID, EmailAddr.EmailAddress, EmailAddr.IsPrimaryEmail
        //          FROM Person INNER JOIN
        //                 Address ON Person.PersonID = Address.Person INNER JOIN
        //                 Phone On Person.PersonID = Phone.Person INNER JOIN
        //                 EmailAddr ON Person.PersonID = EmailAddr.Person INNER JOIN
        //          WHERE (Person.PersonID = @PersonID)";
        //    using (var cmd = new SqlCommand(PersonFullTree, OpenConnection))
        //    {
        //        cmd.Parameters.AddWithValue("@PersonID", personTree.PersonID);
        //        SqlDataReader rdr = null;
        //        rdr = cmd.ExecuteReader();
        //        var PersonCount = 0;
        //        var AddressCount = 0;
        //        var EmailCount = 0;
        //        var PhoneCount = 0;

        //        int PersonIDs = 0;
        //        int AddressIDs = 0;
        //        int EmailIDs = 0;
        //        int PhoneIDs = 0;


        //        Person person = new Person();
        //        Address address = null;
        //        EmailAddr email = null;
        //        Phone phone = null;

        //        person.Addresses = new List<Address> { };
        //        person.EmailAddresses = new List<EmailAddr> { };
        //        person.Phones = new List<Phone> { };

        //        while (rdr.Read())
        //        {
        //            int personID;
        //            personID = (int)rdr["PersonID"];
        //            if (PersonIDs != personID) // C'est possibile ??? Ceci n'est pas Possibile? 
        //            {
        //                PersonCount++;
        //                person.PersonID = personID;
        //                PersonIDs = person.PersonID;
        //                person.FirstName = (string)rdr["FirstName"];
        //                person.MiddleName = (string)rdr["MiddleName"];
        //                if (!rdr.IsDBNull(3))
        //                    person.MiddleName = (string)rdr["MiddleName"];
        //                else
        //                    person.MiddleName = null;
        //                person.LastName = (string)rdr["LastName"];
        //                //Ved ikke helt hvordan jeg har det med ... 
        //                //person.Notes = (List<ContactNote>) rdr["Notes"];
        //                //if (!rdr.IsDBNull(5))
        //                //    person.Notes = (List<ContactNote>) rdr["Notes"];
        //                //else
        //                //    person.Notes = null;
        //                person.ContactType = (string)rdr["ContactType"];
        //                if (!rdr.IsDBNull(6))
        //                    person.ContactType = (string)rdr["ContactType"];
        //                else
        //                    person.ContactType = null;
        //            }

        //            if (!rdr.IsDBNull(7))
        //            {
        //                AddressCount++;
        //                int addressID;
        //                addressID = (int)rdr["AddressID"];
        //                if (AddressIDs != addressID)
        //                {
        //                    address = new Address();
        //                    person.Addresses.Add(address);
        //                }

        //                AddressIDs = address.AddressID;
        //                address.RoadName = (string)rdr["RoadName"];
        //                address.HouseNumber = (string)rdr["HouseNumber"];
        //                address.Story = (string)rdr["Story"];
        //                address.AddressType = (string)rdr["AddressType"];
        //                address.IsRegisteredAddress = (bool)rdr["IsRegisteredAddress"];
        //            }

        //            if (!rdr.IsDBNull(13))
        //            {
        //                EmailCount++;
        //                int emailID;
        //                emailID = (int)rdr["EmailID"];
        //                if (EmailIDs != emailID)
        //                {
        //                    email = new EmailAddr();
        //                    person.EmailAddresses.Add(email);
        //                }

        //                EmailIDs = email.EmailID;
        //                email.EmailAddress = (string)rdr["EmailAdresse"];
        //                email.IsPrimaryEmail = (bool)rdr["IsPrimaryEmail"];
        //            }

        //            if (!rdr.IsDBNull(16))
        //            {
        //                PhoneCount++;
        //                int phoneID = (int)rdr["PhoneID"];
        //                if (PhoneIDs != phoneID)
        //                {
        //                    phone = new Phone();
        //                    person.Phones.Add(phone);
        //                }

        //                PhoneIDs = phone.PhoneID;
        //                phone.PhoneNumber = (string)rdr["PhoneNumber"];
        //                phone.PhoneType = (string)rdr["PhoneType"];
        //                phone.Provider = (string)rdr["Provider"];
        //            }
        //        }
        //    }
        //}
    }
}
