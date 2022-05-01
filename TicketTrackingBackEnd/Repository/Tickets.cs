using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicketTrackingAP.Model;

namespace TicketTrackingAP.Repository
{
    public class Tickets
    {
        const string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TicketTrackingSystem;";

        public static bool Add(Model.Ticket ticket)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = $@"Insert Into [dbo].[Ticket] (
                                                [TicketID]    ,
                                                [Status]      ,
                                                [TicketType]  ,
                                                [Applicant]   ,
                                                [Description] ,
                                                [Summary]     ,
                                                [Solver]      ,
                                                [CreateTime]  ,
                                                [UpdateTime]  ,
                                                [Severity]    ,
                                                [Priority]    ,
                                                [RDRemark]
                                            )
                                            Values(
                                                @TicketID,
                                                'Open',
                                                @TicketType,
                                                @Applicant,
                                                @Description,
                                                @Summary,
                                                @Solver,
                                                @CreateTime,
                                                @UpdateTime,
                                                @Severity,
                                                @Priority,
                                                ''
                                            )";
                        cmd.Parameters.AddWithValue("@TicketID", ticket.TicketId);
                        
                        cmd.Parameters.AddWithValue("@Applicant", ticket.Applicant);
                        cmd.Parameters.AddWithValue("@Description", ticket.Description);
                        cmd.Parameters.AddWithValue("@Summary", ticket.Summary);
                        cmd.Parameters.AddWithValue("@CreateTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UpdateTime", DateTime.Now);
                        
                        cmd.Parameters.AddWithValue("@TicketType", string.IsNullOrEmpty(ticket.TicketType) ? DBNull.Value : (object)ticket.TicketType);
                        cmd.Parameters.AddWithValue("@Solver", string.IsNullOrEmpty(ticket.Solver) ? DBNull.Value : (object)ticket.Solver);
                        cmd.Parameters.AddWithValue("@Severity", string.IsNullOrEmpty(ticket.Severity) ? DBNull.Value : (object)ticket.Severity);
                        cmd.Parameters.AddWithValue("@Priority", string.IsNullOrEmpty(ticket.Priority) ? DBNull.Value : (object)ticket.Priority);

                        var count = cmd.ExecuteNonQuery();
                        
                        conn.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
        
        public static string GetNewID()
        {
            /*此處應查詢資料庫取得新的流水號，因時程限制問題暫時用現在時間代替*/
            return DateTime.Now.ToString("yyyyMMddHHmm");
        }

        public static Model.Ticket Get(string TicketID)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = $@"Select * From [dbo].[Ticket] Where [TicketID] = '{TicketID.Trim()}'";

                        var Dr = cmd.ExecuteReader();

                        while (Dr.Read())
                        {
                            var ticket = new Model.Ticket() {
                                TicketId = Dr[0].ToString(),
                                Status = Dr[1].ToString(),
                                TicketType = Dr[2].ToString(),
                                Applicant = Dr[3].ToString(),
                                Description = Dr[4].ToString(),
                                Summary = Dr[5].ToString(),
                                Solver = Dr[6].ToString(),
                                CreateTime = Convert.ToDateTime(Dr[7].ToString()),
                                UpdateTime = Convert.ToDateTime(Dr[8].ToString()),
                                Severity = Dr[9].ToString(),
                                Priority = Dr[10].ToString(),
                                RDRemark = Dr[11].ToString()
                            };

                            return ticket;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }

            return null;
        }

        public static List<Model.Ticket> GetAll()
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = $@"Select * From [dbo].[Ticket]";

                        var Dr = cmd.ExecuteReader();

                        List<Model.Ticket> tickets = new List<Model.Ticket>();
                        while (Dr.Read())
                        {
                            var test = Dr[1].ToString();

                            tickets.Add(new Model.Ticket()
                            {
                                TicketId = Dr[0].ToString(),
                                Status = Dr[1].ToString(),
                                TicketType = Dr[2].ToString(),
                                Applicant = Dr[3].ToString(),
                                Description = Dr[4].ToString(),
                                Summary = Dr[5].ToString(),
                                Solver = Dr[6].ToString(),
                                CreateTime = Convert.ToDateTime(Dr[7].ToString()),
                                UpdateTime = Convert.ToDateTime(Dr[8].ToString()),
                                Severity = Dr[9].ToString(),
                                Priority = Dr[10].ToString(),
                                RDRemark = Dr[11].ToString()
                            });

                        }

                        return tickets;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }

            return null;
        }

        public static bool Update(Model.Ticket ticket)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = $@"Update dbo.Ticket
                                            Set TicketType = @TicketType,
                                                Status = @Status,
	                                            Description = @Description,
	                                            Summary = @Summary,
	                                            Solver = @Solver,
	                                            UpdateTime = @UpdateTime,
	                                            Severity = @Severity,
	                                            Priority = @Priority,
                                                RDRemark = @RDRemark
                                            Where TicketID = @TicketID;
                                            ";
                        cmd.Parameters.AddWithValue("@TicketID", ticket.TicketId);

                        cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(ticket.Status) ? DBNull.Value : (object)ticket.Status);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(ticket.Description) ? DBNull.Value : (object)ticket.Description);
                        cmd.Parameters.AddWithValue("@Summary", string.IsNullOrEmpty(ticket.Summary) ? DBNull.Value : (object)ticket.Summary);

                        cmd.Parameters.AddWithValue("@UpdateTime", DateTime.Now);

                        cmd.Parameters.AddWithValue("@TicketType", string.IsNullOrEmpty(ticket.TicketType) ? DBNull.Value : (object)ticket.TicketType);
                        cmd.Parameters.AddWithValue("@Solver", string.IsNullOrEmpty(ticket.Solver) ? DBNull.Value : (object)ticket.Solver);
                        cmd.Parameters.AddWithValue("@Severity", string.IsNullOrEmpty(ticket.Severity) ? DBNull.Value : (object)ticket.Severity);
                        cmd.Parameters.AddWithValue("@Priority", string.IsNullOrEmpty(ticket.Priority) ? DBNull.Value : (object)ticket.Priority);
                        cmd.Parameters.AddWithValue("@RDRemark", string.IsNullOrEmpty(ticket.RDRemark) ? DBNull.Value : (object)ticket.RDRemark);

                        var count = cmd.ExecuteNonQuery();

                        conn.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }

            return true;
        }

        public static bool Delete(string ticketID)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = $@"Delete From dbo.Ticket
                                            Where TicketID = @TicketID;
                                            ";
                        cmd.Parameters.AddWithValue("@TicketID", ticketID);
                        
                        var count = cmd.ExecuteNonQuery();

                        conn.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
    }

    
}
