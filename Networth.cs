using System;
using System.IO;
using System.Net;

class Program
{
    static string PlayerName { get; set; } = string.Empty;

    static string FormatNumber(string number)
    {
        int dotIndex = number.IndexOf(".", StringComparison.Ordinal) != -1 ?
            number.IndexOf(".", StringComparison.Ordinal) :
            number.IndexOf(",", StringComparison.Ordinal);
            dotIndex = dotIndex != -1 ? dotIndex : -1;

        if (dotIndex > -1)
        {
            number = number.Substring(0, dotIndex);
        }

        string formattedNumber = string.Empty;
        int count = 0;
        for (int i = number.Length - 1; i >= 0; i--)
        {
            count++;
            formattedNumber = number[i] + formattedNumber;
            if (count == 3 && i != 0)
            {
                formattedNumber = " " + formattedNumber;
                count = 0;
            }
        }

        return formattedNumber;
    }

    static void Main(string[] args)
    {
        Console.Clear();
        Console.Write("Enter the player name: ");
        PlayerName = Console.ReadLine();
        Console.Clear();

        var apiUrl = $"https://sky.shiiyu.moe/api/v2/coins/{PlayerName}";
        var request = (HttpWebRequest)WebRequest.Create(apiUrl);
        request.Method = "GET";

        HttpWebResponse response;
        try
        {
            response = (HttpWebResponse)request.GetResponse();
        }
        catch (WebException ex)
        {
            Console.WriteLine("Error while retrieving data from API. Please try again later.");
            Console.WriteLine(ex.Message);
            return;
        }

        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            string json = reader.ReadToEnd();

            int startIndex = json.IndexOf("profiles\":") + "profiles\":".Length;
            int endIndex = json.LastIndexOf("}") + 1;
            string profilesJson = json.Substring(startIndex, endIndex - startIndex);

            string[] profiles = profilesJson.Split(new[] { "}," }, StringSplitOptions.None);

            Console.WriteLine($"\nUsername: {PlayerName}");
            Console.WriteLine("------------------------------");

            foreach (string profile in profiles)
            {
                int nameStartIndex = profile.IndexOf("cute_name\":\"") + "cute_name\":\"".Length;
                int nameEndIndex = profile.IndexOf("\",", nameStartIndex, StringComparison.Ordinal);
                string name = profile.Substring(nameStartIndex, nameEndIndex - nameStartIndex);

                int purseStartIndex = profile.IndexOf("purse\":") + "purse\":".Length;
                int purseEndIndex = profile.Length - 1;
                if (profile.Contains("bank\":"))
                {
                    purseEndIndex = profile.IndexOf("bank\":") - 1;
                }

                string purse = profile.Substring(purseStartIndex, purseEndIndex - purseStartIndex);
                purse = FormatNumber(purse);

                Console.WriteLine($"Profile: {name}");
                Console.WriteLine($"Purse: {purse}");
                Console.WriteLine("------------------------------");
            }
        }
        Console.ReadLine();
    }
}