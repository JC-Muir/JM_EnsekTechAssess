using System;
using System.Collections.Generic;
using MeterReadingsTechTest.objects;

namespace MeterReadingsTechTest.functions
{
    class UserInput
    {
        public static void InitUserInput(List<PBOM_MeterReadings> inpMeterReadings)
        {
            var meterDetails = inpMeterReadings;
            var validRecords = new List<PBOM_MeterReadings>() { };
            var failedRecords = new List<PBOM_MeterReadings>() { };

            for (var c = 0; c < meterDetails.Count; c++)
            {
                if (meterDetails[c].validRecord == true)
                {
                    validRecords.Add(meterDetails[c]);
                }
                else if (meterDetails[c].validRecord == false)
                {
                    failedRecords.Add(meterDetails[c]);
                }
                else
                {
                    Console.WriteLine("A record with counter number " + c + " cannot be identified as Valid or Failed");
                }
            }

            Console.WriteLine();
            Console.WriteLine("There is a total count of " + meterDetails.Count + " records.");
            Console.WriteLine();
            Console.WriteLine("Of the total count, there are currently " + validRecords.Count + " valid records which have been inputted to the database.");
            Console.WriteLine("Of the total count, there are currently " + failedRecords.Count + " records which failed validation.");
            Console.WriteLine();
            Console.WriteLine("To view the list of successful records, please type 'Valid'. To view the list of failed records, please type 'Failed'. Otherwise, please type 'Exit' to exit the process.");

            while (true)
            {
                Console.WriteLine();
                var userInput = Console.ReadLine();

                if (userInput.Trim().ToLower() == "exit")
                {
                    Console.WriteLine();
                    Console.WriteLine("Thank you for using this service.");
                    break;
                }
                else if (userInput.Trim().ToLower() == "valid")
                {
                    for (var c = 0; c < validRecords.Count; c++)
                    {
                        var accDetails = validRecords[c].accountDetails;
                        var meterReading = validRecords[c].meterReadings;

                        Console.WriteLine();
                        Console.WriteLine("Account ID:- " + accDetails.accountId + ", First Name:- " + accDetails.firstName + ", Last Name:- " + accDetails.lastName + ".");
                        Console.WriteLine("Meter Reading:- " + meterReading.meterReading_value + ", Meter Reading Date:- " + meterReading.meterReading_tms);
                    }
                }
                else if (userInput.Trim().ToLower() == "failed")
                {
                    for (var c = 0; c < failedRecords.Count; c++)
                    {
                        var failedRecord = failedRecords[c].failedMeterReadings;

                        Console.WriteLine();
                        Console.WriteLine("Associated Account ID:- " + failedRecord.accountId + ", Inputted Meter Reading:- " + failedRecord.meterReading_value + ", Inputted Meter Reading Date:- " + failedRecord.meterReading_tms);
                        Console.WriteLine("Reason for Failure:- " + failedRecord.reasonForFailure);
                    }
                } else
                {
                    Console.WriteLine("'"+userInput+"' is not a valid command. Please type one of the following:- ");
                    Console.WriteLine("Valid to display all valid records.");
                    Console.WriteLine("Failed to display all failed records and their reason for failure.");
                    Console.WriteLine("Exit to exit the application.");
                }
            }
        }
    }
}
