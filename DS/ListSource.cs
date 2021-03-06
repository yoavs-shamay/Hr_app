﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class ListSource
    {
        public static List<Employee> employeeList = new List<Employee>();
        public static List<Employer> employerList = new List<Employer>();
        public static List<Specialization> specList = new List<Specialization>();
        public static List<Contract> contractList = new List<Contract>();

        public static List<Bank> bankList = new List<Bank>();

        static ListSource()
        {
            generateSpecs();
            generateBanks();
            generateEmployerEmployeeData();
            generateContracts();
        }

        /// <summary>
        /// Adds sample specialization to the database
        /// </summary>
        private static void generateSpecs()
        {
            Random randGen = new Random();

            string[] specNames = { "programming", "graphic design", "management", "Communications" };
            uint i = 0;
            foreach (string spec in specNames)
            {
                specList.Add(new Specialization
                {
                    Id = (i++).ToString(),
                    Name = spec,
                    MinWageForHour = randGen.Next(50, 250),
                    MaxWageForHour = randGen.Next(250, 1000),
                    Area= (Proffesion)Enum.Parse(typeof(Proffesion),Enum.GetNames(typeof(Proffesion))[randGen.Next(5)]),
                    Degree=(Specialization.Education)Enum.Parse(typeof(Specialization.Education),Enum.GetNames(typeof(Specialization.Education))[randGen.Next(6)])
                });
            }
        }

        /// <summary>
        /// Adds sample banks to the database
        /// </summary>
        private static void generateBanks()
        {
            List<Bank> tempBanks = new List<Bank> {
                new Bank("Leumi", 1155, 19, new CivicAddress() {City="Jerusalem", StreetName="Havaad Haleumi", IsPrivateHouse=true, HouseNumber=20, ApartmentNumber=null })/*,
                new Bank("Hapoalim", 9866, 24, new CivicAddress() {City="Jerusalem", StreetName="Malki", HouseNumber=14, IsPrivateHouse=true, ApartmentNumber=null }),
                new Bank("Discount", 8543, 11, new CivicAddress() {City="Jerusalem", StreetName="Hagai", IsPrivateHouse=true, ApartmentNumber=null}),
                new Bank("Bank of Jerusalem", 6654, 8, new CivicAddress() {City="Jerusalem", StreetName="Keren Hayesod St", IsPrivateHouse=true, ApartmentNumber=null })*/
            };

            foreach (Bank bank in tempBanks)
            {
                bankList.Add(bank);
            }
        }

        /// <summary>
        /// Adds sample employees and employers to the database
        /// </summary>
        private static void generateEmployerEmployeeData()
        {
            Random randGen = new Random();

            int[] randIDsTemp = (from num in Enumerable.Range(1, 16)
                             select randGen.Next(10000000, 100000000)).ToArray();
            int[] randIDs = new int[20];
            for (int i = 0; i < 8; i++)
            {
                randIDs[i] = randIDsTemp[i];
            }
            for (int i = 10; i < 17; i++)
            {
                randIDs[i] = randIDsTemp[i-2];
            }
            randIDs[8] = 217324383;
            randIDs[9] = 308222926;
            randIDs[18] = 217324383;
            randIDs[19] = 308222926;
            string[] firstNames = { "Terresa", "Darrick", "Lue", "Phillis", "Haywood", "Shari", "Ginette", "Connie", "Demetrius", "Priscila", "Brittani", "Olimpia", "Luanne", "Brunilda", "Nevada", "Charmain", "Boyd", "Krysta", "Winifred", "Vonnie" };

            string[] lastNames = { "Huie", "Dilbeck", "Morrow", "Millay", "Nastasi", "Spindler", "Leaf", "Bullen", "Ollis", "Satterwhite", "Spinelli", "Berney", "Skeen", "Wenrich", "Bergin", "Kummer", "Torres", "Kruger", "Burtch", "Knutson" };

            string[] compNames = { "3Com Corp", "3M Company", "A.G. Edwards Inc.", "Abbott Laboratories", "Abercrombie & Fitch Co.", "ABM Industries Incorporated", "Ace Hardware Corporation", "ACT Manufacturing Inc.", "Acterna Corp.", "Adams Resources & Energy, Inc.", "ADC Telecommunications, Inc.", "Adelphia Communications Corporation" };

            DateTime[] dates = (from offset in Enumerable.Range(1, 20)
                                select new DateTime(1960, 1, 1).AddYears(randGen.Next(40)).AddMonths(randGen.Next(12)).AddDays(randGen.Next(30))).ToArray();

            string[] phoneNums = (from i in Enumerable.Range(1, 20)
                                  let num = "052" + randGen.Next(1000000, 9999999)
                                  select num).ToArray();

            int[] yearsxp = (from i in Enumerable.Range(1, 20)
                             select randGen.Next(1, 40)).ToArray();

            string[] emails = { "jgranos@hotmail.com", "sparkzilla@cableone.net", "lowkell@gmail.com", "mcgregor@uwo.ca", "tamtruong99@yahoo.com", "eve@thecsrgroup.com", "cyber_zac52@hotmail.com", "clarkfa@2mawnr.usmc.mil", "hsa@uzsi.cz", "tjnichols@fsbdial.co.uk", "daniel.hiestand@3-a.ch", "dale_turner@scotiacapital.com", "Sinister13thUrge@aol.com", "gary_san@yahoo.com", "stehlyja@1mawmag12.usmc.mil", "racorrea@mre.gov.br", "kangrc@gmail.com", "outremere@comcast.net", "yvonne.deboer@international.gc.ca", "jbloore@aol.com", "arif@alfalahsec.com", "milan@eim.ae", "acbortree@yahoo.com", "dm_heilig@yahoo.com", "lazy7777@aol.com", "edp7@email.byu.edu" };

            int[] bankAccnts = Enumerable.Range(1000, 20).ToArray();

            string[] cities = { "אופקים", "אור יהודה", "אור עקיבא", "אילת", "אריאל", "אשדוד", "אשקלון", "באר שבע", "בית שאן", "בית שמש", "ביתר עילית", "בני ברק", "בת ים", "גבעתיים", "דימונה", "הוד השרון", "הרצליה", "חדרה", "חולון", "חיפה", "טבריה", "טירת כרמל", "יבנה", "יהוד - מונוסון", "ירושלים", "כפר סבא", "כרמיאל", "לוד", "מגדל העמק", "מודיעין - מכבים - רעות", "מעלה אדומים", "מעלות - תרשיחא", "נס ציונה", "נצרת", "נצרת עילית", "נשר", "נתיבות", "נתניה", "עכו", "עפולה", "ערד", "פתח תקוה", "צפת", "קרית אונו", "קרית אתא", "קרית ביאליק", "קרית גת", "קרית ים", "קרית מוצקין", "קרית מלאכי", "קרית שמונה", "ראש העין", "ראשון לציון", "רחובות", "רמלה", "רמת גן", "רמת השרון", "רעננה", "שדרות", "תל אביב - יפו" };
            string[] streets = { "דרך בגין", "רחוב בוגרשוב", "רחוב בזל", "רחוב ביאליק", "רחוב בית אשל", "רחוב בן-יהודה", "דרך בן-צבי", "שדרות בן ציון", "שדרות בן-גוריון", "רחוב בני אפרים" };
            foreach (uint i in Enumerable.Range(0, 10))
            {
                bool privateHouse = randGen.Next(1) == 0 ? false : true;
                employeeList.Add(
                    new Employee
                    {
                        Address = new CivicAddress { City = cities[randGen.Next(cities.Length)], StreetName = streets[randGen.Next(streets.Length)], HouseNumber = (uint)randGen.Next(200), IsPrivateHouse = privateHouse, ApartmentNumber = privateHouse ? (uint?)null : (uint)randGen.Next(20) },
                        Id = randIDs[i].ToString(),
                        FirstName = firstNames[i],
                        LastName = lastNames[i],
                        Birth = dates[i],
                        IsMale = (i % 2 == 0 ? true : false),
                        Email = emails[i],
                        BankAccount = new Bank(bankList[randGen.Next(bankList.Count)]),
                        PhoneNumber = phoneNums[i],
                        YearsOfExperience = (uint)yearsxp[i],
                        ArmyGraduate = (i % 2 == 0 ? true : false),
                        PersonalEducation = (Specialization.Education)randGen.Next(1, 4),
                        EmployeeSpecialization = specList[randGen.Next(specList.Count)]
                    });

                employerList.Add(
                    new Employer
                    {
                        Id = randIDs[i + 10].ToString(),
                        FirstName = firstNames[i + 10],
                        LastName = lastNames[i + 10],
                        Address = new CivicAddress { City = cities[randGen.Next(cities.Length)], StreetName = streets[randGen.Next(streets.Length)], HouseNumber = (uint)randGen.Next(200), IsPrivateHouse = privateHouse, ApartmentNumber = privateHouse ? (uint?)null : (uint)randGen.Next(20) },
                        PhoneNumber = phoneNums[i + 10],
                        IsPrivate = (i % 2 == 0? true : false),
                        CompanyName = compNames[i],
                        SetupDate = dates[i + 10],
                        EmployerProffesion = (Proffesion)randGen.Next(Enum.GetNames(typeof(Proffesion)).Length)
                    });
            }
        }

        /// <summary>
        /// Adds sample contracts to the database
        /// </summary>
        private static void generateContracts()
        {
            Random randGen = new Random();

            int[] sequencialIDs = (from num in Enumerable.Range(10000000, 10)
                                   select num).ToArray();

            DateTime[] ContractStartDates = (from offset in Enumerable.Range(1, 10)
                                             select new DateTime(1980, 1, 1).AddYears(randGen.Next(20)).AddMonths(randGen.Next(12)).AddDays(randGen.Next(30))).ToArray();

            DateTime[] ContractTermDates = (from offset in Enumerable.Range(1, 10)
                                            select new DateTime(2010, 1, 1).AddYears(randGen.Next(20)).AddMonths(randGen.Next(12)).AddDays(randGen.Next(30))).ToArray();

            for (int i = 0; i < 10; i++)
            {
                var tempGrossWage = (specList.Find(x => x == employeeList[i].EmployeeSpecialization).MinWageForHour + specList.Find(x => x == employeeList[i].EmployeeSpecialization).MaxWageForHour) / 2;
                contractList.Add(new Contract
                {
                    ContractEstablishedDate = ContractStartDates[i],
                    TerminateDate = ContractTermDates[i],
                    Id = sequencialIDs[i].ToString(),
                    IsFinalized = (randGen.Next(0, 1) == 0 ? true : false),
                    IsInterview = (randGen.Next(0, 1) == 0 ? true : false),
                    EmployeeId = employeeList[i].Id,
                    EmployerId = employerList[i].Id,
                    MaxWorkHoursForMonth = (uint)randGen.Next(25, 50),
                    GrossWageForHour = tempGrossWage,
                    NetWageForHour = tempGrossWage - randGen.NextDouble() * .1 * tempGrossWage
                });
            }
        }
    }
}
