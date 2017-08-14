using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Data.SQLite;

namespace CefTest
{
    public class ChromeCookieReader
    {
        //public IEnumerable<Tuple<string, string>> ReadCookies(string hostName)
        //{
        //    if (hostName == null) throw new ArgumentNullException("hostName");

        //    var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data\Default\Cookies";
        //    if (!System.IO.File.Exists(dbPath)) throw new System.IO.FileNotFoundException("Cant find cookie store", dbPath); // race condition, but i'll risk it

        //    var connectionString = "Data Source=" + dbPath + ";pooling=false";

        //    using (var conn = new SQLiteConnection(connectionString))
        //    using (var cmd = conn.CreateCommand())
        //    {
        //        var prm = cmd.CreateParameter();
        //        prm.ParameterName = "hostName";
        //        prm.Value = hostName;
        //        cmd.Parameters.Add(prm);

        //        cmd.CommandText = "SELECT name,encrypted_value FROM cookies WHERE host_key = @hostName";

        //        conn.Open();
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                var encryptedData = (byte[])reader[1];
        //                var decodedData = System.Security.Cryptography.ProtectedData.Unprotect(encryptedData, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
        //                var plainText = Encoding.ASCII.GetString(decodedData); // Looks like ASCII

        //                yield return Tuple.Create(reader.GetString(0), plainText);
        //            }
        //        }
        //        conn.Close();
        //    }
        //}
    }
}
