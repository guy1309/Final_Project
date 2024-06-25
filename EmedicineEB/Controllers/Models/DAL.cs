using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmedicineEB.Controllers.Models
{
    public class DAL
    {
        private DateTime expDate;

        public Response register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", 0);
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Status", 1);
            cmd.Parameters.AddWithValue("@ActionType", "Add");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User registered successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User registration failed";
            }
            return response;
        }
        public Response updateFund(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", users.Fund);
            cmd.Parameters.AddWithValue("@ActionType", "AddFund");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Funds updated successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Some error occured. Try after sometime.";
            }
            return response;
        }

        public Response login(Users users, SqlConnection connection)
        {
            using var cmd = new SqlCommand("SELECT * FROM Users WHERE Email=@Email AND Password=@Password");
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            connection.Open();
            using var rdr = cmd.ExecuteReader();
            Response response = new Response();
            if (rdr.Read())
            {
                Users user = new Users();
                user.ID = Convert.ToInt32(rdr["ID"]);
                user.FirstName = Convert.ToString(rdr["FirstName"]);
                user.LastName = Convert.ToString(rdr["LastName"]);
                user.Email = Convert.ToString(rdr["Email"]);
                user.Type = Convert.ToString(rdr["Type"]);
                response.StatusCode = 200;
                response.StatusMessage = "User is valid";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is invalid";
                response.user = null;
            }

            return response;
        }
        public Response viewUser(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_viewUser", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
                user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                response.StatusCode = 200;
                response.StatusMessage = "User exists.";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User does not exist.";

                response.user = null;

            }
            return response;
        }
        public Response updateProfile(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", users.ID);
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@ActionType", users.actionType);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Record updated successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Some error occured. Try after sometime";
            }

            return response;
        }
        public Response addToCart(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", cart.Email);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@ID", cart.ID);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item addedd successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be added";
            }
            return response;
        }
        public Response removeFromCart(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_RemoveToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", cart.Email);
            cmd.Parameters.AddWithValue("@ID", cart.ID);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item removed successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Item could not be removed";
            }
            return response;
        }
        public Response cartList(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            List<Cart> listCart = new List<Cart>();
            SqlDataAdapter da = new SqlDataAdapter("sp_CartList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", cart.Email);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cart obj = new Cart();
                    obj.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    obj.MedicineName = Convert.ToString(dt.Rows[i]["Name"]);
                    obj.Manufacturer = Convert.ToString(dt.Rows[i]["Manufacturer"]);
                    obj.UnitPrice = dt.Rows[i]["UnitPrice"] as decimal? ?? 0;
                    obj.Discount = dt.Rows[i]["Discount"] as decimal? ?? 0;
                    obj.Quantity = dt.Rows[i]["Quantity"] as int? ?? 0;
                    obj.TotalPrice = dt.Rows[i]["TotalPrice"] as decimal? ?? 0;
                    obj.ImageUrl = dt.Rows[i]["ImageUrl"] as string;
                    listCart.Add(obj);
                }
                if (listCart.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Cart details fetched";
                    response.listCart = listCart;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Cart details are not available";
                    response.listCart = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Cart details are not available";
                response.listCart = null;
            }
            return response;
        }

        public Response placeOrder(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_PlaceOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", users.Email);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order has been placed successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order could not be placed";
            }
            return response;
        }

        public Response OrderList(Users users, SqlConnection connection)
        {
            Response response = new Response();
            List<Orders> listOrder = new List<Orders>();
            SqlDataAdapter da = new SqlDataAdapter("sp_OrderList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    order.OrderTotal = dt.Rows[i]["OrderTotal"] as decimal? ?? 0;
                    order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                    order.CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]);
                    order.CustomerName = Convert.ToString(dt.Rows[i]["CustomerName"]);

                    if (users.Type == "UserItems")
                    {
                        order.MedicineName = Convert.ToString(dt.Rows[i]["MedicineName"]);
                        order.Manufacturer = Convert.ToString(dt.Rows[i]["Manufacturer"]);
                        order.UnitPrice = dt.Rows[i]["UnitPrice"] as decimal? ?? 0;
                        order.Quantity = dt.Rows[i]["Quantity"]as int? ?? 0;
                        order.TotalPrice = dt.Rows[i]["TotalPrice"] as decimal? ?? 0;
                        order.CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]);
                        order.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
                    }

                    listOrder.Add(order);
                }
                if (listOrder.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order details fetched";
                    response.listOrders = listOrder;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order details are not available";
                    response.listOrders = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order details are not available";
                response.listOrders = null;
            }
            return response;
        }

        public DateTime GetExpDate()
        {
            return expDate;
        }

        public Response addUpdateMedicine(Medicines medicines, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("INSERT INTO Medicines (Name, Manufacturer, UnitPrice, Discount, Quantity, ExpDate, ImageUrl, Status) VALUES (@Name, @Yatzran,@UnitPrice, @Discount, @Quantity, @ExpDate, @ImageUrl, @Status)", connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            List<Medicines> listMedicine = new List<Medicines>();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@ID", medicines.ID);
            cmd.Parameters.AddWithValue("@Name", medicines.Name);
            cmd.Parameters.AddWithValue("@Yatzran", medicines.Manufacturer);
            cmd.Parameters.AddWithValue("@UnitPrice", medicines.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", medicines.Discount);
            cmd.Parameters.AddWithValue("@Quantity", medicines.Quantity);
            cmd.Parameters.AddWithValue("@ExpDate", medicines.ExpDate);
            cmd.Parameters.AddWithValue("@ImageUrl", medicines.ImageUrl);
            cmd.Parameters.AddWithValue("@Status", medicines.Status);
            //cmd.Parameters.AddWithValue("@Type", medicines.Type);
            if (medicines.Type != "Get" && medicines.Type != "GetByID")
            {
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                if (i > 0)
                {
                    response.StatusCode = 200;
                    if (medicines.Type == "Add")
                        response.StatusMessage = "Medicine inserted successfully";
                    if (medicines.Type == "Update")
                        response.StatusMessage = "Medicine updated successfully";
                    if (medicines.Type == "Delete")
                        response.StatusMessage = "Medicine deleted successfully";
                }
                else
                {
                    response.StatusCode = 100;
                    if (medicines.Type == "Add")
                        response.StatusMessage = "Medicine did not save. try again.";
                    if (medicines.Type == "Update")
                        response.StatusMessage = "Medicine did not update. try again.";
                    if (medicines.Type == "Delete")
                        response.StatusMessage = "Medicine did not delete. try again.";
                }
            }
            else
            {
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Medicines medicine = new Medicines
                        {
                            ID = Convert.ToInt32(dt.Rows[i]["ID"]),
                            Name = Convert.ToString(dt.Rows[i]["Name"]),
                            Manufacturer = Convert.ToString(dt.Rows[i]["Manufacturer"]),
                            UnitPrice = Convert.ToInt32(dt.Rows[i]["UnitPrice"]),
                            Discount = Convert.ToInt32(dt.Rows[i]["Discount"]),
                            Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]),
                            expDate = Convert.ToInt32(dt.Rows[i]["ExpDate"]),
                            ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]),
                            Status = Convert.ToInt32(dt.Rows[i]["Status"])
                        };
                        listMedicine.Add(medicine);
                    }
                    if (listMedicine.Count > 0)
                    {
                        response.StatusCode = 200;
                        response.listMedicines = listMedicine;
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.listMedicines = null;
                    }
                }
            }

            return response;
        }
        public Response userList(SqlConnection connection)
        {
            Response response = new Response();
            List<Users> listUsers = new List<Users>();
            SqlDataAdapter da = new SqlDataAdapter("sp_UserList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Users user = new Users
                    {
                        ID = Convert.ToInt32(dt.Rows[i]["ID"]),
                        FirstName = Convert.ToString(dt.Rows[i]["FirstName"]),
                        LastName = Convert.ToString(dt.Rows[i]["LastName"]),
                        Password = Convert.ToString(dt.Rows[i]["Password"]),
                        Email = Convert.ToString(dt.Rows[i]["Email"]),
                        Fund = Convert.ToInt32(dt.Rows[i]["Fund"]),
                        Status = Convert.ToInt32(dt.Rows[i]["Status"]),
                        OrderDate = Convert.ToString(dt.Rows[i]["OrderDate"])
                    };
                    listUsers.Add(user);
                }
                if (listUsers.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "User details fetched";
                    response.listUsers = listUsers;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "User details are not available";
                    response.listUsers = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User details are not available";
                response.listOrders = null;
            }
            return response;
        }
        public Response updateOrderStatus(Orders order, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_updateOrderStatus", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNo", order.OrderNo);
            cmd.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order status has been updated successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Some error occured. Try after sometime.";
            }
            return response;
        }

        public Response getMedicines(SqlConnection connection)
        {
            Response response = new();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Medicines", connection);
                da.SelectCommand.CommandType = CommandType.Text;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.StatusMessage = ex.Message;
                return response;
            }

            List<Medicines> medicines = new();
            foreach (DataRow row in dt.Rows)
            {
                Medicines medicine = new Medicines
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = Convert.ToString(row["Name"]),
                    Manufacturer = Convert.ToString(row["Manufacturer"]),
                    UnitPrice = row["UnitPrice"] as decimal? ?? 0,
                    Discount = row["Discount"] as decimal? ?? 0,
                    Quantity = row["Quantity"] as int? ?? 0,
                    ExpDate = row["ExpDate"] as DateTime? ?? new DateTime(2024,1,1),
                    ImageUrl = row["ImageUrl"]as string,
                    Status = row["Status"] as int? ?? 0,
                };
                medicines.Add(medicine);
            }
            response.listMedicines = medicines;
            response.StatusCode = 200;
            return response;
        }
    }
}



