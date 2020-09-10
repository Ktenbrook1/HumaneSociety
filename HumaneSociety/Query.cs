﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {        
        static HumaneSocietyDataContext db;

        static Query()
        {
            db = new HumaneSocietyDataContext();
        }

        internal static List<USState> GetStates()
        {
            List<USState> allStates = db.USStates.ToList();       

            return allStates;
        }
            
        internal static Client GetClient(string userName, string password)
        {
            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            Client newClient = new Client();

            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Address addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.USStateId == stateId).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (addressFromDb == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = streetAddress;
                newAddress.City = null;
                newAddress.USStateId = stateId;
                newAddress.Zipcode = zipCode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                addressFromDb = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.InsertOnSubmit(newClient);

            db.SubmitChanges();
        }

        internal static void UpdateClient(Client clientWithUpdates)
        {
            // find corresponding Client from Db
            Client clientFromDb = null;

            try
            {
                clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine("No clients have a ClientId that matches the Client passed in.");
                Console.WriteLine("No update have been made.");
                return;
            }
            
            // update clientFromDb information with the values on clientWithUpdates (aside from address)
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;

            // get address object from clientWithUpdates
            Address clientAddress = clientWithUpdates.Address;

            // look for existing Address in Db (null will be returned if the address isn't already in the Db
            Address updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.USStateId == clientAddress.USStateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if(updatedAddress == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.City = null;
                newAddress.USStateId = clientAddress.USStateId;
                newAddress.Zipcode = clientAddress.Zipcode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;
            
            // submit changes
            db.SubmitChanges();
        }

        

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static void AddNewEmployee(string firstName, string lastName, string username, string password, string email)
        {
            Employee newEmployee = new Employee();

            newEmployee.FirstName = firstName;
            newEmployee.LastName = lastName;
            newEmployee.UserName = username;
            newEmployee.Password = password;
            newEmployee.Email = email;

            db.Employees.InsertOnSubmit(newEmployee);

            db.SubmitChanges();

        }

        internal static void UpdateEmployee()
        {

        }


        internal static void DeleteEmployee()
        {

        }


        internal static void AddUsernameAndPassword(Employee employee)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SubmitChanges();
        }
       
        

        internal static Employee EmployeeLogin(string userName, string password)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();

            return employeeWithUserName != null;
        }


        //// TODO Items: ////
        
        // TODO: Allow any of the CRUD operations to occur here
        internal static void RunEmployeeQueries(Employee employee, string crudOperation)
        {
            // As a developer, I want to implement the Query.RunEmployeeQueries() method so that any CRUD operation can be applied to an employee.
            
            switch (crudOperation.ToLower())
            {
                case "create":
                    AddNewEmployee(employee.FirstName,employee.LastName, employee.UserName, employee.Password, employee.Email); 
                    break;
                case "read":
                    UserInterface.DisplayEmployeeInfo(employee);
                    break;
                case "update":
                    UpdateEmployee(); // LOGIC NOT DONE                 
                    break;
                case "delete":
                    DeleteEmployee(); // LOGIC NOT DONE
                    break;
                default:
                    UserInterface.DisplayUserOptions("Input not recognized please try again.");
                    RunEmployeeQueries(employee, crudOperation);
                    break;
            }

        }

        

        // TODO: Animal CRUD Operations
        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        internal static Animal GetAnimalByID(int id)
        {
            Animal animalOfChoice = db.Animals.Where(a => a.AnimalId == id).FirstOrDefault();

            if (animalOfChoice == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return animalOfChoice;
            }
        }

        internal static void UpdateAnimal(int animalId, Dictionary<int, string> updates)
        {
            Animal animalToUpdate = db.Animals.Where(a => a.AnimalId == animalId).FirstOrDefault();
            var keys = new List<int>(updates.Keys);
            foreach (int key in keys)
            {
                switch (key)
                {
                    case 1:
                        animalToUpdate.Category = db.Categories.Where(c => c.CategoryId == GetCategoryId(updates[key])).FirstOrDefault();
                        db.SubmitChanges();
                        break;
                        //"2. Name", "3. Age", "4. Demeanor", "5. Kid friendly", "6. Pet friendly", "7. Weight", "8. Finished", 
                    case 2:
                        animalToUpdate.Name = updates[key];
                        db.SubmitChanges();
                        break;
                    case 3:
                        try
                        {
                            animalToUpdate.Age = Int32.Parse(updates[key]);
                        }
                        catch 
                        {
                            Console.WriteLine("Invalid Input");
                        }
                        db.SubmitChanges();
                        break;
                    case 4:
                        animalToUpdate.Demeanor = updates[key];
                        db.SubmitChanges();
                        break;
                    case 5:
                        try
                        {
                            animalToUpdate.KidFriendly = bool.Parse(updates[key]);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid Input");
                        }
                        db.SubmitChanges();
                        break;
                    case 6:
                        try
                        {
                            animalToUpdate.PetFriendly = bool.Parse(updates[key]);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid Input");
                        }
                        db.SubmitChanges();
                        break;
                    case 7:
                        try
                        {
                            animalToUpdate.Weight = Int32.Parse(updates[key]);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid Input");
                        }
                        db.SubmitChanges();
                        break;
                    case 8:
                        Console.WriteLine("Update Complete");
                        break;
                }
            }
        }
        internal static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }
        
        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animal> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates) // parameter(s)?
        {           
            var animals = db.Animals.AsQueryable();
            foreach (var pair in updates) // foreach iterate over keyvalue pair (see Perls)
            {
                switch (pair.Key)
                {
                    
                    case 1:
                        animals = animals.Where(s => s.CategoryId != GetCategoryId(pair.Value));                
                        break;
                      
                    case 2:
                        animals = animals.Where(s => s.Name != pair.Value);
                        break;

                    case 3:
                        animals = animals.Where(s => s.Age != int.Parse(pair.Value));                                             
                        break;

                    case 4:
                        animals = animals.Where(s => s.Demeanor != pair.Value);                       
                        break;

                    case 5:
                        animals = animals.Where(s => s.KidFriendly != bool.Parse(pair.Value));                       
                        break;

                    case 6:
                        animals = animals.Where(s => s.PetFriendly != bool.Parse(pair.Value));                       
                        break;

                    case 7:
                        animals = animals.Where(s => s.Weight != int.Parse(pair.Value));                     
                        break;

                    case 8:
                        animals = animals.Where(s => s.AnimalId != int.Parse(pair.Value));                    
                        break;

                }
             
            }

            return animals;

        }

         
        // TODO: Misc Animal Things
        internal static int GetCategoryId(string categoryName)
        {
            var categoryThatExist = db.Categories.Where(c => c.Name == categoryName).FirstOrDefault().CategoryId;
            return categoryThatExist;
        }
        
        internal static Room GetRoom(int animalId)
        {
            Room room = db.Rooms.Where(a => a.AnimalId == animalId).Single();
            return room;
        }
        
        internal static int GetDietPlanId(string dietPlanName)
        {
            var dietPlan = db.DietPlans.Where(d => d.Name == dietPlanName).FirstOrDefault().DietPlanId;
            return dietPlan;
        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animal animal, Client client)
        {
            Animal adoptedAnimal = db.Animals.Where(a => a.AnimalId == animal.AnimalId).SingleOrDefault();
            Client clientAdopter = db.Clients.Where(c => c.ClientId == client.ClientId).SingleOrDefault();

            Adoption newAdoption = new Adoption();

            newAdoption.ClientId = clientAdopter.ClientId;
            newAdoption.ApprovalStatus = "Adopted";
            newAdoption.AdoptionFee = 75;
            newAdoption.AnimalId = adoptedAnimal.AnimalId;

            db.Adoptions.InsertOnSubmit(newAdoption);
            db.SubmitChanges();

        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            throw new NotImplementedException();
        }

        internal static void UpdateAdoption(bool isAdopted, Adoption adoption)
        {
            throw new NotImplementedException();
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            throw new NotImplementedException();
        }

        // TODO: Shots Stuff
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateShot(string shotName, Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}