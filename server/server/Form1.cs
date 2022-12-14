using System.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using MESSAGE;
using connectSQL;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.Logging;

namespace server
{
    public partial class Form1 : Form
    {
        IPEndPoint iep ;
        Socket server ;
        //Dictionary<string, string> DS;
        Dictionary<string, List<string>> DSNhom;
        Dictionary<string, Socket> DSClient;
        List<USER> DS  ;
        bool active = false;
        private void KhoiTaoUser()
        {
            DS = new List<USER>();

            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            try
            {
                string sql = "Select * from Users";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                //DS = new List<USER>();

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            long user_id = Convert.ToInt64(reader.GetValue(0));
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string name = reader.GetString(3);
                            var user = new USER(username, password, name);

                            DS.Add(user);
                            

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                // Đóng kết nối.
                conn.Close();
                // Hủy đối tượng, giải phóng tài nguyên.
                conn.Dispose();
            }






            //DSNhom = new Dictionary<string, List<string>>();
            //char u;
            //for(u='A';u<'Z';u++)
            //    DS.Add(u.ToString(), "123");
            //for(byte i = 0; i < 5; i++)
            //{
            //    List<string> nhom = new List<string>();
            //    for(byte j = 0; j < 3; j++)
            //    {
            //        u = (Char)('A'+3 * i + j);
            //        nhom.Add(u.ToString());
            //    }
            //    DSNhom.Add("N"+i.ToString(), nhom);
            //}
            DSClient = new Dictionary<string, Socket>();
        }
        private Boolean register(string username, string password, string name)
        {
            SqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                // Câu lệnh Insert.
                string sql = "Insert into Users (username, password, name) "
                                                 + " values (@username, @password, @name) ";

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;

                // Thực thi Command (Dùng cho delete, insert, update).
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount > 0)
                {
                    return true;
                }
                else return false;
                //Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
            return false;
            //Console.Read();

        }

        private Boolean send_message(string user_send, string user_receiver, string message)
        {
            SqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                // Câu lệnh Insert.
                string sql = "Insert into Messages (user_send, user_receiver, Date_send, message) "
                                                 + " values (@user_send, @user_receiver, @Date_send, @message) ";

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                cmd.Parameters.Add("@user_send", SqlDbType.VarChar).Value = user_send;

                cmd.Parameters.Add("@user_receiver", SqlDbType.VarChar).Value = user_receiver;

                cmd.Parameters.Add("@Date_send", SqlDbType.DateTime).Value = DateTime.Now;

                cmd.Parameters.Add("@message", SqlDbType.NVarChar).Value = message;

                // Thực thi Command (Dùng cho delete, insert, update).
                int rowCount = cmd.ExecuteNonQuery();
                if (rowCount > 0)
                {
                    return true;
                }
                else return false;
                //Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
            return false;
            //Console.Read();

        }

