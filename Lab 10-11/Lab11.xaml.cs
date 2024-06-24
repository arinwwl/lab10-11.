using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Lab10_11
{
    public partial class Lab11 : Page
    {
        private List<Musician> musicians;

        public Lab11()
        {
            InitializeComponent();
            musicians = new List<Musician>();
            LoadMusicians();
        }

        private void LoadMusicians()
        {
            // Загрузка данных из бинарного файла
            if (File.Exists("musicians.dat"))
            {
                using (BinaryReader reader = new BinaryReader(File.Open("musicians.dat", FileMode.Open)))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        string name = reader.ReadString();
                        string album = reader.ReadString();
                        int circulation = reader.ReadInt32();
                        int year = reader.ReadInt32();
                        double price = reader.ReadDouble();

                        musicians.Add(new Musician
                        {
                            Name = name,
                            AlbumName = album,
                            Circulation = circulation,
                            ReleaseYear = year,
                            Price = price
                        });
                    }
                }
            }
            else
            {
                // Инициализация данных, если файл не найден
                Musician[] initialMusicians = new Musician[]
                {
                    new Musician { Name = "John Coltrane", AlbumName = "A Love Supreme", Circulation = 1500, ReleaseYear = 1965, Price = 60 },
                    new Musician { Name = "Miles Davis", AlbumName = "Kind of Blue", Circulation = 2000, ReleaseYear = 1959, Price = 70 },
                    new Musician { Name = "Ella Fitzgerald", AlbumName = "Ella and Louis", Circulation = 800, ReleaseYear = 1956, Price = 45 },
                    new Musician { Name = "Duke Ellington", AlbumName = "Ellington at Newport", Circulation = 1200, ReleaseYear = 1956, Price = 55 },
                };

                musicians.AddRange(initialMusicians);
                SaveMusicians(); // Сохранение данных в файл
            }

            // Вывод информации об исполнителе
            DisplayMusicianInfo();
        }

        private void DisplayMusicianInfo()
        {
            // Получение исполнителя, который выпустил альбом в текущем году
            Musician targetMusician = musicians.FirstOrDefault(m => m.ReleaseYear == DateTime.Now.Year && m.Circulation > 1000 && m.Price >= 50);

            if (targetMusician != null)
            {
                MessageBox.Show($"Name: {targetMusician.Name}, Album: {targetMusician.AlbumName}, Circulation: {targetMusician.Circulation}, Release Year: {targetMusician.ReleaseYear}, Price: {targetMusician.Price} grn");
            }
            else
            {
                MessageBox.Show("Нет исполнителей, соответствующих критериям.");
            }
        }

        // Сохранение данных в бинарный файл
        private void SaveMusicians()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open("musicians.dat", FileMode.Create)))
            {
                foreach (Musician musician in musicians)
                {
                    writer.Write(musician.Name);
                    writer.Write(musician.AlbumName);
                    writer.Write(musician.Circulation);
                    writer.Write(musician.ReleaseYear);
                    writer.Write(musician.Price);
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Сохранение данных в JSON файл
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JSON файлы (*.json)|*.json";

            if (sfd.ShowDialog() == true)
            {
                string json = JsonSerializer.Serialize(musicians);
                File.WriteAllText(sfd.FileName, json);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Загрузка данных из JSON файла
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON файлы (*.json)|*.json";
            if (ofd.ShowDialog() == true)
            {
                string json = File.ReadAllText(ofd.FileName);
                List<Musician> desMusicians = JsonSerializer.Deserialize<List<Musician>>(json)!;
                musicians.AddRange(desMusicians);
                DisplayMusicianInfo();
            }
        }
    }

}











//using Microsoft.Win32;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using static System.Net.WebRequestMethods;


//namespace Lab10_11
//{
//    public partial class Lab11 : Page
//    {
//        private List<Musician> films;
//        public Lab11()
//        {
//            InitializeComponent();
//            LoadMusicians();
//        }


//        private void LoadMusicians()
//        {
//            Musician[] musicians = new Musician[]
//            {
//            new Musician { Name = "John Coltrane", AlbumName = "A Love Supreme", Circulation = 1500, ReleaseYear = 1965, Price = 60 },
//            new Musician { Name = "Miles Davis", AlbumName = "Kind of Blue", Circulation = 2000, ReleaseYear = 1959, Price = 70 },
//            new Musician { Name = "Ella Fitzgerald", AlbumName = "Ella and Louis", Circulation = 800, ReleaseYear = 1956, Price = 45 },
//            new Musician { Name = "Duke Ellington", AlbumName = "Ellington at Newport", Circulation = 1200, ReleaseYear = 1956, Price = 55 },
//            };

//            using (BinaryWriter writer = new BinaryWriter(File.Open("musicians.dat", FileMode.Create)))
//            {
//                foreach (Musician musician in musicians)
//                {
//                    writer.Write(musician.Name);
//                    writer.Write(musician.AlbumName);
//                    writer.Write(musician.Circulation);
//                    writer.Write(musician.ReleaseYear);
//                    writer.Write(musician.Price);
//                }
//            }

//            using (BinaryReader reader = new BinaryReader(File.Open("musicians.dat", FileMode.Open)))
//            {
//                while (reader.BaseStream.Position != reader.BaseStream.Length)
//                {
//                    string name = reader.ReadString();
//                    string album = reader.ReadString();
//                    int circulation = reader.ReadInt32();
//                    int year = reader.ReadInt32();
//                    double price = reader.ReadDouble();

//                    if (year == DateTime.Now.Year && circulation > 1000 && price >= 50)
//                    {
//                        MessageBox.Show($"Name: {name}, Album: {album}, Circulation: {circulation}, Release Year: {year}, Price: {price} grn");
//                    }
//                }
//            }
//        }



//        private void Button_Click_1(object sender, RoutedEventArgs e)
//        {
//            SaveFileDialog sfd = new SaveFileDialog();
//            sfd.Filter = "Бинарные файлы(*.dat)|*.dat";

//            if (sfd.ShowDialog() == true)
//            {
//                using (BinaryWriter writer = new BinaryWriter(System.IO.File.Open(sfd.FileName, FileMode.OpenOrCreate)))
//                {
//                    string json = JsonSerializer.Serialize(films);
//                    writer.Write(json);
//                }
//            }
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            OpenFileDialog ofd = new OpenFileDialog();
//        ofd.Filter = "Бинарные файлы(*.dat)|*.dat";
//        if (ofd.ShowDialog() == true)
//        {
//            using (BinaryReader reader = new BinaryReader(System.IO.File.Open(ofd.FileName, FileMode.Open)))
//            {
//                string json = reader.ReadString();
//                List<Musician> desFilms = JsonSerializer.Deserialize<List<Musician>>(json)!;
//                foreach (Musician f in desFilms)
//                {
//                    films.Add(f);
//                }

//            }
//        }
//        }


//        //добавить
//        //private void Button_Click(object sender, RoutedEventArgs e)
//        //{

//        //}
//    }

//}


