using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using bonobo.Data;
using bonobo.Droid.Data;
using Xamarin.Forms;

//Assembly addition that will let the compiler know to use this platform specific code for the interface
[assembly: Dependency(typeof(SQLite_Android))]

namespace bonobo.Droid.Data
{
    class SQLite_Android : ISQLite
    {
        public SQLite_Android() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFileName = "TestDB.db3"; //name of the sqlite DB object
            //platform specific document path
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //combine tha path and the file name
            var path = Path.Combine(documentPath, sqliteFileName);
            //make a connection using the full path
            var conn = new SQLite.SQLiteConnection(path);
            //return the connection
            return conn;
        }
    }
}