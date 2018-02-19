using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using bonobo.Data;
using bonobo.iOS.Data;
using Foundation;
using UIKit;
using Xamarin.Forms;

//Assembly addition that will let the compiler know to use this platform specific code for the interface
[assembly: Dependency(typeof(SQLite_IOS))]

namespace bonobo.iOS.Data
{
    public class SQLite_IOS : ISQLite
    {
        public SQLite_IOS() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFileName = "TestDB.db3"; //name of the sqlite DB object
            //platform specific document path
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //library path
            var libraryPath = Path.Combine(documentPath, "..", "Library");
            //combine the library path and the file name
            var path = Path.Combine(documentPath, sqliteFileName);
            //make a connection using the full path
            var conn = new SQLite.SQLiteConnection(path);
            //return the connection
            return conn;
        }
    }
}