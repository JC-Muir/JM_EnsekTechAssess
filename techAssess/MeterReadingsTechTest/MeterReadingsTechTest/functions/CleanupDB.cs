using System.Data.SqlClient;

namespace MeterReadingsTechTest.functions
{
    class CleanupDB
    {
        public static void RemoveExistingRecords()
        {
            //To keep the database clean, this service removes pre-existing records which may have been inputted from previous runs of the technical assessment.

            var conn = new SqlConnection(@"Data Source=DESKTOP-4A93H7F;Initial Catalog=master;Integrated Security=True");
            var sql = "DELETE FROM JM_EMR.METER_READINGS";
            var cmd = new SqlCommand(sql, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
