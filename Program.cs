using System;

namespace Bankomat
{
    class Program
    {
        public static void Main(string[] args)
        {   //users array,money they have in theirs bank account and bank accounts arrays.
            string[,] users = new string[,] { { "Sara", "1234" }, { "Johan", "1233" }, { "Maria", "1333" }, { "Sannie", "3333" }, { "Nils", "3331" } };
            decimal[,] money = new decimal[,] { { 13987.12m, 900.00m, 0m }, { 20000.00m, 1000.00m, 500.00m }, { 2500.00m, 0m, 1500.00m }, { 3400.00m, 1045.00m, 0m }, { 1500.00m, 1235.00m, 0m } };
            string[] bankAccount = new string[3];
            bankAccount[0] = "Lönekonto";
            bankAccount[1] = "Sparkonto";
            bankAccount[2] = "Privatkonto";
            Welcome(users, money, bankAccount); //welcome method.

        }
        static void Welcome(string[,] users, decimal[,] money, string[] bankAccount) //method for user to log in.
        {
            int numberOfTries = 0; //number of tries.

            Console.WriteLine("Välkommen till banken.Skriv ditt namn och lösenord för att logga in:");
            do
            {
                try
                {
                    bool validName = true;
                    bool valitPin = true;
                    var name = Console.ReadLine();
                    var pasword = Console.ReadLine();
                    for (int i = 0; i < users.GetLength(0); i++) //loop that loops throught the users array.
                    {

                        if (users[i, 0].Contains(name) && users[i, 1].Contains(pasword))
                        {//if users array contains the name and pasword that user gives the name and pasword vill be true
                            validName = true;
                            valitPin = true;
                            break;
                        }
                        else //if not the name and pasword will be false and user vill have to try 2 more times
                        {
                            validName = false;
                            valitPin = false;
                        }

                    }
                    if (validName == true && valitPin == true)
                    { //if the name and pasword are true the menu will be shown an user have succeed to log in.
                        Console.WriteLine("Välkommen {0}", name);
                        MenuChoice(users, money, name, bankAccount);
                        break;
                    }
                    else if (!validName || !valitPin) //if name or pasword are wrong the user will see this message.
                    {
                        Console.WriteLine("Error vid inloggning!");
                        numberOfTries++;
                    }
                }catch(Exception ex) 
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }

            } while (numberOfTries < 3);
            if (numberOfTries == 3) //if the user will fail 3 times to log in, this message will be shown
            {
                Console.WriteLine("Du lyckades inte på 3 försök.Vänligen starta om programmet och försök igen");
            }


        }
        public static void MenuChoice(string[,] users, decimal[,] money, string name, string[] bankAccount)
        {  //menu choice method
            bool loop = true;
            while (loop) //while loop
            {
                try // if the user writes a letter or char instead of number.
                {
                    Console.WriteLine("Vänligen gör ett val från 1-4:");
                    Console.WriteLine("1.Se dina konton och saldo");    //Menu choices
                    Console.WriteLine("2.Överföring mellan konton");
                    Console.WriteLine("3.Ta ut pengar");
                    Console.WriteLine("4.Logga ut");

                    int userChoice = int.Parse(Console.ReadLine()); //user choice input.

                    switch (userChoice)
                    {
                        case 1: 
                            AccountAndBalance(users, money, name, bankAccount);//method if the user's menuchoice is 1.
                            break;
                        case 2:
                            Console.Clear();
                            TransferMoney(users, money, name, bankAccount);//method if the user's menuchoice is 2.
                            break;
                        case 3:
                            Console.Clear();
                            WithdrawMoney(users, money, name, bankAccount);//method if the user's menuchoice is 3.
                            break;
                        case 4:
                            Console.Clear(); 
                            Welcome(users, money, bankAccount); //method if the user's menuchoice is 4.
                            break;
                        default:  //if the user writes another number than from 1 to 4.
                            Console.WriteLine("Error!Ogiligt val!");
                            break;
                    }
                } catch (FormatException ex)
                {
                    Console.WriteLine("Inmatningssträngen var inte i korrekt format.");
                }
            }
        }
        public static void AccountAndBalance(string[,] users, decimal[,] money, string name, string[] bankAccount)
        {  //method to show accounts and users money.
            int index = GetUser(users, name); // a method to get the users index from the array.

            for (int i = 0; i < money.GetLength(1); i++) //loop that loops throught the users money array.
            {
                if (money[index, i] != 0) 
                {       //users money and their bank accounts.
                    Console.WriteLine($"Du har: { money[index, i]} kr på ditt {bankAccount[i]}");
                }
            }
            Console.WriteLine("Klicka 'Enter' för att komma till huvudmenu");
            Console.ReadLine();
            Console.Clear();
        }
        public static int GetUser(string[,] users, string name)
        {//method to get the user's index in the array.
            int index = 10;
            for (int i = 0; i < users.GetLength(0); i++) //loop that loops throught users array.
            {
                if (users[i, 0] == name)
                {
                    index = i;
                }
            }
            return index; 

        }
        public static void TransferMoney(string[,] users, decimal[,] money, string name, string[] bankAccount)
        {  //method to transfer money between the users accounts.

            Console.WriteLine("Ange summan som du vill överföra: ");
            decimal sum = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Välj konto du vill ta pengarna ifrån: 0.Lönekonto, 1.Sparkonto eller 2.Privatkonto?");
            int fromAccount = int.Parse(Console.ReadLine());
            Console.WriteLine("Välj konton du vill överföra pengarna till: 0.Lönekonto, 1.Sparkonto eller 2.Privatkonto?");
            int toAccount = int.Parse(Console.ReadLine());
            int index = GetUser(users, name);
            decimal difference = 0;
            decimal finalAmount = 0;
            int j;
            j = toAccount;
            for (int i = 0; i < money.GetLength(1); i++) //for loop that loops throught the money array
            {
                if (money[index, i] != 0) 
                { 
                    if (i == fromAccount)     //condition to take the money from the account that user choosed.
                    {  
                        difference = money[index, i] - sum;
                        Console.WriteLine($"Du har {difference}kr på ditt {bankAccount[i]}");
                    }
                    if (i == toAccount)
                    {                          //condition to send the money to the account that user choosed.
                        finalAmount = money[index, j] + sum;
                        Console.WriteLine($"Du har {finalAmount}kr på ditt {bankAccount[i]}");
                       
                    }


                }
                
            }
            Console.WriteLine("Klicka 'Enter' för att komma till huvudmenu");
            Console.ReadLine();
            Console.Clear();
        }
        public static void WithdrawMoney(string[,] users, decimal[,] money, string name, string[] bankAccount)
        {  //method to take take money from users bank account.
            Console.WriteLine("Ange summan som du vill ta ut: ");
            decimal sum = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Välj konto du vill ta pengarna ifrån: 0.Lönekonto 1.Sparkonto eller 2.Privatkonto: ");
            int fromAccount = int.Parse(Console.ReadLine());
            int index = GetUser(users, name);
            decimal difference = 0;
            for (int i = 0; i < bankAccount.GetLength(0); i++)//loop that loops throught the user's bank account.
            {
                if (money[index, i] != 0) //
                {
                    if (i == fromAccount)
                    {
                        difference = money[index, i] - sum;
                        VerifyPinCode(users, money, name, bankAccount, difference);//method for user to writeand verify pin code.
                    }
                }
            }
            Console.WriteLine("Klicka 'Enter' för att komma till huvudmenu");
            Console.ReadLine();
            Console.Clear();

        }
        public static void VerifyPinCode(string[,] users, decimal[,] money, string name, string[] bankAccount, decimal difference)
        {  //method to verify if the pin code
            Console.WriteLine("Skriv din pinkod för att bekräfta: ");
            string pasword = Console.ReadLine();
            for (int i = 0; i < users.GetLength(0); i++) //loop that loops throught the users array
            {
                if (users[i, 1].Contains(pasword)) //if the user's pasword array contains the pasword that user wrote and
                                                   // the money that the user has withdrawn from the account he has chosen will be seen
                {
                    Console.WriteLine($"Du har {difference}kr på ditt:{bankAccount[i]}");
                    break;
                }
                else
                {
                    Console.WriteLine("Fel lösenord!");
                    break;
                }
            }

        }
    }

}
