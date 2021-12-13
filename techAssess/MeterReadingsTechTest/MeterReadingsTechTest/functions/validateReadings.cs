using System;
using System.Collections.Generic;
using MeterReadingsTechTest.objects;

namespace MeterReadingsTechTest.functions
{
    public class validateReadings
    {
        public static List<PBOM_MeterReadings> meterReadingValidation(List<DBOM_MeterReadings> inpMeterReadings, List<IBOM_AccountDetails> inpAccountDetails)
        {
            var validatedReadings = new List<DBOM_MeterReadings>() { };
            var processData = new List<PBOM_MeterReadings>() { };

            Console.WriteLine();
            Console.WriteLine("Validation Service successfully triggered.");

            if (inpMeterReadings.Count > 0)
            {
                // Ensure that Data is valid?
                for (var c = 0; c < inpMeterReadings.Count; c++)
                {
                    var tmpRecord = new PBOM_MeterReadings();

                    // Valid Account ID?
                    bool validRecord = Int32.TryParse(inpMeterReadings[c].accountId, out _);
                    if (validRecord == false)
                    {
                        //Console.WriteLine();
                        //Console.WriteLine("Invalid Account ID identifed. Account ID is:- " + inpMeterReadings[c].accountId);
                        tmpRecord.failedMeterReadings = new DBOM_MeterReadings();
                        tmpRecord.failedMeterReadings.accountId = inpMeterReadings[c].accountId;
                        tmpRecord.failedMeterReadings.meterReading_tms = inpMeterReadings[c].meterReading_tms;
                        tmpRecord.failedMeterReadings.meterReading_value = inpMeterReadings[c].meterReading_value;
                        tmpRecord.failedMeterReadings.reasonForFailure = "Invalid Account ID Input";
                        tmpRecord.validRecord = false;
                        processData.Add(tmpRecord);
                    }

                    // Valid Timestamp?
                    validRecord = DateTime.TryParse(inpMeterReadings[c].meterReading_tms, out _ );
                    if (validRecord == false)
                    {
                        //Console.WriteLine();
                        //Console.WriteLine("Invalid Record Timestamp identifed. Timestamp is:- " + inpMeterReadings[c].meterReading_tms);
                        tmpRecord.failedMeterReadings = new DBOM_MeterReadings();
                        tmpRecord.failedMeterReadings.accountId = inpMeterReadings[c].accountId;
                        tmpRecord.failedMeterReadings.meterReading_tms = inpMeterReadings[c].meterReading_tms;
                        tmpRecord.failedMeterReadings.meterReading_value = inpMeterReadings[c].meterReading_value;
                        tmpRecord.validRecord = false;
                        tmpRecord.failedMeterReadings.reasonForFailure = "Invalid Timestamp for Meter Reading";
                        processData.Add(tmpRecord);
                    }

                    // Valid Meter Reading Value?
                    validRecord = Int32.TryParse(inpMeterReadings[c].meterReading_value, out _ );
                    if (validRecord == false)
                    {
                        //Console.WriteLine();
                        //Console.WriteLine("Invalid Meter Reading Value identifed. Value is:- " + inpMeterReadings[c].meterReading_value);
                        tmpRecord.failedMeterReadings = new DBOM_MeterReadings();
                        tmpRecord.failedMeterReadings.accountId = inpMeterReadings[c].accountId;
                        tmpRecord.failedMeterReadings.meterReading_tms = inpMeterReadings[c].meterReading_tms;
                        tmpRecord.failedMeterReadings.meterReading_value = inpMeterReadings[c].meterReading_value;
                        tmpRecord.validRecord = false;
                        tmpRecord.failedMeterReadings.reasonForFailure = "Invalid Meter Reading Value";
                        processData.Add(tmpRecord);
                    }

                    // Verified valid record:-
                    else
                    {
                        validatedReadings.Add(inpMeterReadings[c]);
                    }

                }

                inpMeterReadings = validatedReadings;
                validatedReadings = new List<DBOM_MeterReadings>() { };

                // Validate Account ID to ensure it matches Accounts within DB 
                for (var c = 0; c < inpMeterReadings.Count; c++)
                {
                    var validRecord = new Boolean();
                    var tmpRecord = new PBOM_MeterReadings();


                    for (var c2 = 0; c2 < inpAccountDetails.Count; c2++)
                    {
                        if (inpMeterReadings[c].accountId == inpAccountDetails[c2].accountId.ToString())
                        {
                            validRecord = true;
                            break;
                        }
                    }

                    if (validRecord == true)
                    {
                        validatedReadings.Add(inpMeterReadings[c]);
                    } 
                    else 
                    {
                        //Console.WriteLine();
                        //Console.WriteLine("No Existing Account found in Database. Account ID is :- " + inpMeterReadings[c].accountId);
                        tmpRecord.failedMeterReadings = new DBOM_MeterReadings();
                        tmpRecord.failedMeterReadings.accountId = inpMeterReadings[c].accountId;
                        tmpRecord.failedMeterReadings.meterReading_tms = inpMeterReadings[c].meterReading_tms;
                        tmpRecord.failedMeterReadings.meterReading_value = inpMeterReadings[c].meterReading_value;
                        tmpRecord.validRecord = false;
                        tmpRecord.failedMeterReadings.reasonForFailure = "No associated existing account found in the database for this record.";
                        processData.Add(tmpRecord);

                    }

                }

                inpMeterReadings = validatedReadings;
                validatedReadings = new List<DBOM_MeterReadings>() { };

                // Ensure no Duplicate Records:-
                for ( var c = 0; c < inpMeterReadings.Count; c++ )
                {
                    bool matchingRecord = new Boolean();
                    var tmpRecord = new PBOM_MeterReadings();


                    for (var c2 = 0; c2 < validatedReadings.Count; c2++)
                        {
                            if (inpMeterReadings[c].accountId == validatedReadings[c2].accountId)
                            {
                                matchingRecord = true;
                                break;
                            } 
                        }

                    if (matchingRecord == false)
                    {
                        validatedReadings.Add(inpMeterReadings[c]);
                    }
                    else
                    {
                        //Console.WriteLine();
                        //Console.WriteLine("Duplicate Record found for " + inpMeterReadings[c].accountId);
                        tmpRecord.failedMeterReadings = new DBOM_MeterReadings();
                        tmpRecord.failedMeterReadings.accountId = inpMeterReadings[c].accountId;
                        tmpRecord.failedMeterReadings.meterReading_tms = inpMeterReadings[c].meterReading_tms;
                        tmpRecord.failedMeterReadings.meterReading_value = inpMeterReadings[c].meterReading_value;
                        tmpRecord.validRecord = false;
                        tmpRecord.failedMeterReadings.reasonForFailure = "Duplicate Record identified.";
                        processData.Add(tmpRecord);
                    }

                }

                inpMeterReadings = validatedReadings;

                for ( var c = 0; c < inpMeterReadings.Count; c++ )
                {
                    var tmpRecord = new PBOM_MeterReadings();
                    tmpRecord.meterReadings = new IBOM_MeterReadings();
                    tmpRecord.meterReadings.accountId = Int32.Parse(inpMeterReadings[c].accountId);
                    tmpRecord.meterReadings.meterReading_tms = DateTime.Parse(inpMeterReadings[c].meterReading_tms);
                    tmpRecord.meterReadings.meterReading_value = Int32.Parse(inpMeterReadings[c].meterReading_value);

                    for ( var c2 = 0; c2 < inpAccountDetails.Count; c2++ )
                    {
                        if ( tmpRecord.meterReadings.accountId == inpAccountDetails[c2].accountId )
                        {
                            tmpRecord.accountDetails = new IBOM_AccountDetails();
                            tmpRecord.accountDetails.accountId = inpAccountDetails[c].accountId;
                            tmpRecord.accountDetails.firstName = inpAccountDetails[c].firstName;
                            tmpRecord.accountDetails.lastName = inpAccountDetails[c].lastName;
                            break;
                        }
                    }

                    tmpRecord.validRecord = true;
                    processData.Add(tmpRecord);
                }

                Console.WriteLine();
                Console.WriteLine("Validation Service completed.");

            }

            else { Console.WriteLine(); Console.WriteLine("No valid meter reading inputs."); }

            return processData;
        }
    }
}