        private Boolean check_login(string username, string password)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            try
            {
                string sql = "Select * from Users where username = '"+username+"' and password= '" + password +"'";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                //DS = new List<USER>();

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                // Đóng kết nối.
                conn.Close();
                // Hủy đối tượng, giải phóng tài nguyên.
                conn.Dispose();
            }
            return false;

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            //Console.WriteLine(hostName);
            // Get the IP
            foreach(IPAddress ip in Dns.GetHostByName(hostName).AddressList)
            {
                if(ip.ToString().Contains("."))
                {
                    IP.Text = ip.ToString();
                    break;
                }
            }
            KhoiTaoUser();
        }
        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            KQ.Text += value;
        }
        private  void sendJson(Socket client,object obj)
        {
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(obj);
            client.Send(jsonUtf8Bytes, jsonUtf8Bytes.Length, SocketFlags.None);
        }
        private void ThreadClient(Socket client)
        {            
            byte[] data = new byte[1024];
            int recv = client.Receive(data);
            if (recv == 0) return;
            string jsonString = Encoding.ASCII.GetString(data, 0, recv);
            
            MESSAGE.COMMON? com = JsonSerializer.Deserialize<MESSAGE.COMMON>(jsonString);
            if (com!=null )
            {
                if(com.content!=null)
                {
                    switch (com.kind)
                    {
                        case 1:
                            {
                                LOGIN? login = JsonSerializer.Deserialize<LOGIN>(com.content);
                                if (login != null && login.username != null && check_login(login.username,login.pass)==true)
                                {
                                    com = new COMMON(3, "OK");
                                    sendJson(client, com);
                                    DSClient.Remove(login.username);
                                    DSClient.Add(login.username, client);
                                }
                                else
                                {
                                    com = new COMMON(3, "CANCEL");
                                    sendJson(client, com);
                                    return;
                                }
                                break;
                            }
                        case 5:
                            {
                                USER? resgister = JsonSerializer.Deserialize<USER>(com.content);
                                int dem = 0;
                                foreach (USER user in DS)
                                {
                                    if (user.username == resgister.username) dem++;
                                }

                                if(dem == 0)
                                {
                                    if (resgister != null && resgister.username != null )
                                    {
                                        

                                        if (register(resgister.username, resgister.password, resgister.name) == true)
                                        {
                                            com = new COMMON(3, "OK");
                                            sendJson(client, com);
                                        }
                                        else
                                        {
                                            com = new COMMON(3, "CANCEL");
                                            sendJson(client, com);
                                        }    

                                        
                                    }
                                    else
                                    {
                                        com = new COMMON(3, "CANCEL");
                                        sendJson(client, com);
                                        return;
                                    }
                                } 
                                else
                                {
                                    com = new COMMON(3, "CANCEL");
                                    sendJson(client, com);
                                    return;
                                }


                            }
                            
                            break;
                    }
                    
                }
                else
                {
                    com = new COMMON(3, "CANCEL");
                    sendJson(client, com);                    
                    return;
                }                
            }
            try
            {
                bool tieptuc = true;
                while (tieptuc)
                {
                    data = new byte[1024];
                    recv = client.Receive(data);
                    if (recv == 0) continue;
                    string s = Encoding.ASCII.GetString(data, 0, recv);
                    if (s.ToUpper().Equals("QUIT")) break;
                    com = JsonSerializer.Deserialize<MESSAGE.COMMON>(s);

                    if (com != null && com.content != null)
                    {
                        switch (com.kind)
                        {
                            case 2:
                                MESSAGE.MESSAGE? mes = JsonSerializer.Deserialize<MESSAGE.MESSAGE>(com.content);
                                if (mes != null && mes.usernameReceiver != null)
                                {
                                    if (DSClient.Keys.Contains(mes.usernameReceiver))
                                    {
                                        
                                        if(send_message(mes.usernameSender,mes.usernameReceiver,mes.content)==true)
                                        {
                                            AppendTextBox(mes.usernameSender + " send to " + mes.usernameReceiver + " content: " + mes.content + Environment.NewLine);
                                            Socket friend = DSClient[mes.usernameReceiver];
                                            friend.Send(data, recv, SocketFlags.None);
                                        }    
                                    }
                                    else//Nhom
                                    {
                                        if (DSNhom.Keys.Contains(mes.usernameReceiver)) 
                                        {
                                            if (DSNhom[mes.usernameReceiver].Contains(mes.usernameSender))
                                            {
                                                AppendTextBox(mes.usernameSender + " send to " + mes.usernameReceiver + " content: " + mes.content + Environment.NewLine);
                                                foreach (String user in DSNhom[mes.usernameReceiver])
                                                {
                                                    if (DSClient.Keys.Contains(user))
                                                    {

                                                        Socket friend = DSClient[user];
                                                        friend.Send(data, recv, SocketFlags.None);

                                                        
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                com = new COMMON(10, "CANCEL");
                                                sendJson(client, com);
                                            }
                                        }
                                        else
                                        {
                                            com = new COMMON(11, "CANCEL");
                                            sendJson(client, com);
                                        }

                                    }
                                }
                                break;
                            case 4:
                                DSClient[com.content].Close();
                                DSClient.Remove(com.content);
                                tieptuc = false;
                                break;
                            case 6:
                                {

                                    if (!DSNhom.Keys.Contains(com.content))
                                    {
                                        DSNhom.Add(com.content, new List<string>());
                                        com = new COMMON(8, "OK");
                                        sendJson(client, com);
                                    }
                                    else
                                    {
                                        com = new COMMON(8, "CANCEL");
                                        sendJson(client, com);
                                        //return;
                                        
                                    }
                                }
                                break;
                            case 7:
                                {
                                    //MESSAGE.ADDNHOM? obj = JsonSerializer.Deserialize<MESSAGE.ADDNHOM>(com.content);
                                    //if (!DSNhom.Keys.Contains(obj.GrpName))
                                    //{
                                    //    com = new COMMON(9, "CANCEL");
                                    //    sendJson(client, com);
                                    //    return;
                                        
                                    //}                                    
                                    //else 
                                    //{
                                    //    foreach(var user in obj.members)
                                    //        if (!DS.Keys.Contains(user))
                                    //        {
                                    //            com = new COMMON(9, "CANCEL");
                                    //            sendJson(client, com);
                                    //            return;
                                    //        }
                                    //    foreach (var user in obj.members)
                                    //    {
                                    //        if (!DSNhom[obj.GrpName].Contains(user))                                            
                                    //                DSNhom[obj.GrpName].Add(user);
                                    //    }
                                        
                                    //    //DSNhom.Add(com.content, new List<string>());
                                    //    com = new COMMON(9, "OK");
                                    //    sendJson(client, com);
                                    //}
                                }
                                break;
                            

                        }
                    }
                }
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception)
            {
                //server.Close();
            }

        }
        private void ThreadTask()
        {
            while (active)
            {
                try
                {
                    Socket client = server.Accept();                   
                    var t = new Thread(() => ThreadClient(client));
                    t.Start();
                }
                catch (Exception)
                {
                    active = false;
                }
                               
            }
            
            
            
            
        }
        private void Start_Click(object sender, EventArgs e)
        {
            active = true;
            iep = new IPEndPoint(IPAddress.Parse(IP.Text), int.Parse(PORT.Text));
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
            server.Bind(iep);
            server.Listen(10);
            //Console.WriteLine("Cho  ket  noi  tu  client");
            KQ.Text += "Cho  ket  noi  tu  client" + Environment.NewLine;
            
            
            Thread trd = new Thread(new ThreadStart(this.ThreadTask));
            trd.IsBackground = true;
            trd.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            active = false;
        }

        
        
            private void button1_Click(object sender, EventArgs e)
        {
            // Lấy ra đối tượng Connection kết nối vào DB.
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            try
            {
                //QueryEmployee(conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                // Đóng kết nối.
                conn.Close();
                // Hủy đối tượng, giải phóng tài nguyên.
                conn.Dispose();
            }
            Console.Read();
        }
    }


    
}