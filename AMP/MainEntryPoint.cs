using System;
using System.Windows.Forms;
using System.IO;
using Un4seen.Bass;

namespace ArientMusicPlayer {
    //Contains the entry point. Not really used lol.
    static class MainEntryPoint {

        //Entry Point/ Start
        #region Main

        [STAThread]
        static void Main() {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ArientWindow());

        }

        //Is only here for OnProcessExit to reference.
        public static void ExitApplication() {
            Logging.Debug("Program Exiting, freeing memory!");
            // free BASS
            if (Bass.BASS_Free()) {
                Logging.Debug("BASS released from memory!");
            } else {
                Logging.Debug("Error freeing BASS: " + Bass.BASS_ErrorGetCode());
            }
            Logging.Debug("Program Exited.");

            Application.Exit();
        }

        //OnExit. Hopefully it works when user does EndTask lol.
        static void OnProcessExit(object sender, EventArgs e) {
            ExitApplication();
        }

        #endregion


    }


    public static class Logging {

        public static readonly DateTime dateTime = DateTime.Now;

        static string currentSession = DateTime.Now.ToString().Replace("/","-").Replace(":",".").Replace(" ", "_");

        public static void Debug(string lines) {
            //Write the string to a file.append mode is enabled so that the log
            //lines get appended to  test.txt than wiping content and writing the log

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + "\\Log.txt", true)) {
                file.WriteLine("[" + DateTime.Now + "] " + lines);
            }
        }

        public static void Warning(string lines) {
            //Write the string to a file.append mode is enabled so that the log
            //lines get appended to  test.txt than wiping content and writing the log

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + "\\Log.txt", true)) {
                file.WriteLine("[" + DateTime.Now + "] WARNING:" + lines);
            }
        }

        public static void Error(string lines) {
            //Write the string to a file.append mode is enabled so that the log
            //lines get appended to  test.txt than wiping content and writing the log

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + "\\Log.txt", true)) {
                file.WriteLine("[" + DateTime.Now + "] ERROR: " + lines);
            }
        }
    }
}
