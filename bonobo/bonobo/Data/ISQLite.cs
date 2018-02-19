using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection(); //return an SQLite connection for each platform
    }
}
