using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Android.OS;
using Android.Provider;

namespace MobileSoLienLac.Models.SQL
{
    public class Helper : HandleError
    {
        // connect string to db main
        public string connStr = @"SERVER=125.212.218.20;Database = nxtckedu_HeThongSoLienLac; uid= nxtckedu_sa; pwd= H*P*T-1999";
        //Connect String to DB backup
        //public string connStr = @"SERVER=125.212.218.20;Database = nxtckedu_Backup; uid= nxtckedu_Backup; pwd= H*P*T-1999";
        public async Task<int> ExecuteNonQuery(string ProcName, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(ProcName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        await con.OpenAsync();


                        return await cmd.ExecuteNonQueryAsync();
                    }
                }
                //Message = "The transaction ended in the trigger. The batch has been aborted."
                catch (Exception ex)
                {
                    return StringErrorToIDError(ex);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }

            return 0;
        }

        public async Task<DataTableSQL> ExecuteQuery(string ProcName, params SqlParameter[] parameters)
        {
            DataTableSQL val = new DataTableSQL();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(ProcName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        await con.OpenAsync();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(val.Data);
                        }

                    }
                }
                catch (Exception ex)
                {
                    val.Error = StringErrorToIDError(ex);
                    val.Data = null;
                    return val;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }

            return val;
        }

        #region ExecuteScalar

        public async Task<ObjectSQL<T>> ExecuteScalar<T>(string ProcName, params SqlParameter[] parameters)
        {
            ObjectSQL<T> val = new ObjectSQL<T>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(ProcName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        await con.OpenAsync();
                        var Val = await cmd.ExecuteScalarAsync();

                        val.Value = (Val == null) ? default(T) : (T)Convert.ChangeType(Val, typeof(T));
                    }
                }
                catch (Exception ex)
                {
                    val.Error = StringErrorToIDError(ex);
                    return val;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                return val;
            }
        }

        public async Task<int> ExecuteScalarCount(string ProcName, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(ProcName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        await con.OpenAsync();
                        var Val = await cmd.ExecuteScalarAsync();

                        return (Val == null) ? -1 :Convert.ToInt32(Val);

                    }
                }
                catch (Exception ex)
                {
                    return StringErrorToIDError(ex);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                return -1;
            }
        }

        #endregion

        public string SlipString(string str)
        {
            string strReturn = str;

            strReturn = strReturn.Remove(0, str.IndexOf("\r\n"));

            return strReturn;
        }
    }
}

