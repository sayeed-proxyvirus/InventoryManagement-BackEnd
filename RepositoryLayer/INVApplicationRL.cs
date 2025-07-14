using InventoryManagement.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryManagement.RepositoryLayer
{
    public class INVApplicationRL : IINVApplicationRL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _sqlConnection;
        int ConnectionTimeOut = 1800;
        public INVApplicationRL(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION"));
            //_mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlDBConnection"]);
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            LoginResponse response = new LoginResponse { IsSuccess = false, Message = "Invalid credentials." };

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand("usp_login", _sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);

                    await _sqlConnection.OpenAsync();

                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (await _sqlDataReader.ReadAsync())
                        {
                            string storedHashedPassword = _sqlDataReader["Upassword"].ToString();

                            if (BCrypt.Net.BCrypt.Verify(request.Password, storedHashedPassword))
                            {
                                response.IsSuccess = true;
                                response.Message = "Login successful!";
                            }
                        }
                        else
                        {
                            response.Message = "User not found.";
                        }
                    }

                    await _sqlConnection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception: " + ex.Message;
            }

            return response;
        }
        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            RegisterResponse response = new RegisterResponse { IsSuccess = false, Message = "Registration failed." };

            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                using (SqlCommand sqlCommand = new SqlCommand("usp_register", _sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                    sqlCommand.Parameters.AddWithValue("@FullName", request.FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);


                    await _sqlConnection.OpenAsync();
                    await sqlCommand.ExecuteNonQueryAsync();
                    await _sqlConnection.CloseAsync();

                    response.IsSuccess = true;
                    response.Message = "Registration successful!";
                }
            }
            catch (SqlException ex) when (ex.Number == 50000) // RAISERROR number
            {
                response.Message = ex.Message; // e.g., "User already exists."
            }
            catch (Exception ex)
            {
                response.Message = "Exception: " + ex.Message;
            }

            return response;
        }



        public async Task<CreateInformationResponse> CreateInformation(IItemCreateInformationRequest request) 
        {
            CreateInformationResponse resposne = new CreateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                //if (_mySqlConnection != null)
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_additem";
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.CreateInformationQuery, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@ItemCode", request.ItemCode);
                        sqlCommand.Parameters.AddWithValue("@ItemName", request.ItemName);
                        sqlCommand.Parameters.AddWithValue("@CatID", request.CatID);
                        sqlCommand.Parameters.AddWithValue("@SubCatID", request.SubCatID);
                        sqlCommand.Parameters.AddWithValue("@Unit", request.Unit);
                        sqlCommand.Parameters.AddWithValue("@OpeningStock", request.OpeningStock);
                        sqlCommand.Parameters.AddWithValue("@CurrentStock", request.CurrentStock);
                        sqlCommand.Parameters.AddWithValue("@ReorderStock", request.ReorderStock);
                        sqlCommand.Parameters.AddWithValue("@MaxStock", request.MaxStock);
                        sqlCommand.Parameters.AddWithValue("@LastPurRate", request.LastPurRate);
                        sqlCommand.Parameters.AddWithValue("@LastPurDate", request.LastPurDate);
                        sqlCommand.Parameters.AddWithValue("@PrefAlt", request.PrefAlt);
                        sqlCommand.Parameters.AddWithValue("@Amount", request.Amount);
                        sqlCommand.Parameters.AddWithValue("@HScode", request.HScode);
                        sqlCommand.Parameters.AddWithValue("@IsActive", request.IsActive);
                        sqlCommand.Parameters.AddWithValue("@CreatedAT", request.CreatedAT);
                        sqlCommand.Parameters.AddWithValue("@ExpiryDate", request.ExpiryDate);
                        


                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "CreateInformation Not Executed";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }
        public async Task<CreateInformationResponse> CreateInformation(SupplierInfoCreateInformationRequest request) 
        {
            CreateInformationResponse resposne = new CreateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                //if (_mySqlConnection != null)
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_addSupplier";
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.CreateInformationQuery, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@SupplierID", request.SupplierID);
                        sqlCommand.Parameters.AddWithValue("@SupplierName", request.SupplierName);
                        sqlCommand.Parameters.AddWithValue("@ContactNumber", request.ContactNumber);
                        sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                        sqlCommand.Parameters.AddWithValue("@Address", request.Address);
                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "CreateInformation Not Executed";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }
        public async Task<CreateInformationResponse> CreateInformation(ItemCatCreateInfoCreateInformationRequest request) 
        {
            CreateInformationResponse resposne = new CreateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                //if (_mySqlConnection != null)
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_addItemCat";
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.CreateInformationQuery, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@CategoryID", request.CategoryID);
                        sqlCommand.Parameters.AddWithValue("@CategoryName", request.CategoryName);

                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "CreateInformation Not Executed";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }
        public async Task<CreateInformationResponse> CreateInformation(SubCatCreateInfoCreateInformationRequest request) 
        {
            CreateInformationResponse resposne = new CreateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                //if (_mySqlConnection != null)
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_addItemSubCat";
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.CreateInformationQuery, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@SubCatID", request.SubCatID);
                        sqlCommand.Parameters.AddWithValue("@CategoryName", request.CategoryName);
                        sqlCommand.Parameters.AddWithValue("@SubCatName", request.SubCatName);

                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "CreateInformation Not Executed";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }



        public async Task<ReadInformationResponse> ItemReadInformation()
        {
            ReadInformationResponse response = new ReadInformationResponse();
            response.itemreadinformation = new List<ItemReadInformation>();
            response.IsSuccess = true;
            response.Message = "Successful";
            //string nu = "null";
            try
            {
                string StoreProcedure = "usp_ViewItems";
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadInformation, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                ItemReadInformation getResponse = new ItemReadInformation();
                                getResponse.ItemCode = _sqlDataReader["ItemCode"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["ItemCode"]) : 0;
                                getResponse.ItemName = _sqlDataReader["ItemName"] != DBNull.Value ? _sqlDataReader["ItemName"].ToString() : string.Empty;
                                getResponse.HScode = _sqlDataReader["HScode"] != DBNull.Value ? _sqlDataReader["HScode"].ToString() : string.Empty;
                                getResponse.CatName = _sqlDataReader["CatName"] != DBNull.Value ? _sqlDataReader["CatName"].ToString() : string.Empty;
                                getResponse.SubCatName = _sqlDataReader["SubCatName"] != DBNull.Value ? _sqlDataReader["SubCatName"].ToString() : string.Empty;
                                getResponse.LastPurDate = _sqlDataReader["LastPurDate"] == DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(null)) : DateOnly.FromDateTime(Convert.ToDateTime(_sqlDataReader["LastPurDate"]));
                                getResponse.Unit = _sqlDataReader["Unit"] != DBNull.Value ? _sqlDataReader["Unit"].ToString() : string.Empty;
                                getResponse.PrefAlt = _sqlDataReader["PrefAlt"] != DBNull.Value ? _sqlDataReader["PrefAlt"].ToString() : string.Empty;
                                getResponse.OpeningStock = _sqlDataReader["OpeningStock"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["OpeningStock"]) : 0;
                                getResponse.CurrentStock = _sqlDataReader["CurrentStock"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["CurrentStock"]) : 0;
                                getResponse.ReorderStock = _sqlDataReader["ReorderStock"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["ReorderStock"]) : 0;
                                getResponse.MaxStock = _sqlDataReader["OpeningStock"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["MaxStock"]) : 0;
                                getResponse.LastPurRate = _sqlDataReader["LastPurRate"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["LastPurRate"]) : 0;
                                getResponse.Amount = _sqlDataReader["Amount"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Amount"]) : 0;
                                getResponse.CreatedAT = _sqlDataReader["CreatedAT"] == DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(null)) : DateOnly.FromDateTime(Convert.ToDateTime(_sqlDataReader["CreatedAT"]));
                                getResponse.ExpiryDate = _sqlDataReader["ExpiryDate"] == DBNull.Value ? DateOnly.FromDateTime(Convert.ToDateTime(null)) : DateOnly.FromDateTime(Convert.ToDateTime(_sqlDataReader["ExpiryDate"]));
                                getResponse.IsActive = _sqlDataReader["IsActive"] != DBNull.Value? Convert.ToBoolean(_sqlDataReader["IsActive"]): true;
                                response.itemreadinformation.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }
        public async Task<ReadInformationResponse> SupplierReadInformation()
        {
            ReadInformationResponse response = new ReadInformationResponse();
            response.supplierreadinformation = new List<SupplierReadInformation>();
            response.IsSuccess = true;
            response.Message = "Successful";
            //string nu = "null";
            try
            {
                string StoreProcedure = "usp_ViewSupplier";
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadInformation, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                SupplierReadInformation getResponse = new SupplierReadInformation();
                                getResponse.SupplierID = _sqlDataReader["SupplierID"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["SupplierID"]) : 0;
                                getResponse.SupplierName = _sqlDataReader["SupplierName"] != DBNull.Value ? _sqlDataReader["SupplierName"].ToString() : string.Empty;
                                getResponse.ContactNumber = _sqlDataReader["ContactNumber"] != DBNull.Value ? _sqlDataReader["Email"].ToString() : string.Empty;
                                getResponse.Email = _sqlDataReader["CatName"] != DBNull.Value ? _sqlDataReader["CatName"].ToString() : string.Empty;
                                getResponse.Address = _sqlDataReader["SubCatName"] != DBNull.Value ? _sqlDataReader["Address"].ToString() : string.Empty;
                                response.supplierreadinformation.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }
        public async Task<ReadInformationResponse> ItemCatInformation()
        {
            ReadInformationResponse response = new ReadInformationResponse();
            response.itemcatreadinformation = new List<ItemCatInformation>();
            response.IsSuccess = true;
            response.Message = "Successful";
            //string nu = "null";
            try
            {
                string StoreProcedure = "usp_ViewItemCat";
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadInformation, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                ItemCatInformation getResponse = new ItemCatInformation();
                                getResponse.CategoryID = _sqlDataReader["CategoryID"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["CategoryID"]) : 0;
                                getResponse.CategoryName = _sqlDataReader["SupplierName"] != DBNull.Value ? _sqlDataReader["CategoryName"].ToString() : string.Empty;
                                response.itemcatreadinformation.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }
        public async Task<ReadInformationResponse> SubCatInformation()
        {
            ReadInformationResponse response = new ReadInformationResponse();
            response.subcatreadinformation = new List<SubCatInformation>();
            response.IsSuccess = true;
            response.Message = "Successful";
            //string nu = "null";
            try
            {
                string StoreProcedure = "usp_ViewSubCat";
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadInformation, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                SubCatInformation getResponse = new SubCatInformation();
                                getResponse.SubCatID = _sqlDataReader["CategoryID"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["SubCatID"]) : 0;
                                getResponse.CategoryName = _sqlDataReader["SupplierName"] != DBNull.Value ? _sqlDataReader["CategoryName"].ToString() : string.Empty;
                                getResponse.SubCatName = _sqlDataReader["SubCatName"] != DBNull.Value ? _sqlDataReader["SubCatName"].ToString() : string.Empty;
                                response.subcatreadinformation.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }


        public async Task<ReadInformationResponse> ICReadInformation()
        {
            ReadInformationResponse response = new ReadInformationResponse();
            response.icreadinformation = new List<ICReadInformation>();
            response.IsSuccess = true;
            response.Message = "Successful";
            //string nu = "null";
            try
            {
                string StoreProcedure = "usp_ViewItemCount";
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadInformation, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                ICReadInformation getResponse = new ICReadInformation();
                                getResponse.Count = _sqlDataReader["Count"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Count"]) : 0;
                                
                                response.icreadinformation.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }
        public async Task<ReadInformationResponse> SupCReadInformation()
        {
            ReadInformationResponse response = new ReadInformationResponse();
            response.supcreadinformation = new List<SupCReadInformation>();
            response.IsSuccess = true;
            response.Message = "Successful";
            //string nu = "null";
            try
            {
                string StoreProcedure = "usp_ViewSupCount";
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadInformation, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                SupCReadInformation getResponse = new SupCReadInformation();
                                getResponse.Count = _sqlDataReader["Count"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Count"]) : 0;

                                response.supcreadinformation.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }
        public async Task<ReadInformationResponse> ItemCatCReadInformation()
        {
            ReadInformationResponse response = new ReadInformationResponse();
            response.itemcatcreadinformation = new List<ItemCatCReadInformation>();
            response.IsSuccess = true;
            response.Message = "Successful";
            //string nu = "null";
            try
            {
                string StoreProcedure = "usp_ViewItemCatCount";
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadInformation, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                ItemCatCReadInformation getResponse = new ItemCatCReadInformation();
                                getResponse.Count = _sqlDataReader["Count"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Count"]) : 0;

                                response.itemcatcreadinformation.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }
        public async Task<ReadInformationResponse> SubCatCReadInformation()
        {
            ReadInformationResponse response = new ReadInformationResponse();
            response.subcatcreadinformation = new List<SubCatCReadInformation>();
            response.IsSuccess = true;
            response.Message = "Successful";
            //string nu = "null";
            try
            {
                string StoreProcedure = "usp_ViewSubCatCount";
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadInformation, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                SubCatCReadInformation getResponse = new SubCatCReadInformation();
                                getResponse.Count = _sqlDataReader["Count"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Count"]) : 0;

                                response.subcatcreadinformation.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }




        public async Task<UpdateInformationResponse> UpdateInformation(ItemUpdateInformationRequest request)
        {
            UpdateInformationResponse resposne = new UpdateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_EditItem";
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.UpdateInformation, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@ItemCode", request.ItemCode);
                        sqlCommand.Parameters.AddWithValue("@ItemName", request.ItemName);
                        sqlCommand.Parameters.AddWithValue("@CatName", request.CatName);
                        sqlCommand.Parameters.AddWithValue("@SubCatname", request.SubCatname);
                        sqlCommand.Parameters.AddWithValue("@OpeningStock", request.OpeningStock);
                        sqlCommand.Parameters.AddWithValue("@CurrentStock", request.CurrentStock);
                        sqlCommand.Parameters.AddWithValue("@ReorderStock", request.ReorderStock);
                        sqlCommand.Parameters.AddWithValue("@MaxStock", request.MaxStock);
                        sqlCommand.Parameters.AddWithValue("@LastPurRate", request.LastPurRate);
                        sqlCommand.Parameters.AddWithValue("@LastPurDate", request.LastPurDate);
                        sqlCommand.Parameters.AddWithValue("@PrefAlt", request.PrefAlt);
                        sqlCommand.Parameters.AddWithValue("@Amount", request.Amount);
                        sqlCommand.Parameters.AddWithValue("@HScode", request.HScode);
                        sqlCommand.Parameters.AddWithValue("@IsActive", request.IsActive);
                        sqlCommand.Parameters.AddWithValue("@CreatedAT", request.CreatedAT);
                        sqlCommand.Parameters.AddWithValue("@ExpiryDate", request.ExpiryDate);
                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "Information Not Update";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {

                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }
        public async Task<UpdateInformationResponse> UpdateInformation(SupplierUpdateInformationRequest request)
        {
            UpdateInformationResponse resposne = new UpdateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_EditSupplier";
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.UpdateInformation, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@ItemCode", request.SupplierID);
                        sqlCommand.Parameters.AddWithValue("@SupplierName", request.SupplierName);
                        sqlCommand.Parameters.AddWithValue("@ContactNumber", request.ContactNumber);
                        sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                        sqlCommand.Parameters.AddWithValue("@Address", request.Address);
                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "Information Not Update";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {

                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }
        public async Task<UpdateInformationResponse> UpdateInformation(ItemCatUpdateInformationRequest request)
        {
            UpdateInformationResponse resposne = new UpdateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_EditItemCat";
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.UpdateInformation, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@CategoryID", request.CategoryID);
                        sqlCommand.Parameters.AddWithValue("@CategoryName", request.CategoryName);
                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "Information Not Update";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {

                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }
        public async Task<UpdateInformationResponse> UpdateInformation(SubCatUpdateInformationRequest request)
        {
            UpdateInformationResponse resposne = new UpdateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_EditSubCat";
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.UpdateInformation, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                       
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@SubCatID", request.SubCatID);
                        sqlCommand.Parameters.AddWithValue("@CategoryName", request.CategoryName);
                        sqlCommand.Parameters.AddWithValue("@SubCatName", request.SubCatName);
                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "Information Not Update";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {

                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }




        public async Task<DeleteInformationResponse> IItemDeleteInfo(DeleteInformationRequest request)
        {
            DeleteInformationResponse resposne = new DeleteInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_deleteItem";

                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        //sqlCommand.Parameters.AddWithValue("?UserId", request.UserId);
                        sqlCommand.Parameters.AddWithValue("@ItemCode", request.Id);
                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "UnSuccessful";
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }
        public async Task<DeleteInformationResponse> SupplierDeleteInfo(DeleteInformationRequest request)
        {
            DeleteInformationResponse resposne = new DeleteInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_deleteSupplier";

                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        //sqlCommand.Parameters.AddWithValue("?UserId", request.UserId);
                        sqlCommand.Parameters.AddWithValue("@SupplierID", request.Id);
                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "UnSuccessful";
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }
        public async Task<DeleteInformationResponse> ItemCatDeleteInfo(DeleteInformationRequest request)
        {
            DeleteInformationResponse resposne = new DeleteInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_deleteItemCat";

                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        //sqlCommand.Parameters.AddWithValue("?UserId", request.UserId);
                        sqlCommand.Parameters.AddWithValue("@CategoryID", request.Id);
                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "UnSuccessful";
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }
        public async Task<DeleteInformationResponse> SubCatDeleteInfo(DeleteInformationRequest request)
        {
            DeleteInformationResponse resposne = new DeleteInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "usp_deleteSubCat";

                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        //sqlCommand.Parameters.AddWithValue("?UserId", request.UserId);
                        sqlCommand.Parameters.AddWithValue("@SubCatID", request.Id);
                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "UnSuccessful";
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }




        public async Task<SearchInformationByNameResponse> SubCatSearchInformationByCat(SearchInformationByNameRequest request)
        {
            SearchInformationByNameResponse response = new SearchInformationByNameResponse();
            response.subcatsearchinformationByCat = new List<SubCatSearchInformationByCat>();
            response.IsSuccess = true;
            response.Message = "Successful";
            string nu = "null";
            try
            {
                string StoreProcedure = "usp_ViewSubCatbyCat";
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadInformation, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    sqlCommand.Parameters.AddWithValue("@CategoryName", request.Name);
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                SubCatSearchInformationByCat getResponse = new SubCatSearchInformationByCat();
                                ///getResponse.Id = _sqlDataReader["Id"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Id"]) : 0;
                                getResponse.SubCatID = _sqlDataReader["SubCatID"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["SubCatID"]) : 0;
                                //getResponse.CategoryName = _sqlDataReader["SupplierName"] != DBNull.Value ? _sqlDataReader["CategoryName"].ToString() : string.Empty;
                                getResponse.SubCatName = _sqlDataReader["SubCatName"] != DBNull.Value ? _sqlDataReader["SubCatName"].ToString() : string.Empty;

                                response.subcatsearchinformationByCat.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }
        public async Task<SearchInformationByNameResponse> ItemSearchInformationByName(SearchInformationByNameRequest request)
        {
            SearchInformationByNameResponse response = new SearchInformationByNameResponse();
            response.itemsearchInformationByName = new List<ItemSearchInformationByName>();
            response.IsSuccess = true;
            response.Message = "Successful";
            string nu = "null";
            try
            {
                string StoreProcedure = "usp_ViewItembySubCat";
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadInformation, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    sqlCommand.Parameters.AddWithValue("@SubCatName", request.Name);
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                ItemSearchInformationByName getResponse = new ItemSearchInformationByName();
                                getResponse.ItemCode = _sqlDataReader["ItemCode"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["ItemCode"]) : 0;
                                getResponse.ItemName = _sqlDataReader["ItemName"] != DBNull.Value ? _sqlDataReader["ItemName"].ToString() : string.Empty;

                                response.itemsearchInformationByName.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }



    }
}
