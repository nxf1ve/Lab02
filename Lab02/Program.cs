using System;
using System.Collections.Generic;

namespace Lab02
{
    public class Program
    {
        static List<int> GreedyCoinChange(List<int> coins, int amount)
        {
            // Сортируем монеты в порядке убывания их номиналов
            coins.Sort((a, b) => b.CompareTo(a));

            List<int> change = new List<int>(coins.Count);
            for (int i = 0; i < coins.Count; i++)
            {
                change.Add(0);
            }

            int remainingAmount = amount; // Оставшаяся сумма для размена

            // Проходимся по всем монетам и выбираем максимальное количество монет каждого номинала
            for (int i = 0; i < coins.Count; i++)
            {
                change[i] = remainingAmount / coins[i]; // Вычисляем количество монет
                remainingAmount -= change[i] * coins[i]; // Вычитаем из оставшейся суммы сумму, покрытую этими монетами

                if (remainingAmount == 0) // Если оставшаяся сумма стала равна 0, то размен завершен
                    break;
            }

            return change;
        }

        // Функция для нахождения наибольшей общей последовательности
        static string LongestCommonSubsequence(string str1, string str2)
        {
            int m = str1.Length;
            int n = str2.Length;

            // Создаем массив для хранения результатов подзадач
            int[,] dp = new int[m + 1, n + 1];

            // Заполняем массив dp с использованием динамического программирования
            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0)
                        dp[i, j] = 0;
                    else if (str1[i - 1] == str2[j - 1])
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    else
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }

            // Находим наибольшую общую последовательность, используя массив dp
            int index = dp[m, n];
            char[] lcs = new char[index];

            int p = m, q = n;
            while (p > 0 && q > 0)
            {
                if (str1[p - 1] == str2[q - 1])
                {
                    lcs[index - 1] = str1[p - 1];
                    p--;
                    q--;
                    index--;
                }
                else if (dp[p - 1, q] > dp[p, q - 1])
                    p--;
                else
                    q--;
            }

            return new string(lcs);
        }
        static void Main(string[] args)
        {
            List<int> coins = new List<int> { 1, 2, 5, 10, 20, 50, 100, 200 }; // Доступные номиналы монет
            Console.WriteLine("Введите сумму денег, которую нужно рассчитать в монетах: ");
            int amount = Convert.ToInt32(Console.ReadLine());
            List<int> change = GreedyCoinChange(coins, amount);

            // Выводим результат размена
            Console.WriteLine($"Минимальное количество монет для размена {amount} составляет:");
            for (int i = 0; i < coins.Count; i++)
            {
                if (change[i] > 0)
                    Console.WriteLine($"{coins[i]} руб.: {change[i]} монет");
            }

            string str1 = "GXTXAYB";
            string str2 = "AGGTAB";

            string lcs = LongestCommonSubsequence(str1, str2);
            Console.WriteLine("====================================");
            Console.WriteLine($"Наибольшая общая последовательность: {lcs}");



            Console.ReadKey();
        }
    }

}


