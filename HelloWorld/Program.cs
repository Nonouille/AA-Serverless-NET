namespace HelloWorld;

using System.Diagnostics;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

class Personne
{
    public string Nom { get; set; }
    public int Age { get; set; }

    public Personne(string Nom, int Age)
    {
        this.Nom = Nom;
        this.Age = Age;
    }

    public void Hello(bool isLowercase)
    {
        if (isLowercase)
        {
            Console.WriteLine($"hello {Nom}, you are {Age}");
        }
        else
        {
            Console.WriteLine($"HELLO {Nom}, YOU ARE {Age}");
        }
    }
}

class Program
{

    static string chemin_images =
        @"C:\Users\arnau\OneDrive - CentraleSupelec\Architecture applicative\TP4 - .NET\images\";
    static string nouveau_chemin_image =
        @"C:\Users\arnau\OneDrive - CentraleSupelec\Architecture applicative\TP4 - .NET\images\nouvelles\";


    static void Main(string[] args)
    {
        Personne p = new Personne("Arnaud", 22);
        string json = JsonConvert.SerializeObject(p, Formatting.Indented);
        Console.WriteLine(json);

        Stopwatch stopwatch = Stopwatch.StartNew();

        Parallel.ForEach(Directory.EnumerateFiles(chemin_images), (chemin_image) =>
            {
                TraiterImage(chemin_image, nouveau_chemin_image + Path.GetFileName(chemin_image));
            }
        );

        stopwatch.Stop();
        Console.WriteLine($"Temps d'exécution parallélisé : {stopwatch.ElapsedMilliseconds} ms");

        Stopwatch stopwatch2 = Stopwatch.StartNew();

        foreach (string chemin_image in Directory.EnumerateFiles(chemin_images))
        {
            TraiterImage(chemin_image, nouveau_chemin_image + Path.GetFileName(chemin_image));
        }

        stopwatch2.Stop();
        Console.WriteLine($"Temps d'exécution sérialisé : {stopwatch2.ElapsedMilliseconds} ms");
    }

    public static void TraiterImage(string chemin_image, string nouveau_chemin_image)
    {
        using (Image image = Image.Load(chemin_image))
        {
            image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2).Grayscale());
            image.Save(nouveau_chemin_image);
        }
    }
}
