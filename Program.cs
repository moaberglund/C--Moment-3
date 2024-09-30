/*************************************************************
    Gästbok
    
    C# program som skapar en gästbok där användaren kan skriva
    inlägg och ta bort inlägg. Inläggen sparas i en json fil.

    Kodad av Moa Berglund / mobe2305 Mittuniversitetet
***************************************************************/

using System;

namespace guestbook
{

    class Program
    {

        static void Main(string[] args)
        {

            // skapa ny gästbok
            GuestBook gb = new GuestBook();
            int i = 0;

            while (true)
            {
                // Töm konsolen
                Console.Clear();
                Console.CursorVisible = false;

                Console.WriteLine("Moas Gästbok\n\n");
                Console.WriteLine("1. Lägg till inlägg");
                Console.WriteLine("2. Ta bort inlägg\n");
                Console.WriteLine("X. Avsluta\n\n");

                i = 0;
                // skriv ut inläggen
                foreach (Post p in gb.getPosts())
                {
                    Console.WriteLine($"[{i++}] {p.Owner} - {p.Text}");
                }

                // Läs in användarens val
                int choice = (int)Console.ReadKey(true).Key;
                switch (choice)
                {
                    case 49: // 1
                        Console.CursorVisible = true;
                        Console.WriteLine("\nLägg till inlägg\n");

                        // Be om namn
                        Console.Write("Namn: ");
                        string? owner = Console.ReadLine();

                        // Be om text
                        Console.Write("Text: ");
                        string? text = Console.ReadLine();

                        try
                        {
                            // Anropa addpost som innehåller validering
                            gb.addPost(owner, text);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"\nFel: {ex.Message}");
                        }

                        Console.WriteLine("Tryck på valfri knapp för att gå vidare");
                        Console.ReadKey();
                        break;


                    case 50: // 2
                        Console.CursorVisible = true;
                        Console.WriteLine("\nTa bort inlägg nummer:\n");
                        string? index = Console.ReadLine();

                        if (!String.IsNullOrEmpty(index))
                            try
                            {
                                gb.removePost(Convert.ToInt32(index));
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("Felaktigt index\nTryck på valfri knapp för att gå vidare");
                                Console.ReadKey();
                            }

                        break;
                    case 88: // X
                        Environment.Exit(0);
                        return;
                }


            }
        }


    }
}