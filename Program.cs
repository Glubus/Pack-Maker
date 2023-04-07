using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Coosu.Beatmap;
using Coosu.Shared;
using Coosu.Storyboard;
using System.IO;
using System.Collections;

namespace OsuPackGenerator
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Directory.Exists("files") == false)
            {
                Directory.CreateDirectory("files");
            }

            if (Directory.Exists("end") == false)
            {
                Directory.CreateDirectory("end");
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        public static void OsuPack(bool OG, bool difficulty, string PackName, string MapperName, string Artists, string Tags)
        {
            if (string.IsNullOrEmpty(Artists) == true){
                Artists = "Various Artists";
            }
            string[] dir = Directory.GetDirectories("files");
            int y = 0;

            foreach (string i in dir)
            {
                string[] dir2 = Directory.GetFiles(i);
                foreach (string z in dir2)
                {
                    FileInfo files1 = new FileInfo(z);
                    if (files1.Extension == ".osu")
                    {
                        OsuFile A = changeosu(z, MapperName, PackName, Artists, Tags, OG, difficulty);
                        string fileName = "end/" + y.ToString() + ".osu";
                        Console.WriteLine(z);
                        Console.WriteLine(A.General.AudioFilename);
                        A.General.AudioFilename = renameAudio(y, i + "/"+A.General.AudioFilename);
                        Console.WriteLine("CAMARCHE");
                        A.Events.BackgroundInfo.Filename = renameAudio(y, i +"/"+ A.Events.BackgroundInfo.Filename);
                        A.Save(fileName);
                        y++;
                    }
                }
            }
        }
        static OsuFile changeosu(string path, string Mapper, string PackName, string Artists, string Tags, bool Source, bool version)
        {
            OsuFile A = OsuFile.ReadFromFile(path);
            string Source1 = A.Metadata.Source;
            string version1 = "";
            List<string> Tags1 = new List<string> { };

            if (string.IsNullOrEmpty(Tags) == true)
            {
                Tags1 = A.Metadata.TagList;
            }
            else
            {
                string[] tagstab = Tags.Split('/');
                Tags1 = tagstab.ToList();
            }
            if (Source == true)
            {
                Source1 = A.Metadata.Creator;
            }
            if (version == true)
            {
                version1="["+A.Metadata.Version+"]";
            }
            string difficulty = A.Metadata.Artist + " - " + A.Metadata.Title + version1;
            A.Metadata.Title = PackName;
            A.Metadata.TitleUnicode = PackName;
            A.Metadata.Artist = Artists;
            A.Metadata.ArtistUnicode = Artists;
            A.Metadata.Creator = Mapper;
            A.Metadata.Version = difficulty;
            A.Metadata.Source = Source1;
            A.Metadata.TagList = Tags1;
            A.Metadata.BeatmapId = 0;
            A.Metadata.BeatmapSetId = -1;
            return A;
        }
        static string renameAudio(int y, string path)
        {
            FileInfo files = new FileInfo(path);
            string u = files.Extension;
            File.Move(path, "end/"+y.ToString()+ u);
            return y.ToString() + u;
        }
    }
}
