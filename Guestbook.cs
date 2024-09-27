using System.Collections.Generic;
using System.IO;
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
            if(File.Exists(filename)==true)
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
            // skapa nytt inlägg
            Post obj = new Post();
            obj.Owner = o;
            obj.Text = t;
            posts.Add(obj);

            // spara inlägg 
            marsal();

            return obj;
        }

        // ta bort inlägg
        public int removePost(int index)
        {
            // ta bort inlägg ur listan
            posts.RemoveAt(index);

            // spara 
            marsal();

            return index;
        }

        // hämta inlägg
        public List<Post> getPosts()
        {
            return posts;
        }


        private void marsal()
        {
            // serialisera lista till json
            string jsonString = JsonSerializer.Serialize(posts);

            // skriv till fil
            File.WriteAllText(filename, jsonString);
        }
    }
}