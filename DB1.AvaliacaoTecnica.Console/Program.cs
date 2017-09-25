using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB1.AvaliacaoTecnica.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int menuOption;

                do
                {
                    menuOption = DisplayMenu();
                    switch (menuOption)
                    {
                        case 1:
                            Collatz();
                            break;
                        case 2:
                            EvenOrOdd();
                            break;
                        case 3:
                            ExceptArrays();
                            break;
                        default:
                            Exit();
                            break;
                    }
                    System.Console.ReadKey();
                    System.Console.Clear();
                }
                while (menuOption != 0);
            }
            catch (Exception)
            {
                System.Console.WriteLine("Ocorreu um erro inesperado.");
            }
        }

        static int DisplayMenu()
        {
            System.Console.WriteLine("-------------------------------------");
            System.Console.WriteLine("DB1 Global Software - Avaliação Técnica");
            System.Console.WriteLine();
            System.Console.WriteLine("[ 1 ] Collatz");
            System.Console.WriteLine("[ 2 ] Par ou ímpar");
            System.Console.WriteLine("[ 3 ] Diferença entre arrays");
            System.Console.WriteLine("[ 0 ] Sair");
            System.Console.WriteLine("-------------------------------------");
            System.Console.Write("Digite uma opção: ");

            Int32.TryParse(System.Console.ReadLine(), out int menuOption);

            return menuOption;
        }

        static void Exit()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Digite qualquer tecla para sair...");
        }

        static void BackToMenu()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Digite qualquer tecla para voltar ao menu...");
        }

        static void Loading(long iteration, long limit)
        {
            if (iteration == (limit * 0.25) || iteration == (limit * 0.50) || iteration == (limit * 0.75))
            {
                System.Console.Write("...");
            }
        }

        static void PrintList(string message, List<int> list, bool newLine = true)
        {
            if (!string.IsNullOrEmpty(message))
                System.Console.WriteLine(message);

            list.ForEach(i => System.Console.Write("{0} ", i.ToString()));

            if(newLine)
                System.Console.WriteLine();
        }

        static bool IsEven(long num)
        {
            return ((num % 2) == 0);
        }

        static bool IsOdd(long num)
        {
            return ((num % 2) != 0);
        }

        static void Collatz()
        {
            System.Console.WriteLine("\n---------- Collatz ----------");

            long num = 0, maxNum = 0, startNum = 0, aux = 0;
            long limit = 1000000;
            long count = 1;

            System.Console.Write("Processando");

            for (int i = 1; i <= limit; i++)
            {
                num = i;
                do
                {
                    if (IsEven(num))
                    {
                        aux = num / 2;
                    }
                    else
                    {
                        aux = (3 * num) + 1;
                    }
                    num = aux;
                    count++;
                } while (aux != 1);

                if (count > maxNum) 
                {
                    maxNum = count; 
                    startNum = i; 
                }

                aux = 0;
                count = 1;
                Loading(i, limit);
            }

            System.Console.WriteLine("\nNúmero inicial entre 1 e 1 milhão que produz a maior sequência: {0}", startNum);
        }

        static void EvenOrOdd()
        {
            System.Console.WriteLine("\n---------- Par ou ímpar ----------");

            int[] numbers = new int[11] { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };

            if (numbers.Any())
            {
                var evenList = from num in numbers
                               where IsEven(num)
                               select num;

                var oddList = from num in numbers
                              where IsOdd(num)
                              select num;

                bool allEven = numbers.All(num => IsEven(num));
                bool allOdd = numbers.All(num => IsOdd(num));

                PrintList("Sequência: ", numbers.ToList());

                if (allOdd)
                {
                    System.Console.WriteLine("\nA sequência contém apenas números ímpares.");
                }
                else if (allEven)
                {
                    System.Console.WriteLine("\nA sequência contém apenas números pares.");
                }
                else
                {
                    System.Console.WriteLine("\nA sequência contém números pares e ímpares.");
                }

                if (evenList.Any())
                {
                    PrintList("Números pares da sequência: ", evenList.ToList());
                }

                if (oddList.Any())
                {
                    PrintList("Números ímpares da sequência: ", oddList.ToList());
                }

                BackToMenu();
            }
        }

        static void ExceptArrays()
        {
            System.Console.WriteLine("\n---------- Diferença entre arrays ----------");

            int[] firstArray = new int[8] { 1, 3, 7, 29, 42, 98, 234, 93 };
            int[] secondArray = new int[7] { 4, 6, 93, 7, 55, 32, 3 };

            PrintList("\nPrimeiro array: ", firstArray.ToList());
            PrintList("\nSegundo array: ", secondArray.ToList());
            PrintList("\nNúmeros do primeiro array que não estão contidos no segundo array: ", firstArray.Except(secondArray).ToList());

            BackToMenu();
        }
    }
}
