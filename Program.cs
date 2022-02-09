using System;
using System.Timers;
using System.Diagnostics;
using System.IO;
namespace HouseOfAshesAutoSave
{
    class Program
    {
        const int autoSaveIntervalMS = 1000 * 60 * 2;
        const String saveName = "SPSaveSlot0001.sav";
        public static String[] altSaveNames = { "SPSaveSlot0002.sav", "SPSaveSlot0003.sav" };
        public static int currAlt = 0;
        public static String pathToSaveFile = "\\SaveGames";
        public static String pathToFileSaved = "\\backups";
        public static String pathToSaveFolder = "C:\\Users\\Grant\\AppData\\" + "\\local" + "\\HouseOfAshes" + "\\Saved";

        static void Main(string[] args)
        {
            Timer autoSave = new Timer();
            autoSave.AutoReset = true;
            autoSave.Interval = autoSaveIntervalMS;
            autoSave.Elapsed += onExpired;
            autoSave.Start();
            Environment.CurrentDirectory = pathToSaveFolder;
            
            while (true)
            {

            }
            autoSave.Stop();
            autoSave.Dispose();
        }

        public static void onExpired(Object sender, ElapsedEventArgs e)
        {
            try
            {
                int currentSave = 0;
                String[] files = Directory.GetFiles(pathToSaveFolder + pathToFileSaved);
                foreach (String s in files)
                {
                    String[] parsed = s.Split("\\");
                    String fileName = parsed[parsed.Length-1];
                    int currstringid = int.Parse(fileName.Split(".")[0]);
                    if (currentSave < currstringid) { currentSave = currstringid; }
                }
                currentSave++;
                File.Copy(pathToSaveFolder + pathToSaveFile + "\\" + saveName, pathToSaveFolder + pathToFileSaved + "\\" + currentSave.ToString() + ".sav");
                File.Delete(pathToSaveFolder + pathToSaveFile + "\\" + altSaveNames[currAlt]);
                File.Copy(pathToSaveFolder + pathToSaveFile + "\\" + saveName, pathToSaveFolder + pathToSaveFile + "\\" + altSaveNames[currAlt]);
                _ = currAlt < altSaveNames.Length-1 ? currAlt++ : currAlt=0;
                Console.WriteLine(DateTime.Now.ToLocalTime() + " Saved File: " + currentSave.ToString() + ".sav");
            }
            catch (Exception err)
            {
                Console.WriteLine("Error Copying Save File: " + err);
            }
        }



    }
}
