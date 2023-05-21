using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App.DB
{
    public class ConfigRepository
    {
        #region Var
        private SQLiteConnection con;
        private static ConfigRepository instancia;
        public string MensageNotification { get; private set; }
        #endregion

        #region Constructor
        public static ConfigRepository Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ConfigRepository(
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "configuser.db3"));
                }
                return instancia;
            }
        }
        #endregion

        #region Methods
        public static void Inicializador(string Filename)
        {
            if (Filename == null)
                throw new ArgumentNullException();

            if (Instancia != null)
                instancia.con.Close();

            instancia = new ConfigRepository(Filename);
        }

        private ConfigRepository(string dbPath)
        {
            con = new SQLiteConnection(dbPath);
            con.CreateTable<ConfigUser>();
        }

        public IEnumerable<ConfigUser> GetConfigUser()
        {
            try
            {
                return con.Table<ConfigUser>();
            }
            catch (Exception exMess)
            {
                MensageNotification = exMess.Message;
            }

            return Enumerable.Empty<ConfigUser>();
        }

        public int AddConfigUser(ConfigUser modelconfig)
        {
            int result = 0;

            try
            {

                result = con.Insert(modelconfig);
                return result;
            }
            catch (Exception exM)
            {
                MensageNotification = exM.Message;
                throw;
            }
        }

        public int updateConfig(ConfigUser modelconfig)
        {
            int result = 0;

            try
            {

                result = con.Update(modelconfig);
                return result;
            }
            catch (Exception exM)
            {
                MensageNotification = exM.Message;
                throw;
            }
        }

        public void deleteConfig(ConfigUser modelconfig)
        {
            try
            {
                con.Delete(modelconfig);
            }
            catch { }
        }
        public void deleteAll()
        {
            try
            {
                var grp = GetConfigUser();
                foreach (var itemG in grp)
                    con.Delete(itemG);
            }
            catch (Exception exM)
            {
                Console.WriteLine("Error: " + exM.Message);
            }
        }
        #endregion
    }
}
