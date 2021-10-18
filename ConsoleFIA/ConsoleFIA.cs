using System;

namespace ConsoleFIA
{
    class ConsoleFIA
    {
        static void Main()
        {
            CreateRecord();
        }

        static void CreateRecord()
        {
            string name,
                tin,
                legalAddress,
                year;
            int quarter;
            decimal income,
                consumption,
                profit;
            double rentability;


            GetEnterprise(out name, out tin, out legalAddress);
            PrintEnterprise(name, tin, legalAddress);

            do
            {
                Console.WriteLine();
                GetFinancialResults(out year, out quarter, out income, out consumption);
                profit = CalculateProfit(income, consumption);
                rentability = CalculateRentability(profit, consumption);
                PrinResult(profit, rentability);


                Console.WriteLine("ПРОДОЛЖИТЬ - любая кнопка |" + "|  ВЫХОД - ESC");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

        }

        static void PrinResult(decimal profit, double rentability)
        {
            Console.WriteLine();
            Console.WriteLine($"Прибыль: {profit}");
            Console.WriteLine("Рентабельность: {0:P2}", rentability);
            Console.WriteLine("**********************************");
        }

        static void PrintEnterprise(string name, string tin, string legalAddress)
        {
            Console.WriteLine();
            Console.WriteLine("Предприятие");
            Console.WriteLine($"Название: {name}");
            Console.WriteLine($"ИНН: {tin}");
            Console.WriteLine($"Адрес: {legalAddress}");
            Console.WriteLine("----------------------------------");
        }

        private static double CalculateRentability(decimal profit, decimal consumption)
            => (double)Math.Round((profit / consumption), 2);


        private static decimal CalculateProfit(decimal income, decimal consumption)
            => (income - consumption);

        static void GetEnterprise(out string name, out string tin, out string legalAddress)
        {

            Console.WriteLine("Введите информацию о предприятии");
            Console.WriteLine();
            Input("Введите название предприятия: ", "NAME", out name);
            Input("Введите ИНН предприятия: ", "TIN", out tin);
            Input("Введите адрес предприятия: ", "ADDRESS", out legalAddress);

        }

        static void GetFinancialResults(out string year, out int quarter, out decimal income,
                out decimal consumption)
        {
            string quarterStr,
                incomeStr,
                consumptionStr;

            Input("Введите год (4 цифры): ", "YEAR", out year);

            do
            {
                Input("Введите номер квартала (1 - 4): ", "QUARTER", out quarterStr);
                quarter = int.Parse(quarterStr);
                if (quarter <= GetNumberOfAvailableQuarters(int.Parse(year)))
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


            Input("Введите доход за квартал (в рублях): ", "INCOME", out incomeStr);
            income = decimal.Parse(incomeStr);
            Input("Введите расход за квартал (в рублях): ", "CONSUMPTION", out consumptionStr);
            consumption = decimal.Parse(consumptionStr);
        }

        static bool InputControl(string input, out string checkedInput, out string inputError)
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

        static bool TINValidator(string tin, out string inputError)
        {
            if (tin.Length != 3)
            {
                inputError = "ИНН должен состоять из 3 цифр!"; // Из 10, просто так легче проверять :)
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

        static bool YearValidator(string yearStr, out string inputError)
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

        static bool QuaterValidator(string quarterStr, out string inputError)
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

        static int GetNumberOfAvailableQuarters(int year)
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

        static bool IncomeValidator(string incomeStr, out string inputError)
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

        static bool ConsumptionValidator(string consumptionStr, out string inputError)
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


        static void Input(string consoleRequest, string argumetType, out string argument)
        {
            bool isValid;
            string inputError;
            do
            {
                isValid = false;
                inputError = String.Empty;

                Console.Write($"{consoleRequest}");

                if (InputControl(Console.ReadLine(), out argument, out inputError))
                {
                    switch (argumetType)
                    {
                        case "NAME":
                        case "ADDRESS":
                            {
                                isValid = true;
                                break;
                            }
                        case "TIN":
                            {
                                isValid = TINValidator(argument, out inputError);
                                break;
                            }
                        case "YEAR":
                            {
                                isValid = YearValidator(argument, out inputError);
                                break;
                            }
                        case "QUARTER":
                            {
                                isValid = QuaterValidator(argument, out inputError);
                                break;
                            }
                        case "INCOME":
                            {
                                isValid = IncomeValidator(argument, out inputError);
                                break;
                            }
                        case "CONSUMPTION":
                            {
                                isValid = ConsumptionValidator(argument, out inputError);
                                break;
                            }
                        default:
                            {
                                inputError = "Не найден подходяхий валидатор";
                                isValid = false;
                                break;
                            }
                    }
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
        }
    }
}


