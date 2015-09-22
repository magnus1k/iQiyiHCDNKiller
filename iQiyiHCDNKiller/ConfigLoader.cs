namespace iQiyiHCDNKiller
{
    public class ConfigLoader
    {
        public int waitexit = 3000;
        public string clientpath = @"C:\Program Files (x86)\IQIYI Video\PStyle";
        public int waitstart = 3000;

        private static ConfigLoader _instance = new ConfigLoader();
        public bool inited = false;

        public static ConfigLoader instance
        {
            get
            {
                _instance.init();
                return _instance;
            }
        }

        private ConfigLoader()
        {
        }

        private void init()
        {
            if (!inited)
            {
                string inifile = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "config.ini");
                if (System.IO.File.Exists("config.ini"))
                {
                    IniHelper ih = new IniHelper(inifile);
                    clientpath = ih.ReadValue("Path", "QyClient");
                    string strstart = ih.ReadValue("Config", "WaitStart");
                    waitstart = int.Parse(strstart);
                    waitexit = int.Parse(ih.ReadValue("Config", "WaitExit"));
                }
                else
                {
                    IniHelper ih = new IniHelper(inifile);
                    ih.WriteValue("Path", "QyClient", clientpath);
                    ih.WriteValue("Config", "WaitStart", waitstart.ToString());
                    ih.WriteValue("Config", "WaitExit", waitexit.ToString());
                }
                inited = true;
            }
        }
    }
}