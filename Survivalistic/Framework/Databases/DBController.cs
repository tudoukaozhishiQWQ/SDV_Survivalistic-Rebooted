using StardewModdingAPI;
using System.Linq;

namespace Survivalistic_Rebooted.Framework.Databases
{
    static class DBController
    {
        public static void LoadDatabases()
        {
            EdiblesDB _actualModDatabase = ModEntry.Instance.Helper.Data.ReadJsonFile<EdiblesDB>("assets/Databases/BaseGame_edibles.json");
            if (_actualModDatabase != null)
            {
                for (var i = 0; i < _actualModDatabase.Edibles.Length / 2; i++)
                {
                    Foods.FoodDatabase.Add(_actualModDatabase.Edibles[i, 0], _actualModDatabase.Edibles[i, 1]);
                }
            }

            foreach (IModInfo _mod in ModEntry.Instance.Helper.ModRegistry.GetAll().ToList())
            {
                _actualModDatabase = ModEntry.Instance.Helper.Data.ReadJsonFile<EdiblesDB>($"assets/Databases/{_mod.Manifest.UniqueID}_edibles.json");
                if (_actualModDatabase != null)
                {
                    for (var i = 0; i < _actualModDatabase.Edibles.Length / 2; i++)
                    {
                        try
                        {
                            Foods.FoodDatabase.Add(_actualModDatabase.Edibles[i, 0], _actualModDatabase.Edibles[i, 1]);
                        }
                        catch (System.ArgumentException exception)
                        {
                            ModEntry.Instance.Monitor.Log(_mod.Manifest.UniqueID + "_edibles Attempted to add duplicate entry to EdiblesDB", LogLevel.Trace);
                            ModEntry.Instance.Monitor.Log(exception.Message, LogLevel.Trace);
                            ModEntry.Instance.Monitor.Log(exception.StackTrace, LogLevel.Trace);
                        }

                    }
                }
            }
        }
    }
}
