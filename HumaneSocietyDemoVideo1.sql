CREATE TABLE Employees (EmployeeId INTEGER IDENTITY (1,1) PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50), UserName VARCHAR(50), Password VARCHAR(50), EmployeeNumber INTEGER, Email VARCHAR(50));
CREATE TABLE Categories (CategoryId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE DietPlans(DietPlanId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), FoodType VARCHAR(50), FoodAmountInCups INTEGER);
CREATE TABLE Animals (AnimalId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), Weight INTEGER, Age INTEGER, Demeanor VARCHAR(50), KidFriendly BIT, PetFriendly BIT, Gender VARCHAR(50), AdoptionStatus VARCHAR(50), CategoryId INTEGER FOREIGN KEY REFERENCES Categories(CategoryId), DietPlanId INTEGER FOREIGN KEY REFERENCES DietPlans(DietPlanId), EmployeeId INTEGER FOREIGN KEY REFERENCES Employees(EmployeeId));
CREATE TABLE Rooms (RoomId INTEGER IDENTITY (1,1) PRIMARY KEY, RoomNumber INTEGER, AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId));
CREATE TABLE Shots (ShotId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE AnimalShots (AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId), ShotId INTEGER FOREIGN KEY REFERENCES Shots(ShotId), DateReceived DATE, CONSTRAINT AnimalShotId PRIMARY KEY (AnimalId, ShotId));
CREATE TABLE USStates (USStateId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), Abbreviation VARCHAR(2));
CREATE TABLE Addresses (AddressId INTEGER IDENTITY (1,1) PRIMARY KEY, AddressLine1 VARCHAR(50), City VARCHAR(50), USStateId INTEGER FOREIGN KEY REFERENCES USStates(USStateId),  Zipcode INTEGER); 
CREATE TABLE Clients (ClientId INTEGER IDENTITY (1,1) PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50), UserName VARCHAR(50), Password VARCHAR(50), AddressId INTEGER FOREIGN KEY REFERENCES Addresses(AddressId), Email VARCHAR(50));
CREATE TABLE Adoptions(ClientId INTEGER FOREIGN KEY REFERENCES Clients(ClientId), AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId), ApprovalStatus VARCHAR(50), AdoptionFee INTEGER, PaymentCollected BIT, CONSTRAINT AdoptionId PRIMARY KEY (ClientId, AnimalId));

INSERT INTO Employees VALUES('Bob', 'Thomas', 'bobthomas25', 'password123', 01, 'bobthomas@humanesociety.org');
INSERT INTO Employees VALUES('Susan', 'Jones', 'susanjones19', 'password123', 02, 'susanjones@humanesociety.org');
INSERT INTO Employees VALUES('Hali', 'Watson', 'haliwatson55', 'password123', 03, 'haliwatson@humanesociety.org');
INSERT INTO Employees VALUES('Kim', 'Ross', 'kimross10', 'password123', 04, 'kimross@humanesociety.org');
INSERT INTO Employees VALUES('Jost', 'Cunningham', 'jostcunningham12', 'password123', 05, 'jostcunningham@humanesociety.org');

INSERT INTO Clients(FirstName, LastName, UserName, Password, Email) VALUES('Faith', 'Bell', 'faithbell', 'password123', 'faithbell@gmail.com');
INSERT INTO Clients(FirstName, LastName, UserName, Password, Email) VALUES('Cory', 'Black', 'coryblack', 'password123','coryblack@gmail.com');
INSERT INTO Clients(FirstName, LastName, UserName, Password, Email) VALUES('Steve', 'White', 'stevewhite', 'password123', 'stevewhite@gmail.com');
INSERT INTO Clients(FirstName, LastName, UserName, Password, Email) VALUES('John', 'Wick', 'johnwick', 'password123','johnwick@gmail.com');
INSERT INTO Clients(FirstName, LastName, UserName, Password, Email) VALUES('Greg', 'Ham', 'gregham', 'password123','gregham@gmail.com');

INSERT INTO Rooms(RoomNumber) VALUES(105);
INSERT INTO Rooms(RoomNumber) VALUES(106);
INSERT INTO Rooms(RoomNumber) VALUES(107);
INSERT INTO Rooms(RoomNumber) VALUES(108);
INSERT INTO Rooms(RoomNumber) VALUES(109);
INSERT INTO Rooms(RoomNumber) VALUES(110);

INSERT INTO Animals(Name, Weight, Age, Demeanor, KidFriendly, PetFriendly, Gender, AdoptionStatus) VALUES('Dog', 23, 5, 'Sweet', 'True', 'True', 'Male', 'Available');
INSERT INTO Animals(Name, Weight, Age, Demeanor, KidFriendly, PetFriendly, Gender, AdoptionStatus) VALUES('Cat', 9, 2, 'Mean', 'False', 'True', 'Female', 'Available');
INSERT INTO Animals(Name, Weight, Age, Demeanor, KidFriendly, PetFriendly, Gender, AdoptionStatus) VALUES('Fish', 2, 1, 'Gentle', 'True', 'True', 'Male', 'Not Available');
INSERT INTO Animals(Name, Weight, Age, Demeanor, KidFriendly, PetFriendly, Gender, AdoptionStatus) VALUES('Chicken', 3, 2, 'Loud', 'True', 'True', 'Male', 'Not Available');
INSERT INTO Animals(Name, Weight, Age, Demeanor, KidFriendly, PetFriendly, Gender, AdoptionStatus) VALUES('Horse', 900, 3, 'Gentle', 'True', 'True', 'Female', 'Available');

