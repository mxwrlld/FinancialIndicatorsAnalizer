using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFIA.Controllers
{
    static class InputValidator
    {
        private delegate bool Validator(string input, out string inputError);

        public static string ReadName()
        {
            return Input("Введите название предприятия: ", NameValidator);
        }

        public static string ReadTIN()
        {
            return Input("Введите ИНН предприятия: ", TINValidator);
        }

        public static string ReadAddress()
        {
            return Input("Введите адрес предприятия: ", AddressValidator);
        }
        
        public static int ReadYear()
        {
            string yearStr = Input("Введите год (4 цифры): ", YearValidator);
            return int.Parse(yearStr); 
        }

        public static int ReadQuarter(int year)
        {
            string quarterStr;
            int quarter;

            do
            {
                quarterStr = Input("Введите номер квартала (1 - 4): ", QuaterValidator);
                quarter = int.Parse(quarterStr);
                if (quarter <= GetNumberOfAvailableQuarters(year))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("К сожалению введённый вами квартал пока недоступен");
                    Console.WriteLine("Приходите позже)");
                    Console.WriteLine();
                }
            } while (true);

            return quarter;
        }

        public static decimal ReadIncome()
        {
            string incomeStr = Input("Введите доход за квартал (в рублях): ", IncomeValidator);
            return decimal.Parse(incomeStr);
        }

        public static decimal ReadConsumption()
        {
            string consumptionStr = Input("Введите расход за квартал (в рублях): ", ConsumptionValidator);
            return decimal.Parse(consumptionStr);
        }

        private static bool InputControl(string input, out string checkedInput, out string inputError)
        {
            input = input.Trim();
            checkedInput = input;
            if (!(input.Length >= 1))
            {
                inputError = "Просьба не игнорировать ввод";
                return false;
            }
            inputError = String.Empty;
            return true;
        }


        private static bool NameValidator(string name, out string inputError)
        {
            inputError = "Ввод прошёл валидацию";
            return true;
        }

        private static bool AddressValidator(string legalAddress, out string inputError)
        {
            inputError = "Ввод прошёл валидацию";
            return true;
        }

        private static bool TINValidator(string tin, out string inputError)
        {
            if (tin.Length != 10)
            {
                inputError = "ИНН должен состоять из 10 цифр!"; // Из 10, просто так легче проверять :)
                return false;
            }
            if (tin.Substring(0, 2) == "00")
            {
                inputError = "Диапазон кодов региона - [01 - 99]";
                return false;
            }
            inputError = "Ввод прошёл валидацию";
            return true;
        }

        private static bool YearValidator(string yearStr, out string inputError)
        {
            int year;
            DateTime currentTime = DateTime.Now;
            if (yearStr.Length != 4)
            {
                inputError = "Год состоит из 4 цифр!";
                return false;
            }
            if (!int.TryParse(yearStr, out year))
            {
                inputError = "Год - число";
                return false;
            }
            if (year > currentTime.Year)
            {
                inputError = "Ввод данных за будущее запрещён";
                return false;
            }
            if (year == currentTime.Year && GetNumberOfAvailableQuarters(year) == 0)
            {
                inputError = "Текущий год не богат на кварталы";
                return false;
            }
            inputError = "Ввод прошёл валидацию";
            return true;
        }

        private static bool QuaterValidator(string quarterStr, out string inputError)
        {
            int quarter;
            if (!int.TryParse(quarterStr, out quarter))
            {
                inputError = "Квартал - целое число";
                return false;
            }
            if (quarter < 1 || quarter > 4)
            {
                inputError = "Квартал - целое число в диапазоне [1 - 4]";
                return false;
            }
            inputError = "Ввод прошёл валидацию";
            return true;
        }

        private static int GetNumberOfAvailableQuarters(int year)
        {
            DateTime currentTime = DateTime.Now;
            int month = currentTime.Month;
            if (year == currentTime.Year)
            {
                if (month >= 10)
                    return 3;
                if (month >= 7)
                    return 2;
                if (month >= 4)
                    return 1;
                return 0;
            }
            return 4;
        }

        private static bool IncomeValidator(string incomeStr, out string inputError)
        {
            decimal income;
            if (!decimal.TryParse(incomeStr, out income))
            {
                inputError = "Доход указывается числом!";
                return false;
            }
            if (income < 0)
            {
                inputError = "Минимальная величина дохода - 0 руб.";
                return false;
            }
            inputError = "Ввод прошёл валидацию";
            return true;
        }

        private static bool ConsumptionValidator(string consumptionStr, out string inputError)
        {
            decimal consumption;
            if (!decimal.TryParse(consumptionStr, out consumption))
            {
                inputError = "Расход указывается числом!";
                return false;
            }
            if (consumption < 0)
            {
                inputError = "Минимальная величина расхода - 0 руб.";
                return false;
            }
            inputError = "Ввод прошёл валидацию";
            return true;
        }

        private static string Input(string consoleRequest, Validator validator)
        {
            string argument,
                inputError;
            bool isValid;

            do
            {
                isValid = false;
                inputError = String.Empty;

                Console.Write($"{consoleRequest}");

                if (InputControl(Console.ReadLine(), out argument, out inputError))
                {
                    isValid = validator(argument, out inputError);
                }

                if (!isValid)
                {
                    Console.WriteLine($"Ошибка ввода: {inputError}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                    break;
                }

            } while (true);

            return argument;
        }

        public static int ReadInput(List<int> validValues)
        {
            int inputInt;
            bool isValid = false;
            do
            {
                Console.WriteLine();
                Console.Write("Ввод: ");
                string input = Console.ReadLine();
                inputInt = int.Parse(input);
                foreach (var value in validValues)
                {
                    if (inputInt == value)
                    {
                        isValid = true;
                        break;
                    }
                }
                if (isValid)
                    break;
                Console.WriteLine("Ошибка ввода: введено недопустимое значение");
            } while (true);
            return inputInt;
        }

    }
}
