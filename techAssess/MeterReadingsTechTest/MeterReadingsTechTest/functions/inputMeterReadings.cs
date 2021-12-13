using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MeterReadingsTechTest.objects;

namespace MeterReadingsTechTest.functions
{
    public class inputMeterReadings
    {
        public static void InsertMeterData(List<PBOM_MeterReadings> inpMeterReadings)
        {
            Console.WriteLine();
            Console.WriteLine("Insert Service successfuly triggered.");

            var conn = new SqlConnection(@"Data Source=DESKTOP-4A93H7F;Initial Catalog=master;Integrated Security=True");

            for ( var c = 0; c < inpMeterReadings.Count; c++)
            {
                //Console.WriteLine();
                //Console.WriteLine("Looping through record " + c);
                if (inpMeterReadings[c].validRecord == true)
                {
                    //Console.WriteLine("Valid record, inserting Record " + c);
                    var sql = "INSERT INTO JM_EMR.METER_READINGS (ACCOUNT_ID, METER_READING_TMS, METER_READING_VALUE) VALUES(@accountId, @meterReading_tms, @meterReading_value)";

                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@accountId", inpMeterReadings[c].meterReadings.accountId);
                    cmd.Parameters.AddWithValue("@meterReading_tms", inpMeterReadings[c].meterReadings.meterReading_tms.ToString("yyyy-MMM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@meterReading_value", inpMeterReadings[c].meterReadings.meterReading_value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }

            Console.WriteLine();
            Console.WriteLine("Insert Service completed.");

        }
    }
}
