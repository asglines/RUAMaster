using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Life Path Calculator");
        Console.WriteLine("Enter a birthdate (MM/DD/YYYY) or 'x' to exit.");

        while (true)
        {
            Console.Write("\nYour input: ");
            string input = Console.ReadLine()?.Trim();

            if (string.Equals(input, "x", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nGoodbye! May your path always be clear.");
                break;
            }

            if (DateTime.TryParse(input, out DateTime birthDate))
            {
                int lifePath = CalculateLifePath(birthDate);

                Console.WriteLine($"\nYour Life Path Number is: {lifePath}");

                string meaning = GetLifePathMeaning(lifePath);
                Console.WriteLine(meaning);
            }
            else
            {
                Console.WriteLine("Invalid date format. Please use MM/DD/YYYY.");
            }
        }
    }

    static int CalculateLifePath(DateTime date)
    {
        // Combine digits of MMDDYYYY
        string allDigits = date.Month.ToString("00") + date.Day.ToString("00") + date.Year.ToString();
        int sum = 0;

        foreach (char digit in allDigits)
        {
            sum += (digit - '0');
        }

        // Reduce until single digit or master number
        while (!(sum == 11 || sum == 22 || sum == 33) && sum > 9)
        {
            sum = ReduceToDigits(sum);
        }

        return sum;
    }

    static int ReduceToDigits(int number)
    {
        int sum = 0;
        while (number > 0)
        {
            sum += number % 10;
            number /= 10;
        }
        return sum;
    }

    static string GetLifePathMeaning(int number)
    {
        var meanings = new Dictionary<int, (string Light, string Shadow)>
        {
            {1, ("Leader, independent, full of initiative.", "Can be selfish, stubborn, lonely, or paralyzed by fear of failure.")},
            {2, ("Peacemaker, cooperative, sensitive, harmony-driven.", "Can be clingy, indecisive, passive, or avoid conflict at all costs.")},
            {3, ("Creative, expressive, joyful, communicator.", "Can scatter energy, gossip, exaggerate, or avoid responsibility.")},
            {4, ("Disciplined, reliable, hardworking, builder of foundations.", "Can become rigid, controlling, stuck, or overly cautious.")},
            {5, ("Adventurous, curious, free-spirited, loves change.", "Can self-destruct with recklessness, addiction, or escapism.")},
            {6, ("Nurturing, responsible, loving, protector of family and community.", "Can be controlling, overbearing, or martyr-like.")},
            {7, ("Spiritual, analytical, introspective, truth-seeking.", "Can withdraw, become cynical, or isolate from others.")},
            {8, ("Ambitious, powerful, goal-driven, materially successful.", "Can be greedy, power-hungry, manipulative, or obsessed with status.")},
            {9, ("Compassionate, humanitarian, wise, inspirational.", "Can be self-righteous, scattered, or emotionally overwhelmed.")},
            {11, ("Master 11 – Spiritual Messenger: intuitive, inspirational, charismatic.",
                  "Shadow: anxious, ungrounded, overwhelmed by intensity, may feel cursed by pressure.")},
            {22, ("Master 22 – Master Builder: practical visionary, turns dreams into reality.",
                  "Shadow: crushed by responsibility, fearful of failure, may give up before achieving potential.")},
            {33, ("Master 33 – Master Teacher: compassionate, nurturing, full of wisdom and love.",
                  "Shadow: can be over-sacrificing, controlling, resentful, or burned out from giving too much.")}
        };

        if (meanings.ContainsKey(number))
        {
            var m = meanings[number];
            return $"Light Side: {m.Light}\nShadow Side: {m.Shadow}";
        }

        return "Your path is unique — the stars couldn’t quite pin it down!";
    }
}
