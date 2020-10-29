
using GoodiModels_1272.DataModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace GoodiModels_1272.DataBase
{
    public class DBParser
    {
        /// <summary>
        /// The singleton instance
        /// </summary>
        private static DBParser m_ins;
        /// <summary>
        /// select command
        /// </summary>
        private SelectCmd m_selsctCmd;

        /// <summary>
        /// update command
        /// </summary>
        private UpdateCmd m_updateCmd;


        private readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DBParser));

        readonly string m_connectionString = ConfigurationManager.ConnectionStrings["testos"].ConnectionString;

        /// <summary>
        /// constructor
        /// </summary>
        private DBParser()
        {
            m_selsctCmd = new SelectCmd();
            m_updateCmd = new UpdateCmd();
        }

        /// <summary>
        /// Get the singleton instance
        /// </summary>
        /// <returns>The singleton instance</returns>
        public static DBParser Instance()
        {
            if (m_ins == null)
            {
                m_ins = new DBParser();
            }
            return m_ins;
        }

        /// <summary>
        /// read string that must not be empty - if it is emty - set the valid data flag to be false
        /// </summary>
        /// <param name="curReader">the current reader</param>
        /// <param name="clmName">the coulumn name</param>
        /// <returns>the string</returns>
        private string ReadString(OracleDataReader curReader, string clmName)
        {
            string str = ReadNotValidString(curReader, clmName);
            return str;
        }
        /// <summary>
        /// read string that can be empty
        /// </summary>
        /// <param name="curReader">the current reader</param>
        /// <param name="clmName">the coulumn name</param>
        /// <returns>the string</returns>
        private string ReadNotValidString(OracleDataReader curReader, string clmName)
        {
            string val = null;
            try
            {
                if (curReader[clmName] != null)
                {
                    val = curReader[clmName].ToString();
                }
            }
            catch
            {
                string errMsg = "Error: Invalid colum name: " + clmName;
                throw new Exception(errMsg);

            }
            return val;
        }
        /// <summary>
        /// read int that must not be empty - if it is emty - set the valid data flag to be false
        /// </summary>
        /// <param name="curReader">the current reader</param>
        /// <param name="clmName">the coulumn name</param>
        /// <returns>the int</returns>
        private int ReadInt(OracleDataReader curReader, string clmName)
        {
            int val = 0;
            string str = ReadString(curReader, clmName);
            val = Convert.ToInt32(str);
            return val;
        }

        /// <summary>
        /// read double that must not be empty - if it is emty - set the valid data flag to be false
        /// </summary>
        /// <param name="curReader">the current reader</param>
        /// <param name="clmName">the coulumn name</param>
        /// <returns>the int</returns>
        private double ReadDouble(OracleDataReader curReader, string clmName)
        {
            double val = 0;
            string str = ReadString(curReader, clmName);
            val = Convert.ToDouble(str);
            return val;
        }

        /// <summary>
        /// read date time that must not be empty - if it is emty - set the valid data flag to be false
        /// </summary>
        /// <param name="curReader">the current reader</param>
        /// <param name="clmName">the coulumn name</param>
        /// <returns>the int</returns>
        private DateTime ReadDateTime(OracleDataReader curReader, string clmName)
        {
            DateTime val = DateTime.Now;
            string str = ReadString(curReader, clmName);
            val = Convert.ToDateTime(str);
            return val;
        }

        /// <summary>
        /// read time  that must not be empty - if it is emty - set the valid data flag to be false
        /// </summary>
        /// <param name="curReader">the current reader</param>
        /// <param name="clmName">the coulumn name</param>
        /// <returns>date in string</returns>
        public string ReadTime(OracleDataReader curReader, string clmName)
        {
            DateTime val = DateTime.Now;
            string str = ReadString(curReader, clmName);
            val = Convert.ToDateTime(str);
            return val.ToString("HH-mm-ss");
        }



        /// <summary>
        /// read date  that must not be empty - if it is emty - set the valid data flag to be false
        /// </summary>
        /// <param name="curReader">the current reader</param>
        /// <param name="clmName">the coulumn name</param>
        /// <returns>date in string</returns>
        public string ReadDate(OracleDataReader curReader, string clmName)
        {
            DateTime val = DateTime.Now;
            string str = ReadString(curReader, clmName);
            val = Convert.ToDateTime(str);
            return val.ToString("yyyy-MM-dd");
        }

        public Token GetToken()
        {
            using (OracleConnection connection = new OracleConnection(m_connectionString))
            {
                try
                {
                    connection.Open();
                    m_selsctCmd.TblName = "TBL_GOODI_TOKEN_1272";
                    using (OracleCommand command = new OracleCommand(m_selsctCmd.GetCmd(), connection))
                    {
                        command.CommandTimeout = 5000;
                        OracleDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            Token token = new Token
                            {
                                TokenNumber = ReadString(reader, "TOKEN"),
                                Stemp_Tar = ReadDateTime(reader, "STAMP_TAR")
                            };
                            return token;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //insert log error message
                    log.Error($"Exception when try to get token, error message: {ex.Message}");


                }
                finally
                {
                    connection.Close();

                }
            }
            return null;
        }

        [Obsolete("Message")]
        public IEnumerable<ExecuteTransaction> GetAllTransactions()
        {
            ExecuteTransaction transaction = null;
            List<ExecuteTransaction> transactions = new List<ExecuteTransaction>();
            using (OracleConnection connection = new OracleConnection(m_connectionString))
            {
                try
                {
                    connection.Open();
                    m_selsctCmd.TblName = "V_TBL_GOODIES_ZAHAL_1272";
                    using (OracleCommand command = new OracleCommand(m_selsctCmd.GetCmd(), connection))
                    {
                        command.CommandTimeout = 5000;
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            transaction = new ExecuteTransaction
                            {
                                Id = ReadInt(reader, "ROW_ID"),
                                Price = ReadDouble(reader, "PRICE"),
                                Amount = ReadDouble(reader, "AMOUNT"),
                                Kod_makor = ReadInt(reader, "KOD_MAKOR"),
                                Kod_station = ReadInt(reader, "KOD_STATION"),
                                Kod_hetken = ReadInt(reader, "KOD_HETKEN"),
                                Mispar_hetken = ReadString(reader, "MISPAR_HETKEN"),
                                Tidluk_date = ReadString(reader, "TIDLUK_DATE"),
                                Tidluk_time = ReadString(reader, "TIDLUK_TIME"),
                                Kod_tazkik = ReadInt(reader, "KOD_TAZKIK"),
                                Station_order = ReadString(reader, "STATION_ORDER"),


                            };
                            transactions.Add(transaction);
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    //insert log error message
                    log.Error($"Exception when try to get all transactions, error message: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return transactions;
        }

        public bool UpdateToken(Token token)
        {
            using (OracleConnection connection = new OracleConnection(m_connectionString))
            {
                try
                {
                    connection.Open();
                    m_updateCmd.TblName = "TBL_GOODI_TOKEN_1272";
                    m_updateCmd.AddTextVal("TOKEN", token.TokenNumber);
                    m_updateCmd.AddTextVal("STAMP_TAR", token.Stemp_Tar.ToString());

                    using (OracleCommand command = new OracleCommand(m_updateCmd.GetCmd(), connection))
                    {
                        command.CommandTimeout = 5000;
                        int result = command.ExecuteNonQuery();
                        if (result > 0) return true;

                    }
                }
                catch (Exception ex)
                {
                    //insert log error message
                    log.Error($"Exception when try to update Token number, error message: {ex.Message}");
                    return false;
                }
                finally
                {
                    connection.Close();

                }
                return false;
            }
        }

        [Obsolete("Message")]
        public bool UpdateTransaction(int id, string errorMessage = null, string errorCode = null, ExecuteTransactionResponse transactionResponse = null)
        {
            using (OracleConnection connection = new OracleConnection(m_connectionString))
            {
                try
                {
                    connection.Open();
                    m_updateCmd.TblName = "TBL_GOODIES_ZAHAL_1272";
                    m_updateCmd.AddIntKeyVal("ID", id);
                    if (errorCode == null && errorMessage == null)
                    {
                        m_updateCmd.AddIntVal("STATUS", 1);
                        m_updateCmd.AddTextVal("BALANCE", transactionResponse.Balance);
                        m_updateCmd.AddTextVal("ORDERID", transactionResponse.orderId);
                    }
                    else
                    {
                        m_updateCmd.AddIntVal("STATUS", 2);
                        m_updateCmd.AddTextVal("ERROR_MESSAGE", errorMessage);
                        m_updateCmd.AddTextVal("ERROR_CODE", errorCode);

                    }
                    using (OracleCommand command = new OracleCommand(m_updateCmd.GetCmd(), connection))
                    {
                        command.CommandTimeout = 5000;
                        int result = command.ExecuteNonQuery();
                        if (result > 0) return true;
                    }
                }
                catch (Exception ex)
                {
                    //log.Error($"Exception when try to update seler status, error message: {ex.Message}");
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return false;
        }


    }
}