INSERT INTO DietPlans VALUES('Weight Reduction', 'Low Calorie', 1);
INSERT INTO DietPlans VALUES('Weight Gain', 'High Fat', 3);
INSERT INTO DietPlans VALUES('Weight Maintain', 'Balanced', 2);
INSERT INTO DietPlans VALUES('Enrichment', 'Treats', 1);
INSERT INTO DietPlans VALUES('Organic','Whole Foods', 2);

INSERT INTO Rooms(RoomNumber) VALUES(101);
INSERT INTO Rooms(RoomNumber) VALUES(102);
INSERT INTO Rooms(RoomNumber) VALUES(103);
INSERT INTO Rooms(RoomNumber) VALUES(104);

INSERT INTO Categories(Name) VALUES('Dog');
INSERT INTO Categories(Name) VALUES('Cat');
INSERT INTO Categories(Name) VALUES('Fish');
INSERT INTO Categories(Name) VALUES('Chicken');
INSERT INTO Categories(Name) VALUES('Horse');

UPDATE ANIMALS SET Name = 'Fido' WHERE AnimalID = 1;
UPDATE ANIMALS SET Name = 'Lucy' WHERE AnimalID = 2;
UPDATE ANIMALS SET Name = 'Pucker' WHERE AnimalID = 3;
UPDATE ANIMALS SET Name = 'Cluck' WHERE AnimalID = 4;
UPDATE ANIMALS SET Name = 'Shadow' WHERE AnimalID = 5;



INSERT INTO USStates VALUES('Alabama','AL');
INSERT INTO USStates VALUES('Alaska','AK');
INSERT INTO USStates VALUES('Arizona','AZ');
INSERT INTO USStates VALUES('Arkansas','AR');
INSERT INTO USStates VALUES('California','CA');
INSERT INTO USStates VALUES('Colorado','CO');
INSERT INTO USStates VALUES('Connecticut','CT');
INSERT INTO USStates VALUES('Delaware','DE');
INSERT INTO USStates VALUES('Florida','FL');
INSERT INTO USStates VALUES('Georgia','GA');
INSERT INTO USStates VALUES('Hawaii','HI');
INSERT INTO USStates VALUES('Idaho','ID');
INSERT INTO USStates VALUES('Illinois','IL');
INSERT INTO USStates VALUES('Indiana','IN');
INSERT INTO USStates VALUES('Iowa','IA');
INSERT INTO USStates VALUES('Kansas','KS');
INSERT INTO USStates VALUES('Kentucky','KY');
INSERT INTO USStates VALUES('Louisiana','LA');
INSERT INTO USStates VALUES('Maine','ME');
INSERT INTO USStates VALUES('Maryland','MD');
INSERT INTO USStates VALUES('Massachusetts','MA');
INSERT INTO USStates VALUES('Michigan','MI');
INSERT INTO USStates VALUES('Minnesota','MN');
INSERT INTO USStates VALUES('Mississippi','MS');
INSERT INTO USStates VALUES('Missouri','MO');
INSERT INTO USStates VALUES('Montana','MT');
INSERT INTO USStates VALUES('Nebraska','NE');
INSERT INTO USStates VALUES('Nevada','NV');
INSERT INTO USStates VALUES('New Hampshire','NH');
INSERT INTO USStates VALUES('New Jersey','NJ');
INSERT INTO USStates VALUES('New Mexico','NM');
INSERT INTO USStates VALUES('New York','NY');
INSERT INTO USStates VALUES('North Carolina','NC');
INSERT INTO USStates VALUES('North Dakota','ND');
INSERT INTO USStates VALUES('Ohio','OH');
INSERT INTO USStates VALUES('Oklahoma','OK');
INSERT INTO USStates VALUES('Oregon','OR');
INSERT INTO USStates VALUES('Pennsylvania','PA');
INSERT INTO USStates VALUES('Rhode Island','RI');
INSERT INTO USStates VALUES('South Carolina','SC');
INSERT INTO USStates VALUES('South Dakota','SD');
INSERT INTO USStates VALUES('Tennessee','TN');
INSERT INTO USStates VALUES('Texas','TX');
INSERT INTO USStates VALUES('Utah','UT');
INSERT INTO USStates VALUES('Vermont','VT');
INSERT INTO USStates VALUES('Virginia','VA');
INSERT INTO USStates VALUES('Washington','WA');
INSERT INTO USStates VALUES('West Virgina','WV');
INSERT INTO USStates VALUES('Wisconsin','WI');
INSERT INTO USStates VALUES('Wyoming','WY');