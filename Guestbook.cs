using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;


namespace guestbook
{
    public class GuestBook
    {
        // variabel för json filen
        private string filename = @"guestbook.json";

        // initiera lista
        private List<Post> posts = new List<Post>();

        // konstruktor
        public GuestBook()
        {
            // kontrollera att filen finns
            if (File.Exists(filename) == true)
            {
                // läs innehållet i filen
                string jsonString = File.ReadAllText(filename);

                // deserialisera json till lista (! i slutet för att tala om att det inte är null)
                posts = JsonSerializer.Deserialize<List<Post>>(jsonString)!;
            }
        }

        // lägg till inlägg
        public Post addPost(string o, string t)
        {
            if (string.IsNullOrEmpty(o) || o.Length > 20)
            {
                throw new ArgumentException("Namnet måste vara mellan 1-20 tecken");
            }

            if (string.IsNullOrEmpty(t) || t.Length > 100)
            {
                throw new ArgumentException("Texten får ej vara tom eller längre än 100 tecken");
            }

            // skapa nytt inlägg
            Post obj = new Post();
            obj.Owner = o;
            obj.Text = t;
            posts.Add(obj);

            // spara inlägg 
            marshal();

            return obj;
        }

        // ta bort inlägg
        public int removePost(int index)
        {
            if (index >= 0 && index < posts.Count)
            {
                posts.RemoveAt(index);  //ta bort inlägg ur listan
                marshal();  // spara inlägg
                return index;   
            }
            else{
                throw new ArgumentOutOfRangeException("Index out of range");
            }
         
        }

        // hämta inlägg
        public List<Post> getPosts()
        {
            return posts;
        }


        private void marshal()
        {
            // serialisera lista till json
            string jsonString = JsonSerializer.Serialize(posts);

            // skriv till fil
            File.WriteAllText(filename, jsonString);
        }
    }
}