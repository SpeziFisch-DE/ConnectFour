using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ConnectFour.Core;
using ConnectFour.Objects;
using ConnectFour.FileHandler;
using ConnectFour.Game;

namespace ConnectFourApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu[] menu = Menu.CreateAllMenu();
            int chosenMenu = 0;
            int statsLink = -1;

            ConsoleKeyInfo CKI;
            string input = "";

            do
            {
                Console.Clear();
                Console.WriteLine(menu[chosenMenu].title);
                Console.WriteLine("");
                if (chosenMenu == 0)
                {
                    Console.WriteLine("");
                    for (int i = 0; i < menu[chosenMenu].choices.Length; i++)
                    {
                        Console.WriteLine("[" + menu[chosenMenu].linkedMenu[i] + "] " + menu[chosenMenu].choices[i]);
                    }


                    string pattern = "^[1-5]{1}$";
                    Regex rg = new Regex(pattern);
                    input = Console.ReadLine();
                    if (rg.Match(input).Success)
                    {
                        int intInput = Convert.ToInt32(input);

                        chosenMenu = intInput;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("invalid input.");
                        Console.WriteLine("[back]");
                        input = Console.ReadLine();
                    }

                }
                else
                {
                    if (chosenMenu == 1) // Register Menu
                    {
                        if (statsLink >= 0)
                        {
                            Console.WriteLine("You are already logged in!");
                            Console.ReadKey();
                            chosenMenu = 0;
                        }
                        else
                        {
                            int formPosition = 0;
                            RegisteredUser newUser = new RegisteredUser();

                            RegisteredUser[] existingUsersArr = Handler.ReadJSON<RegisteredUser[]>("/userdata", "userdata.txt");
                            List<RegisteredUser> existingUsersList = existingUsersArr.ToList<RegisteredUser>();

                            UserStats[] statsArr = Handler.ReadJSON<UserStats[]>("/userdata", "stats.txt");
                            List<UserStats> stats = statsArr.ToList<UserStats>();

                            do
                            {
                                Console.WriteLine(menu[chosenMenu].choices[formPosition] + ": ");
                                if (formPosition == 0) // username validation
                                {
                                    string pattern = "^[a-zA-Z0-9]{3,15}$";
                                    Regex rg = new Regex(pattern);

                                    input = Console.ReadLine();

                                    if (rg.Match(input).Success)
                                    {
                                        bool usernameFree = true;
                                        foreach (RegisteredUser user in existingUsersList)
                                        {
                                            if (user.username == input)
                                            {
                                                usernameFree = false;
                                            }
                                        }
                                        if (usernameFree)
                                        {
                                            newUser.username = input;
                                            formPosition++;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Username is already taken!");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Username did not meet requirements!");
                                    }
                                }
                                else if (formPosition == 1)
                                {
                                    string pattern = "^[a-zA-Z0-9]{3,15}$";
                                    Regex rg = new Regex(pattern);

                                    // build password
                                    string password = "";
                                    do
                                    {
                                        CKI = Console.ReadKey(true);
                                        password = password + ((CKI.Key != ConsoleKey.Enter) ? CKI.KeyChar : "");

                                    } while (CKI.Key != ConsoleKey.Enter);

                                    if (rg.Match(password).Success)
                                    {
                                        newUser.password = password;
                                        formPosition++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Password did not meet requirements!");
                                    }
                                }
                                else if (formPosition == 2)
                                {
                                    Console.WriteLine("(y/n)");
                                    input = Console.ReadLine();
                                    if (input == "y")
                                    {
                                        formPosition++;
                                    }
                                    else if (input == "n")
                                    {
                                        chosenMenu = 0;
                                    }
                                    else
                                    {
                                        Console.WriteLine("wrong input. Please use either y or n");
                                    }
                                }

                            } while (formPosition < menu[chosenMenu].choices.Length);
                            stats.Add(new UserStats());
                            newUser.statsLink = stats.ToArray<UserStats>().Length - 1;
                            statsLink = newUser.statsLink;
                            existingUsersList.Add(newUser);
                            existingUsersArr = existingUsersList.ToArray<RegisteredUser>();
                            Handler.WriteJSON<UserStats[]>("/userdata", "stats.txt", stats.ToArray<UserStats>());
                            Handler.WriteJSON<RegisteredUser[]>("/userdata", "userdata.txt", existingUsersArr);

                            chosenMenu = 0;
                        }
                    }
                    if (chosenMenu == 2) // Login Menu
                    {
                        if (statsLink >= 0)
                        {
                            Console.WriteLine("You are already logged in!");
                            Console.WriteLine("Want to log out?");
                            Console.WriteLine("(y/n)");
                            input = Console.ReadLine();
                            if (input == "y")
                            {
                                statsLink = -1;
                                chosenMenu = 0;
                            }
                            else if (input == "n")
                            {
                                chosenMenu = 0;
                            }
                            else
                            {
                                Console.WriteLine("wrong input. Please use either y or n");
                            }
                        }
                        else
                        {
                            int formPosition = 0;

                            RegisteredUser[] existingUsersArr = Handler.ReadJSON<RegisteredUser[]>("/userdata", "userdata.txt");

                            RegisteredUser loggingUser = null;

                            do
                            {
                                if (formPosition == 0)
                                {
                                    Console.WriteLine("username: ");
                                    input = Console.ReadLine();


                                    int usernamePos = -1;
                                    for (int i = 0; i < existingUsersArr.Length; i++)
                                    {
                                        if (existingUsersArr[i].username == input)
                                        {
                                            usernamePos = i;
                                        }
                                    }
                                    if (usernamePos >= 0)
                                    {
                                        loggingUser = existingUsersArr[usernamePos];
                                        formPosition++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Username does not exist!");
                                        Console.WriteLine("Want to try again?");
                                        Console.WriteLine("(y/n)");
                                        input = Console.ReadLine();
                                        if (input == "y") { }
                                        else if (input == "n")
                                        {
                                            formPosition = 3;
                                            chosenMenu = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine("wrong input. Please use either y or n");
                                        }
                                    }
                                }
                                if (formPosition == 1)
                                {
                                    Console.WriteLine("password: ");
                                    // build password
                                    string password = "";
                                    do
                                    {
                                        CKI = Console.ReadKey(true);
                                        password = password + ((CKI.Key != ConsoleKey.Enter) ? CKI.KeyChar : "");

                                    } while (CKI.Key != ConsoleKey.Enter);

                                    if (loggingUser != null && loggingUser.password == password)
                                    {
                                        statsLink = loggingUser.statsLink;
                                        formPosition++;
                                        chosenMenu = 0;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Password was incorrect!");
                                        Console.WriteLine("Want to try again?");
                                        Console.WriteLine("(y/n)");
                                        input = Console.ReadLine();
                                        if (input == "y") { }
                                        else if (input == "n")
                                        {
                                            formPosition = 3;
                                            chosenMenu = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine("wrong input. Please use either y or n");
                                        }
                                    }
                                }
                            } while (formPosition < 2);
                            chosenMenu = 0;
                        }
                    }
                    if (chosenMenu == 3) // Game
                    {
                        Game newField = new Game(3,3);
                        newField.DrawField();

                        input = Console.ReadLine();
                    }
                    if (chosenMenu == 4) // Stats
                    {
                        if (statsLink >= 0)
                        {
                            UserStats[] statsArr = Handler.ReadJSON<UserStats[]>("/userdata", "stats.txt");
                            Console.WriteLine("You played " + statsArr[statsLink].timesPlayed + " times.");
                            Console.WriteLine("You have won" + statsArr[statsLink].timesWon + " games.");
                        }
                        else
                        {
                            Console.WriteLine("You are not logged in.");
                        }

                        Console.WriteLine("");
                        Console.WriteLine("[back]");
                        Console.ReadKey();
                        chosenMenu = 0;
                    }
                    if (chosenMenu == 5) // Exit Menu
                    {
                        Console.WriteLine("(y/n)");
                        input = Console.ReadLine();
                        if (input == "y")
                        {
                            input = "-1";
                        }
                        else if (input == "n")
                        {
                            input = "-2";
                            chosenMenu = 0;
                        }
                        else
                        {
                            Console.WriteLine("wrong input. Please use either y or n");
                            input = "-3";
                        }
                    }

                }

                // input = Console.ReadLine();


            } while (!(chosenMenu == 5 && Convert.ToInt32(input) == -1));
        }
    }
}
